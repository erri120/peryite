using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Peryite.Common.Skyrim
{
    public enum GlobalDataType
    {
        MiscStats = 0,
        PlayerLocation = 1,
        TES = 2,
        GlobalVariable = 3,
        CreatedObject = 4,
        Effects = 5,
        Weather = 6,
        Audio = 7,
        SkyCells = 8,

        ProcessLists = 100,
        Combat = 101,
        Interface = 102,
        ActorCauses = 103,
        Unknown104 = 104,
        DetectionManager = 105,
        LocationMetaData = 106,
        QuestStaticData = 107,
        StoryTeller = 108,
        MagicFavorites = 109,
        PlayerControls = 110,
        StoryEventManager = 111,
        IngredientShared = 112,
        MenuControls = 113,
        MenuTopicManager = 114,

        TempEffects = 1000,
        Papyrus = 1001,
        AnimObjects = 1002,
        Timer = 1003,
        SynchronizedAnimations = 1004,
        Main = 1005
    }

    public class GlobalData
    {
        public GlobalDataType Type;
        public uint Length;
        public IGlobalDataType[] Data;
    }

    public interface IGlobalDataType
    {
        GlobalDataType Type { get; }

        void ReadData(BinaryReader br);
    }
}
