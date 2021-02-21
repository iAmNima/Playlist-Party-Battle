using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace PPB
{

    public static class BattleSupport
    {
        public static string log = string.Empty;

        public static List<string> MakeListFromString(string sourceString)
        {
            var peIDsList = new List<string> { };
            string[] peIdArray = sourceString.Split(',');
            if (peIdArray.Length > 1)
                for (int i = 0; i <= peIdArray.Length - 1; i++)
                {

                    peIDsList.Add(peIdArray[i]);
                }

            return peIDsList;
        }


    }
}
