using System.IO;

namespace Peryite.Common.Skyrim.GlobalDataTypes
{
    public class PlayerLocation : IGlobalData
    {
        public GlobalDataType Type => GlobalDataType.PlayerLocation;

        /// <summary>
        /// Number of next savegame specific object id. Use <c>toString("X8)</c>
        /// </summary>
        public uint NextObjectID;

        /// <summary>
        /// Usually 0x0 or a worldspace, <see cref="X"/> and <see cref="Y"/> represent
        /// a cell in this worldspace
        /// </summary>
        public RefID WorldSpace1;

        /// <summary>
        /// X-Coordinate (cell coordinate) in <see cref="WorldSpace1"/>
        /// </summary>
        public int X;

        /// <summary>
        /// Y-Coordinate (cell coordinate) in <see cref="WorldSpace1"/>
        /// </summary>
        public int Y;

        /// <summary>
        /// This can either be a worldspace or an interior cell. If it's a worldspace,
        /// the player is located at the cell (<see cref="X"/>, <see cref="Y"/>). <see cref="PosX"/>,
        /// <see cref="PosY"/> and <see cref="PosZ"/> is the player's position inside the cell.
        /// </summary>
        public RefID WorldSpace2;

        /// <summary>
        /// X-Coordinate in <see cref="WorldSpace2"/>
        /// </summary>
        public float PosX;

        /// <summary>
        /// Y-Coordinate in <see cref="WorldSpace2"/>
        /// </summary>
        public float PosY;

        /// <summary>
        /// Z-Coordinate in <see cref="WorldSpace2"/>
        /// </summary>
        public float PosZ;

        /// <summary>
        /// Unknown
        /// </summary>
        public byte Unknown;

        public IGlobalData ReadData(BinaryReader br)
        {
            NextObjectID = br.ReadUInt32();
            WorldSpace1 = br.ReadRefID();
            X = br.ReadInt32();
            Y = br.ReadInt32();
            WorldSpace2 = br.ReadRefID();
            PosX = br.ReadSingle();
            PosY = br.ReadSingle();
            PosZ = br.ReadSingle();
            Unknown = br.ReadByte();

            return this;
        }
    }
}
