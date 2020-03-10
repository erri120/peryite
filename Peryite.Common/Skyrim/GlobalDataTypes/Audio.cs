namespace Peryite.Common.Skyrim.GlobalDataTypes
{
    public class Audio : IGlobalData
    {
        public GlobalDataType Type => GlobalDataType.Audio;

        [Read(1)]
        public RefID Unknown;

        private VSVAL _tracksCount;

        [Read(2)]
        public VSVAL TracksCount
        {
            get => _tracksCount;
            set
            {
                _tracksCount = value;
                Tracks = new RefID[_tracksCount.Value];
            }
        }

        /// <summary>
        /// Contains music tracks playing at time of saving
        /// </summary>
        [Read(3)]
        public RefID[]? Tracks;

        /// <summary>
        /// The background music at time of saving
        /// </summary>
        [Read(4)]
        public RefID BGM;
    }
}
