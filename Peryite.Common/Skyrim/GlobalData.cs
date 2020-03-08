using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Peryite.Common.Skyrim.GlobalDataTypes;

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

        IGlobalDataType ReadData(BinaryReader br);
    }

    public static partial class BinaryReaderExtensions
    {
        public static GlobalData ReadGlobalData([NotNull] this BinaryReader br, int typeStart, int typeEnd)
        {
            var res = new GlobalData
            {
                Type = (GlobalDataType)br.ReadUInt32(),
                Length = br.ReadUInt32()
            };

            if ((int)res.Type < typeStart || (int)res.Type > typeEnd)
                return null;

            res.Data = new IGlobalDataType[res.Length];

            for (var i = 0; i < res.Length; i++)
            {
                res.Data[i] = br.ReadIGlobalDataType(res.Type);
            }

            return res;
        }

        public static IGlobalDataType ReadIGlobalDataType([NotNull] this BinaryReader br, GlobalDataType type)
        {
            switch (type)
            {
                case GlobalDataType.MiscStats:
                    return new MiscStats().ReadData(br);
                case GlobalDataType.PlayerLocation:
                    break;
                case GlobalDataType.TES:
                    break;
                case GlobalDataType.GlobalVariable:
                    break;
                case GlobalDataType.CreatedObject:
                    break;
                case GlobalDataType.Effects:
                    break;
                case GlobalDataType.Weather:
                    break;
                case GlobalDataType.Audio:
                    break;
                case GlobalDataType.SkyCells:
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

            return null;
        }
    }
}
