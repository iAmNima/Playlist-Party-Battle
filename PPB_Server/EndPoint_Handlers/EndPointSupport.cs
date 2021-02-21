using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPB.EndPoint_Handlers
{
    public class EndPointSupport
    {

        public EndPointSupport()
        {
        
        
        }


        public static DataTable jsonStringToTable(string jsonContent)
        {
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(jsonContent);
            return dt;
        }

        public static string CnvDataTableToJsonString(DataTable table)
        {
            var JSONString = new StringBuilder();
            if (table.Rows.Count > 0)
            {
                JSONString.Append("[");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    JSONString.Append("{");
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        if (j < table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\",");
                        }
                        else if (j == table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\"");
                        }
                    }
                    if (i == table.Rows.Count - 1)
                    {
                        JSONString.Append("}");
                    }
                    else
                    {
                        JSONString.Append("},");
                    }
                }
                JSONString.Append("]");
            }
            return JSONString.ToString();
        }

        public static string GetAsJson_Simple(string name, string value)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add(name, value);

            return cnvToJsonString(dic);

        }

        public static string cnvToJsonString(Dictionary<string, string> dic)
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


        public static string GetSingleValueFromJson(string jsonStr, string fieldName)
        {

            return jsonStringToTable(jsonStr).Rows[0][fieldName].ToString();

        }
    }
}
