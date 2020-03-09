using System;
using System.IO;

namespace Peryite.Common
{
    public enum Game
    {
        Skyrim,
        SkyrimSpecialEdition,
        Oblivion,
        Fallout3,
        Fallout4,
        FalloutNewVegas
    }

    public interface ISaveFile
    {
        Game Game { get; }

        void LoadFile(string path);
    }

    public class SaveFileException : Exception
    {
        public SaveFileException(string msg) : base(msg) { }
    }

    public class CorruptedSaveFileException : SaveFileException
    {
        public CorruptedSaveFileException(string msg) : base(msg) { }

        public CorruptedSaveFileException(string msg, BinaryReader br) : base(
            msg + $"\nAt position: {br.BaseStream.Position}")
        {
        }
    }
}
