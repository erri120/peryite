using System.IO;

namespace Peryite.Common.Skyrim.GlobalDataTypes
{
    public class CreatedObjects : IGlobalData
    {
        public GlobalDataType Type => GlobalDataType.CreatedObject;

        public VSVAL WeaponCount;
        public Enchantment[] WeaponEnchantments = default!;
        public VSVAL ArmorCount;
        public Enchantment[] ArmorEnchantments = default!;
        public VSVAL PotionCount;
        public Enchantment[] Potions = default!;
        public VSVAL PoisonCount;
        public Enchantment[] Poisons = default!;

        public IGlobalData ReadData(BinaryReader br)
        {
            WeaponCount = br.ReadVSVAL();
            WeaponEnchantments = new Enchantment[WeaponCount.Value];

            for (var i = 0; i < WeaponCount; i++)
            {
                WeaponEnchantments[i] = new Enchantment().ReadEnchantment(br);
            }

            ArmorCount = br.ReadVSVAL();
            ArmorEnchantments = new Enchantment[ArmorCount.Value];

            for (var i = 0; i < ArmorCount; i++)
            {
                ArmorEnchantments[i] = new Enchantment().ReadEnchantment(br);
            }

            PotionCount = br.ReadVSVAL();
            Potions = new Enchantment[PotionCount.Value];

            for (var i = 0; i < PotionCount; i++)
            {
                Potions[i] = new Enchantment().ReadEnchantment(br);
            }

            PoisonCount = br.ReadVSVAL();
            Poisons = new Enchantment[PoisonCount.Value];

            for (var i = 0; i < PoisonCount; i++)
            {
                Poisons[i] = new Enchantment().ReadEnchantment(br);
            }

            return this;
        }

        public class Enchantment
        {
            public RefID RefID;
            public uint TimesUsed;
            public VSVAL MagicEffectsCount;
            public MagicEffect[] Effects = default!;

            public Enchantment ReadEnchantment(BinaryReader br)
            {
                RefID = br.ReadRefID();
                TimesUsed = br.ReadUInt32();
                MagicEffectsCount = br.ReadVSVAL();

                Effects = new MagicEffect[MagicEffectsCount.Value];

                for (var i = 0; i < MagicEffectsCount; i++)
                {
                    Effects[i] = new MagicEffect().ReadMagicEffect(br);
                }

                return this;
            }
        }

        public class MagicEffect
        {
            public RefID EffectID;
            public EnchantmentInfo Info;
            public float Price;

            public MagicEffect ReadMagicEffect(BinaryReader br)
            {
                EffectID = br.ReadRefID();

                Info = new EnchantmentInfo
                {
                    Magnitude = br.ReadSingle(),
                    Duration = br.ReadUInt32(),
                    Area = br.ReadUInt32()
                };

                Price = br.ReadSingle();

                return this;
            }
        }

        public struct EnchantmentInfo
        {
            public float Magnitude;
            public uint Duration;
            public uint Area;
        }
    }
}
