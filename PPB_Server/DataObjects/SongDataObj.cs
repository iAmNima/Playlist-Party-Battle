using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;

namespace PPB.DataObjects
{
    public class SongDataObj : DataObjSupport
    {

        public long SongID { get; set; }
        public long UserID { get; set; }
        public string Name { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public int InPlaylist { get; set; }

        public SongDataObj()
        {

            Name = string.Empty;

            Artist = string.Empty;

            Album = string.Empty;

            InPlaylist = 0;

        }
      

    }
}
