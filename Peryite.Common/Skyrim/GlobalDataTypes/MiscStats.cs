using System.IO;

namespace Peryite.Common.Skyrim.GlobalDataTypes
{
    public class MiscStats : IGlobalData
    {
        public GlobalDataType Type => GlobalDataType.MiscStats;

        public uint Count;
        public MiscStat[] Stats = default!;

        public IGlobalData ReadData(BinaryReader br)
        {
            Count = br.ReadUInt32();
            Stats = new MiscStat[Count];
            for (var i = 0; i < Count; i++)
            {
                var stat = new MiscStat
                {
                    Name = br.ReadWString()
                };

                var category = br.ReadByte();

                stat.Category = (MiscStatCategory) category;
                stat.Value = br.ReadInt32();

                Stats[i] = stat;
            }

            return this;
        }
    }

    public enum MiscStatCategory
    {
        General = 0,
        Quest = 1,
        Combat = 2,
        Magic = 3,
        Crafting = 4,
        Crime = 5,
        DLC = 6
    }

    public struct MiscStat
    {
        public WString Name;
        public MiscStatCategory Category;
        public int Value;
    }
}
