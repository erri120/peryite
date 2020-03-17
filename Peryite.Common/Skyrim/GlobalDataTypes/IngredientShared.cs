namespace Peryite.Common.Skyrim.GlobalDataTypes
{
    public class IngredientShared : IGlobalData
    {
        public GlobalDataType Type => GlobalDataType.IngredientShared;

        private uint _count;
        
        [Read(1)]
        public uint Count
        {
            get => _count;
            set
            {
                _count = value;
                IngredientsCombinedArray = new IngredientsCombined[_count];
            }
        }
        
        [Read(1, IsCustomType = true)]
        public IngredientsCombined[]? IngredientsCombinedArray;

        public struct IngredientsCombined
        {
            [Read(1)]
            public RefID Ingredient0;
            [Read(2)]
            public RefID Ingredient1;
        }
    }
}
