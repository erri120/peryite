namespace Peryite.Common.Skyrim.GlobalDataTypes
{
    public class CreatedObjects : IGlobalData
    {
        public GlobalDataType Type => GlobalDataType.CreatedObject;

        private VSVAL _weaponCount;

        [Read(1)]
        public VSVAL WeaponCount
        {
            get => _weaponCount;
            set
            {
                _weaponCount = value;
                WeaponEnchantments = new Enchantment[_weaponCount.Value];
            }
        }
        [Read(2)]
        public Enchantment[]? WeaponEnchantments;

        private VSVAL _armorCount;

        [Read(3)]
        public VSVAL ArmorCount
        {
            get => _armorCount;
            set
            {
                _armorCount = value;
                ArmorEnchantments = new Enchantment[_armorCount.Value];
            }
        }
        [Read(4)]
        public Enchantment[]? ArmorEnchantments;

        private VSVAL _potionCount;

        [Read(5)]
        public VSVAL PotionCount
        {
            get => _potionCount;
            set
            {
                _potionCount = value;
                Potions = new Enchantment[_potionCount.Value];
            }
        }
        [Read(6)]
        public Enchantment[]? Potions;

        private VSVAL _poisonCount;

        [Read(7)]
        public VSVAL PoisonCount
        {
            get => _poisonCount;
            set
            {
                _poisonCount = value;
                Poisons = new Enchantment[_poisonCount.Value];
            }
        }
        [Read(8)]
        public Enchantment[]? Poisons;

        public class Enchantment
        {
            [Read(1)]
            public RefID RefID;
            [Read(2)]
            public uint TimesUsed;

            private VSVAL _magicEffectsCount;

            [Read(3)]
            public VSVAL MagicEffectsCount
            {
                get => _magicEffectsCount;
                set
                {
                    _magicEffectsCount = value;
                    Effects = new MagicEffect[_magicEffectsCount.Value];
                }
            }
            [Read(4)]
            public MagicEffect[]? Effects;
        }

        public class MagicEffect
        {
            [Read(1)]
            public RefID EffectID;

            [Read(2)]
            public EnchantmentInfo Info;

            [Read(3)]
            public float Price;
        }

        public struct EnchantmentInfo
        {
            [Read(1)]
            public float Magnitude;

            [Read(2)]
            public uint Duration;
            
            [Read(3)]
            public uint Area;
        }
    }
}
