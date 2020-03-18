using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using ICSharpCode.SharpZipLib.Zip.Compression;
using K4os.Compression.LZ4;

namespace Peryite.Common.Skyrim
{
    public enum SkyrimSaveFileCompression
    {
        None = 0,
        ZLib = 1,
        L4Z = 2
    }

    public class SkyrimSaveFile : ISaveFile
    {
        public Game Game => Game.Skyrim;

        public const string SkyrimMagic = "TESV_SAVEGAME";

        public uint HeaderSize;
        public Header Header;
        public bool IsSpecialEdition;
        public Bitmap? Screenshot;
        public SkyrimSaveFileCompression Compression;
        public byte FormVersion;
        public uint PluginInfoSize;
        public PluginInfo PluginInfo;
        public FileLocationTable FileLocationTable;
        public IGlobalData[] GlobalDataTable1 = default!;
        public IGlobalData[] GlobalDataTable2 = default!;
        public ChangeForm[] ChangeForms = default!;
        public IGlobalData[] GlobalDataTable3 = default!;
        public uint FormIDArrayCount;
        public uint[] FormIDArray = default!;
        public uint VisitedWorldspaceArrayCount;
        public uint[] VisitedWorldspaceArray = default!;
        public uint Unknown3TableSize;
        public Unknown3Table Unknown3Table;

