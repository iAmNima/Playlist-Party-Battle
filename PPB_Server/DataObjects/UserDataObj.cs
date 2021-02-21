using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;

namespace PPB.DataObjects
{
    public class UserDataObj : DataObjSupport
    {
        public long UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Set { get; set; }
        public int Point { get; set; }
        public int IsAdmin { get; set; }

        public UserDataObj()
        { 
  
            
        }
        public void FillDataObj(string userDataAsJsonStr)
        {

            DataTable dt = JsonConvert.DeserializeObject<DataTable>(userDataAsJsonStr);

            foreach (DataRow dr in dt.Rows)
            {
                this.UserID = long.Parse(dr["userID"].ToString());

                this.Username = dr["username"].ToString();

                this.Password = dr["password"].ToString();

                this.Set = dr["set"].ToString();

                this.Point = int.Parse(dr["point"].ToString());

                this.IsAdmin = int.Parse(dr["isAdmin"].ToString());
                
            }

        }
        public string GetDataObjAsJsonStr()
        {

          

            Dictionary<string, string> userRecDic = new Dictionary<string, string>();

            userRecDic.Add("userID", this.UserID.ToString() );

            userRecDic.Add("username", this.Username);

            userRecDic.Add("password", this.Password);

            userRecDic.Add("point", this.Point.ToString().Trim());

            userRecDic.Add("isAdmin", this.IsAdmin.ToString());


            return DicToJson(userRecDic);

        }

    }
}
