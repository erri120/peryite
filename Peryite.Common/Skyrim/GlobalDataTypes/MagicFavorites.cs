namespace Peryite.Common.Skyrim.GlobalDataTypes
{
    public class MagicFavorites : IGlobalData
    {
        public GlobalDataType Type => GlobalDataType.MagicFavorites;

        private VSVAL _count0;

        [Read(1)]
        public VSVAL Count0
        {
            get => _count0;
            set
            {
                _count0 = value;
                FavoriteMagic = new RefID[_count0.Value];
            }
        }
        
        [Read(2)]
        public RefID[]? FavoriteMagic;

        private VSVAL _count1;
        
        [Read(3)]
        public VSVAL Count1
        {
            get => _count1;
            set
            {
                _count1 = value;
                MagicHotKeys = new RefID[_count1.Value];
            }
        }
        
        [Read(4)]
        public RefID[]? MagicHotKeys;
    }
}
