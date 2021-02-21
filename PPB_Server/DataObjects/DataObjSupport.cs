using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPB.DataObjects
{
    public abstract class DataObjSupport
    {

        public DataObjSupport()
        {
        
        
        }


        public static DataTable jsonStringToTable(string jsonContent)
        {
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(jsonContent);
            return dt;
        }


        public static string DicToJson(Dictionary<string, string> dic)
        {
            var JSONString = new StringBuilder();


            JSONString.Append("[");
            JSONString.Append("{");

            foreach (var item in dic)
            {
                JSONString.Append("\"" + item.Key + "\":" + "\"" + item.Value + "\",");

            }
            JSONString.Append("},");

            JSONString.Append("]");

            return JSONString.ToString();
        }

        //public static List<string> MakeListFromString(string sourceString)
        //{
        //    var peIDsList = new List<string> { };
        //    string[] peIdArray = sourceString.Split(',');
        //    if (peIdArray.Length > 1)
        //        for (int i = 0; i <= peIdArray.Length - 1; i++)
        //        {

        //            peIDsList.Add(peIdArray[i]);
        //        }

        //    return peIDsList;
        //}

    }
}
