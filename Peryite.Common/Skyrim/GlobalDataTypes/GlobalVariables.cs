namespace Peryite.Common.Skyrim.GlobalDataTypes
{
    public class GlobalVariables : IGlobalData
    {
        public GlobalDataType Type => GlobalDataType.GlobalVariables;

        private VSVAL _count;

        [Read(1)]
        public VSVAL Count
        {
            get => _count;
            set
            {
                _count = value;
                Globals = new GlobalVariable[_count.Value];
            }
        }

        [Read(2)]
        public GlobalVariable[]? Globals;

        public class GlobalVariable
        {
            [Read(1)]
            public RefID FormID;

            [Read(2)]
            public float Value;
        }
    }
}
