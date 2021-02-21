using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace PPB_ClientTest
{

    public static class BattleSupportClient
    {


        public static string MakeHttpWebRequest(string actionType, string arg)
        {

            string url1 = string.Format("http://localhost:8080/{0}/{1}", actionType, arg);

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url1);

            httpWebRequest.Method = "Get";

            httpWebRequest.ContentType = "application/json";

            httpWebRequest.Headers.Add("Authorization", new Settings_Client().Token);


            string strResponseValue = string.Empty;


            using (HttpWebResponse response1 = (HttpWebResponse)httpWebRequest.GetResponse())
            {
                using (Stream responseStream = response1.GetResponseStream())
                {

                    using (StreamReader reader = new StreamReader(responseStream))
                    {

                        if (actionType == "TPlayer/StartTournament")
                            strResponseValue = reader.ReadToEnd(); // will not remove return/newline  code
                        else
                            strResponseValue = reader.ReadToEnd().Replace(System.Environment.NewLine, string.Empty);


                    }
                }

            }

            return strResponseValue;

        }

        public static string MakeHttpWebRequest_Post(string actionType, string content)
        {

            string url1 = string.Format("http://localhost:8080/{0}", actionType);

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url1);

            httpWebRequest.Method = "POST";

            httpWebRequest.ContentType = "application/json";

            httpWebRequest.ContentLength = content.Length;

            httpWebRequest.Headers.Add("Authorization", new Settings_Client().Token);

            var data = Encoding.ASCII.GetBytes(content);

            using (var stream = httpWebRequest.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            string strResponseValue = string.Empty;


            using (HttpWebResponse response1 = (HttpWebResponse)httpWebRequest.GetResponse())
            {
                using (Stream responseStream = response1.GetResponseStream())
                {

                    using (StreamReader reader = new StreamReader(responseStream))
                    {

                        strResponseValue = reader.ReadToEnd().Replace(System.Environment.NewLine, string.Empty);
                    }
                }

            }

            return strResponseValue;

        }

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
