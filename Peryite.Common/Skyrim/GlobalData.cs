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
                    result = new MiscStats().ReadThis(br);
                    break;
                case GlobalDataType.PlayerLocation:
                    result = new PlayerLocation().ReadThis(br);
                    break;
                case GlobalDataType.TES:
                    result = new TES().ReadThis(br);
                    break;
                case GlobalDataType.GlobalVariables:
                    result = new GlobalVariables().ReadThis(br);
                    break;
                case GlobalDataType.CreatedObject:
                    result = new CreatedObjects().ReadThis(br);
                    break;
                case GlobalDataType.Effects:
                    result = new Effects().ReadThis(br);
                    break;
                case GlobalDataType.Weather:
                    result = new Weather().ReadThis(br);
                    break;
                case GlobalDataType.Audio:
                    result = new Audio().ReadThis(br);
                    break;
                case GlobalDataType.SkyCells:
                    result = new SkyCells().ReadThis(br);
                    break;
                case GlobalDataType.ProcessLists:
                    result = new ProcessLists().ReadThis(br);
                    break;
                case GlobalDataType.Combat:
                    result = new Combat().ReadThis(br);
                    break;
                case GlobalDataType.Interface:
                    result = new Interface().ReadThis(br);
                    break;
                case GlobalDataType.ActorCauses:
                    result = new ActorCauses().ReadThis(br);
                    break;
                case GlobalDataType.Unknown104:
                    throw new CorruptedSaveFileException(
                        "Your save file contains an unknown global data type. Type number 104 normally does not appear in save files. Please open an Issue on GitHub with your save file!",
                        br);
                case GlobalDataType.DetectionManager:
                    result = new DetectionManager().ReadThis(br);
                    break;
                case GlobalDataType.LocationMetaData:
                    result = new LocationMetaData().ReadThis(br);
                    break;
                case GlobalDataType.QuestStaticData:
                    result = new QuestStaticData().ReadThis(br);
                    break;
                case GlobalDataType.StoryTeller:
                    result = new StoryTeller().ReadThis(br);
                    break;
                case GlobalDataType.MagicFavorites:
                    result = new MagicFavorites().ReadThis(br);
                    break;
                case GlobalDataType.PlayerControls:
                    result = new PlayerControls().ReadThis(br);
                    break;
                case GlobalDataType.StoryEventManager:
                    result = new StoryEventManager().ReadThis(br);
                    break;
                case GlobalDataType.IngredientShared:
                    result = new IngredientShared().ReadThis(br);
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
