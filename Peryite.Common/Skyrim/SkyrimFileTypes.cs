using System;

namespace Peryite.Common.Skyrim
{
    /// <summary>
    /// Header for a <see cref="SkyrimSaveFile"/>
    /// </summary>
    public struct Header
    {
        /// <summary>
        /// Version number
        /// </summary>
        public uint Version;

        /// <summary>
        /// Save number aka often saved the Player with the current Character
        /// </summary>
        public uint SaveNumber;

        /// <summary>
        /// The Player name
        /// </summary>
        public WString PlayerName;

        /// <summary>
        /// The current Player level
        /// </summary>
        public uint PlayerLevel;

        /// <summary>
        /// The current Player location (name of the cell)
        /// </summary>
        public WString PlayerLocation;

        /// <summary>
        /// In-game date at the time of saving
        /// </summary>
        public WString GameDate;

        /// <summary>
        /// EditorID of the player race
        /// </summary>
        public WString PlayerRaceEditorID;

        /// <summary>
        /// The Player sex saved as 0 = Male or 1 = Female
        /// </summary>
        public ushort PlayerSex;

        /// <summary>
        /// Experience gathered
        /// </summary>
        public float PlayerCurExp;

        /// <summary>
        /// Experience required for the next level up
        /// </summary>
        public float PlayerLevelUpExp;

        /// <summary>
        /// 
        /// </summary>
        public DateTime FileTime;
    }

    /// <summary>
    /// Struct containing info about loaded plugins
    /// </summary>
    public struct PluginInfo
    {
        /// <summary>
        /// Number of loaded plugins
        /// </summary>
        public byte PluginCount;

        /// <summary>
        /// Array of all loaded plugin names
        /// </summary>
        public WString[] Plugins;
    }

    public struct FileLocationTable
    {
        public uint FormIDArrayCountOffset;
        public uint UnknownTable3Offset;
        public uint GlobalDataTable1Offset;
        public uint GlobalDataTable2Offset;
        public uint ChangeFormsOffset;
        public uint GlobalDataTable3Offset;
        public uint GlobalDataTable1Count;
        public uint GlobalDataTable2Count;
        public uint GlobalDataTable3Count;
        public uint ChangeFormCount;
        public uint[] Unused;
    }

    public struct ChangeForm
    {

    }

    public struct Unknown3Table
    {

    }
}
