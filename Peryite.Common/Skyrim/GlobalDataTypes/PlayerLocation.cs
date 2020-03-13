namespace Peryite.Common.Skyrim.GlobalDataTypes
{
    public class PlayerLocation : IGlobalData
    {
        public GlobalDataType Type => GlobalDataType.PlayerLocation;

        /// <summary>
        /// Number of next savegame specific object id. Use <c>toString("X8)</c>
        /// </summary>
        [Read(1)]
        public uint NextObjectID;

        /// <summary>
        /// Usually 0x0 or a worldspace, <see cref="X"/> and <see cref="Y"/> represent
        /// a cell in this worldspace
        /// </summary>
        [Read(2)]
        public RefID WorldSpace1;

        /// <summary>
        /// X-Coordinate (cell coordinate) in <see cref="WorldSpace1"/>
        /// </summary>
        [Read(3)]
        public int X;

        /// <summary>
        /// Y-Coordinate (cell coordinate) in <see cref="WorldSpace1"/>
        /// </summary>
        [Read(4)]
        public int Y;

        /// <summary>
        /// This can either be a worldspace or an interior cell. If it's a worldspace,
        /// the player is located at the cell (<see cref="X"/>, <see cref="Y"/>). <see cref="PosX"/>,
        /// <see cref="PosY"/> and <see cref="PosZ"/> is the player's position inside the cell.
        /// </summary>
        [Read(5)]
        public RefID WorldSpace2;

        /// <summary>
        /// X-Coordinate in <see cref="WorldSpace2"/>
        /// </summary>
        [Read(6)]
        public float PosX;

        /// <summary>
        /// Y-Coordinate in <see cref="WorldSpace2"/>
        /// </summary>
        [Read(7)]
        public float PosY;

        /// <summary>
        /// Z-Coordinate in <see cref="WorldSpace2"/>
        /// </summary>
        [Read(8)]
        public float PosZ;

        /// <summary>
        /// Unknown
        /// </summary>
        [Read(9)]
        public byte Unknown;
    }
}
