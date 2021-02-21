using PPB.DatabaseRepositories;
using PPB.DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPB.EndPoint_Handlers
{
    public class User : EndPointSupport
    {
        public string RetValue { get; set; }

        public User(Dictionary<string, string> urlParams)
        {

            switch (urlParams["actionMethod"])
            {

                case "SignIn":
                    RetValue = SignIn(urlParams["data"]);
                    break;

                case "SignUp":
                    RetValue = SignUp(urlParams["data"]);
                    break;

                case "EditProfile":
                    RetValue = EditProfile(urlParams["data"]);
                    break;

                case "DefineSet":
                    RetValue = DefineSet(urlParams["data"]);
                    break;

                case "GetUserRec":
                    RetValue = GetUserRec(urlParams["data"]);
                    break;

                case "IsUserAdmin":
                    RetValue = IsUserAdmin(urlParams["data"]);
                    break;

                case "GetAllScores":
                    RetValue = GetAllScores();
                    break;

            }

        }

        public string SignIn(string UserDataToSignIn) //OK

        {

            string retValue = string.Empty;


            foreach (DataRow dr in jsonStringToTable(UserDataToSignIn).Rows)
            {

                UserDataObj user = new UserDataObj();

                user.Username = dr["username"].ToString();

                user.Password = dr["password"].ToString();

                UserReps userRps = new UserReps();

                userRps.Athenticate(user);

                retValue = JSonUtil_Server.CnvDataTableToJsonString(userRps.Athenticate(user));


            }


            return retValue;

        }

        public static string SignUp(string UserDataToAdd) //OK

        {

            string retValue = string.Empty;

            foreach (DataRow dr in jsonStringToTable(UserDataToAdd).Rows)
            {

                // New User will be added to database

                UserDataObj user = new UserDataObj();

                user.Username = dr["username"].ToString();

                user.Password = dr["password"].ToString();

                user.Point = 100; //Default

                UserReps userRps = new UserReps();

                userRps.Insert(user);

                retValue = CnvDataTableToJsonString(userRps.Athenticate(user));

            }

            return retValue;

        }

        public static string EditProfile(string jSonUserRecToEditProfile) //OK

        {

            string retValue = string.Empty;

            foreach (DataRow dr in jsonStringToTable(jSonUserRecToEditProfile).Rows)
            {



                UserDataObj user = new UserDataObj();

                user.UserID = long.Parse(dr["userID"].ToString());

                user.Username = dr["username"].ToString();

                user.Password = dr["password"].ToString();

                UserReps userRps = new UserReps();

                userRps.Update(user);

         
                retValue = GetAsJson_Simple("userID", user.UserID.ToString());
            }


            return retValue;

        }
        // Define Set
        public static string DefineSet(string jSonUserRecToDefineSet) //OK

        {

            string retValue = string.Empty;

            UserDataObj user = new UserDataObj();

           

            foreach (DataRow dr in jsonStringToTable(jSonUserRecToDefineSet).Rows)
            {

                user.UserID = long.Parse(dr["userID"].ToString());

                user.Set  = dr["set"].ToString();

                retValue = new UserReps ().UpdateSet (user);

            }


            return retValue;

        }

        public static string GetUserRec(string json_userID) //OK

        {

            long userID = long.Parse(GetSingleValueFromJson(json_userID, "userID"));

            UserDataObj user = new UserDataObj();

            user.UserID = userID;

            DataTable dtPlayers = new UserReps().GetUserRec(user);

            return CnvDataTableToJsonString(dtPlayers);

        }

        public static string IsUserAdmin(string userID) //OK

        {

            UserDataObj user = new UserDataObj();

            user.UserID = long.Parse(userID);

            return new UserReps().IsUserAdmin(user); 

        }

        public static string GetAllScores() //OK

        {

            DataTable dtScors = new UserReps().GetScores();

            return CnvDataTableToJsonString(dtScors);

        }
    }
}