        public void LoadFile(string path)
        {
            if (!File.Exists(path))
                throw new ArgumentException($"The provided file at {path} does not exist!");

            byte[] fileBytes = File.ReadAllBytes(path);

            using var ms = new MemoryStream();
            ms.Write(fileBytes, 0, fileBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);
            using var br = new BinaryReader(ms);

            //reference: https://en.uesp.net/wiki/Tes5Mod:Save_File_Format
            //file format conventions: https://en.uesp.net/wiki/Tes5Mod:File_Format_Conventions

            char[] magic = br.ReadChars(13);
            var magicString = new string(magic);
            if (magicString != SkyrimMagic)
            {
                throw new CorruptedSaveFileException($"The magic string of the save file does not equal \"TESV_SAVEGAME\" but \"{magicString}\"", br);
            }

            HeaderSize = br.ReadUInt32();
            Header = new Header
            {
                Version = br.ReadUInt32(),
                SaveNumber = br.ReadUInt32(),
                PlayerName = br.ReadWString(),
                PlayerLevel = br.ReadUInt32(),
                PlayerLocation = br.ReadWString(),
                GameDate = br.ReadWString(),
                PlayerRaceEditorID = br.ReadWString(),
                PlayerSex = br.ReadUInt16(),
                PlayerCurExp = br.ReadSingle(),
                PlayerLevelUpExp = br.ReadSingle(),
                FileTime = br.ReadFileTime()
            };

            IsSpecialEdition = Header.Version == 12;

            var screenshotWidth = (int)br.ReadUInt32();
            var screenshotHeight = (int)br.ReadUInt32();

            //for some unknown reason the type of compression comes between the image resolution and the image data
            Compression = IsSpecialEdition ? (SkyrimSaveFileCompression) br.ReadUInt16() : 0;

            Screenshot = ReadScreenshot(br, IsSpecialEdition, screenshotWidth, screenshotHeight);

            if (Compression != SkyrimSaveFileCompression.None)
            {
                if (Compression == SkyrimSaveFileCompression.ZLib)
                {
                    //IGNORED: https://github.com/ModOrganizer2/modorganizer-game_gamebryo/blob/178c35674a8d3a6a5312065898f2e3c35fb701ac/src/gamebryo/gamebryosavegame.cpp#L226
                    return;
                }
                
                /*
                 * 1) read the compressed data
                 * 2) save the current position
                 * 3) decompress the data
                 * 4) copy the decompressed data to the main MemoryStream
                 * 5) jump to the saved position to continue reading
                 */

                var decompressedSize = br.ReadUInt32();
                var compressedSize = br.ReadUInt32();

                byte[] compressed = br.ReadBytes((int) compressedSize);
                var currentPosition = ms.Position;
                var decompressed = new byte[decompressedSize];

                var res = LZ4Codec.Decode(compressed, 0, compressed.Length, decompressed, 0, decompressed.Length);

                if (res != decompressedSize)
                {
                    throw new CorruptedSaveFileException($"Could not decode compressed data at position {currentPosition} with length {compressedSize} to a decompressed byte array of length {decompressedSize}", br);
                }

                using var tempStream = new MemoryStream(decompressed);
                tempStream.CopyTo(ms);
                ms.Position = currentPosition;
                tempStream.Dispose();
            }

            FormVersion = br.ReadByte();
            PluginInfoSize = br.ReadUInt32();
            PluginInfo = new PluginInfo
            {
                PluginCount = br.ReadByte()
            };

            PluginInfo.Plugins = new WString[PluginInfo.PluginCount];
            for (var i = 0; i < PluginInfo.PluginCount; i++)
            {
                PluginInfo.Plugins[i] = br.ReadWString();
            }

            FileLocationTable = new FileLocationTable
            {
                FormIDArrayCountOffset = br.ReadUInt32(),
                UnknownTable3Offset = br.ReadUInt32(),
                GlobalDataTable1Offset = br.ReadUInt32(),
                GlobalDataTable2Offset = br.ReadUInt32(),
                ChangeFormsOffset = br.ReadUInt32(),
                GlobalDataTable3Offset = br.ReadUInt32(),
                GlobalDataTable1Count = br.ReadUInt32(),
                GlobalDataTable2Count = br.ReadUInt32(),
                GlobalDataTable3Count = br.ReadUInt32(),
                ChangeFormCount = br.ReadUInt32(),
                Unused = ReadUInt32Array(br, 15)
            };

            // types 0 to 8
            GlobalDataTable1 = new IGlobalData[FileLocationTable.GlobalDataTable1Count];
            
            // types 100 to 114
            GlobalDataTable2 = new IGlobalData[FileLocationTable.GlobalDataTable2Count];
            
            ChangeForms = new ChangeForm[FileLocationTable.ChangeFormCount];
            
            // types 1000 to 1005
            GlobalDataTable3 = new IGlobalData[FileLocationTable.GlobalDataTable3Count];

            for (var i = 0; i < GlobalDataTable1.Length; i++)
            {
                GlobalDataTable1[i] = br.ReadGlobalData(0, 8);
            }

            for (var i = 0; i < GlobalDataTable2.Length; i++)
            {
                GlobalDataTable2[i] = br.ReadGlobalData(100, 114);
            }

            for (var i = 0; i < ChangeForms.Length; i++)
            {
                var changeForm = br.ReadChangeForm();

                //check if data is compressed
                if (changeForm.Length2 != 0)
                {
                    byte[] compressed = br.ReadBytes((int) changeForm.Length1);
                    var decompressed = new byte[changeForm.Length2];

                    var inflater = new Inflater();
                    inflater.SetInput(compressed);
                    var res = inflater.Inflate(decompressed);

                    if(res != changeForm.Length2)
                        throw new CorruptedSaveFileException($"Result of inflation resulted in a return value of {res}!", br);

                    changeForm.Compression = SkyrimSaveFileCompression.ZLib;
                    changeForm.Data = decompressed;
                }
                else
                {
                    changeForm.Data = br.ReadBytes((int) changeForm.Length1);
                }

                ChangeForms[i] = changeForm;
            }

            for (var i = 0; i < GlobalDataTable3.Length; i++)
            {
                GlobalDataTable3[i] = br.ReadGlobalData(1000, 1005);
            }

            return;
        }

        private static Bitmap ReadScreenshot(BinaryReader br, bool isSpecialEdition, int width, int height)
        {
            Bitmap result;
            var data = new byte[height][][];

            //the special edition save screenshot also comes with an alpha layer while Oldrim only has RGB
            var layers = 3;
            if (isSpecialEdition)
            {
                layers = 4;
                result = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            }
            else
            {
                result = new Bitmap(width, height, PixelFormat.Format32bppRgb);
            }
            
            for (var i = 0; i < height; i++)
            {
                data[i] = new byte[width][];
                for (var j = 0; j < width; j++)
                {
                    data[i][j] = new byte[layers];
                    for (var k = 0; k < layers; k++)
                    {
                        data[i][j][k] = br.ReadByte();
                    }

                    var r = data[i][j][0];
                    var g = data[i][j][1];
                    var b = data[i][j][2];
                    var a = isSpecialEdition ? data[i][j][3] : 255;
                    var c = Color.FromArgb(a, r, g, b);
                    result.SetPixel(j, i, c);
                }
            }

            return result;
        }

        private static uint[] ReadUInt32Array(BinaryReader br, int length)
        {
            var result = new uint[length];

            for (var i = 0; i < length; i++)
            {
                result[i] = br.ReadUInt32();
            }

            return result;
        }
    }
}
