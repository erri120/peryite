using System.IO;

namespace Peryite.Common.Skyrim.GlobalDataTypes
{
    public class Audio : IGlobalData
    {
        public GlobalDataType Type => GlobalDataType.Audio;

        public RefID Unknown;
        public VSVAL TracksCount;

        /// <summary>
        /// Contains music tracks playing at time of saving
        /// </summary>
        public RefID[] Tracks = default!;

        /// <summary>
        /// The background music at time of saving
        /// </summary>
        public RefID BGM;

        public IGlobalData ReadData(BinaryReader br)
        {
            Unknown = br.ReadRefID();
            TracksCount = br.ReadVSVAL();

            Tracks = new RefID[TracksCount.Value];

            for (var i = 0; i < TracksCount; i++)
            {
                Tracks[i] = br.ReadRefID();
            }

            BGM = br.ReadRefID();

            return this;
        }
    }
}
