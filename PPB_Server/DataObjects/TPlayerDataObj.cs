using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;

namespace PPB.DataObjects
{
    public class TPlayerDataObj : DataObjSupport
    {

        public long UserID { get; set; }
        public int Ready { get; set; }

        public TPlayerDataObj()
        {

            Ready = 0;
        }
      

    }
}
