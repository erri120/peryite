using System;
using System.IO;
using Peryite.Common.Skyrim.GlobalDataTypes;

namespace Peryite.Common.Skyrim
{
    public enum GlobalDataType
    {
        MiscStats = 0,
        PlayerLocation = 1,
        TES = 2,
        GlobalVariables = 3,
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

    public interface IGlobalData
    {
        GlobalDataType Type { get; }

        IGlobalData ReadData(BinaryReader br);
    }

    public static partial class BinaryReaderExtensions
    {
        public static IGlobalData ReadGlobalData(this BinaryReader br, int typeStart, int typeEnd)
        {
            var type = (GlobalDataType) br.ReadUInt32();
            var length = br.ReadUInt32();

            if ((int)type < typeStart || (int)type > typeEnd)
                throw new CorruptedSaveFileException($"Expected GlobalData type should be between {typeStart} and {typeEnd} but is {type}!", br);

            return br.ReadIGlobalDataType(type, length);
        }

        public static IGlobalData ReadIGlobalDataType(this BinaryReader br, GlobalDataType type, uint length)
        {
            var positionStart = br.BaseStream.Position;

            IGlobalData result = default!;

            switch (type)
            {
                case GlobalDataType.MiscStats:
                    result = new MiscStats().ReadData(br);
                    break;
                case GlobalDataType.PlayerLocation:
                    result = new PlayerLocation().ReadData(br);
                    break;
                case GlobalDataType.TES:
                    result = new TES().ReadData(br);
                    break;
                case GlobalDataType.GlobalVariables:
                    result = new GlobalVariables().ReadData(br);
                    break;
                case GlobalDataType.CreatedObject:
                    result = new CreatedObjects().ReadData(br);
                    break;
                case GlobalDataType.Effects:
                    result = new Effects().ReadData(br);
                    break;
                case GlobalDataType.Weather:
                    result = new Weather().ReadData(br);
                    break;
                case GlobalDataType.Audio:
                    result = new Audio().ReadData(br);
                    break;
                case GlobalDataType.SkyCells:
                    result = new SkyCells().ReadData(br);
                    break;
                case GlobalDataType.ProcessLists:
                    break;
                case GlobalDataType.Combat:
                    break;
                case GlobalDataType.Interface:
                    break;
                case GlobalDataType.ActorCauses:
                    break;
                case GlobalDataType.Unknown104:
                    break;
                case GlobalDataType.DetectionManager:
                    break;
                case GlobalDataType.LocationMetaData:
                    break;
                case GlobalDataType.QuestStaticData:
                    break;
                case GlobalDataType.StoryTeller:
                    break;
                case GlobalDataType.MagicFavorites:
                    break;
                case GlobalDataType.PlayerControls:
                    break;
                case GlobalDataType.StoryEventManager:
                    break;
                case GlobalDataType.IngredientShared:
                    break;
                case GlobalDataType.MenuControls:
                    break;
                case GlobalDataType.MenuTopicManager:
                    break;
                case GlobalDataType.TempEffects:
                    break;
                case GlobalDataType.Papyrus:
                    break;
                case GlobalDataType.AnimObjects:
                    break;
                case GlobalDataType.Timer:
                    break;
                case GlobalDataType.SynchronizedAnimations:
                    break;
                case GlobalDataType.Main:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            var positionEnd = br.BaseStream.Position;

            if (positionStart + length != positionEnd)
                throw new CorruptedSaveFileException(
                    $"Expected amounts of bytes: {length}, number of bytes read: {positionEnd - positionStart} while reading GlobalData of type {type}!", br);

            return result;
        }
    }
}
