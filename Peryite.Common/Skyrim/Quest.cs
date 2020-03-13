using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Peryite.Common.Skyrim
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum QuestChangeFormFlag : uint
    {
        CHANGE_FORM_FLAGS = 0x01,
        CHANGE_QUEST_FLAGS = 0x02,
        CHANGE_QUEST_SCRIPT_DELAY = 0x04,
        CHANGE_QUEST_ALREADY_RUN = 0x4000000,
        CHANGE_QUEST_INSTANCES = 0x8000000,
        CHANGE_QUEST_RUNDATA = 0x10000000,
        CHANGE_QUEST_OBJECTIVES = 0x20000000,
        CHANGE_QUEST_SCRIPT = 0x40000000,
        CHANGE_QUEST_STAGES = 0x80000000
    }

    public class QuestInstances
    {
        [Read(1)]
        public uint UnknownUInt;

        private VSVAL _count;

        [Read(2)]
        public VSVAL Count
        {
            get => _count;
            set
            {
                _count = value;
                QuestInstanceData = new QuestInstanceData[_count.Value];
            }
        }
        [Read(3, IsCustomType = true)]
        public QuestInstanceData[]? QuestInstanceData;
    }

    public class QuestInstanceData
    {
        [Read(1)]
        public uint UnknownUInt1;

        private VSVAL _count1;

        [Read(2)]
        public VSVAL Count1
        {
            get => _count1;
            set
            {
                _count1 = value;
                Unknown0Array = new Unknown0[_count1.Value];
            }
        }
        [Read(3, IsCustomType = true)]
        public Unknown0[]? Unknown0Array;

        private VSVAL _count2;

        [Read(4)]
        public VSVAL Count2
        {
            get => _count2;
            set
            {
                _count2 = value;
                Unknown1Array = new Unknown1[_count2.Value];
            }
        }
        [Read(5, IsCustomType = true)]
        public Unknown1[]? Unknown1Array;
        [Read(6)]
        public ushort UnknownUShort;
        [Read(7)]
        public byte UnknownByte;

        public struct Unknown0
        {
            [Read(1)]
            public uint UnknownUInt;
            [Read(2)]
            public RefID UnknownRefID;
        }

        public struct Unknown1
        {
            [Read(1)]
            public RefID UnknownRefID;
            [Read(2)]
            public uint UnknownUInt;
        }
    }

    public class QuestRunData
    {
        [Read(1)]
        public byte UnknownByte;

        private uint _count1;

        [Read(2)]
        public uint Count1
        {
            get => _count1;
            set
            {
                _count1 = value;
                QuestRunDataItems1 = new QuestRunDataItem1[_count1];
            }
        }
        [Read(3, IsCustomType = true)]
        public QuestRunDataItem1[]? QuestRunDataItems1;

        private uint _count2;

        [Read(4)]
        public uint Count2
        {
            get => _count2;
            set
            {
                _count2 = value;
                QuestRunDataItems2 = new QuestRunDataItem2[_count2];
            }
        }
        
        [Read(5, IsCustomType = true)]
        public QuestRunDataItem2[]? QuestRunDataItems2;

        [Read(6)]
        public byte Flag;

        [Read(7, IsCustomType = true)]
        [ConditionalParsing(Name = nameof(Flag), ChainingType = typeof(byte), Not = true, Chaining = new object[]{(byte)0})]
        public QuestRunDataItem3 QuestRunDataItem3;
    }

    public struct QuestRunDataItem1
    {
        [Read(1)]
        public uint UnknownUInt;
        private byte _flag;
        
        [Read(2)]
        public byte Flag
        {
            get => _flag;
            set
            {
                _flag = value;
                RefIDs = _flag == 0 ? new RefID[1] : new RefID[5];
            }
        }
        [Read(3)]
        public RefID[]? RefIDs;
    }

    public struct QuestRunDataItem2
    {
        [Read(1)]
        public uint UnknownUInt;
        [Read(2)]
        public RefID UnknownRefID;
    }

    public struct QuestRunDataItem3
    {
        [Read(1)]
        public uint UnknownUInt;
        [Read(2)]
        public float UnknownFloat;

        private uint _count;

        [Read(3)]
        public uint Count
        {
            get => _count;
            set
            {
                _count = value;
                QuestRunDataItem3Array = new QuestRunDataItem3Data[_count];
            }
        }
        [Read(4, IsCustomType = true)]
        public QuestRunDataItem3Data[]? QuestRunDataItem3Array;

        public struct QuestRunDataItem3Data
        {
            [Read(1)]
            public uint Type;
            [Read(2)]
            [ConditionalParsing(ChainingType = typeof(uint), Name = nameof(Type), And = false, Chaining = new object[]
            {
                (uint)1,2,4
            })]
            public RefID UnknownRefID;
            [Read(3)]
            [ConditionalParsing(ChainingType = typeof(uint), Name = nameof(Type), And = false, Chaining = new object[]
            {
                (uint)3
            })]
            public uint UnknownUInt;
        }
    }
}
