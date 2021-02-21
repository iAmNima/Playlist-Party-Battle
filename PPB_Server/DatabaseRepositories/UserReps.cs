using Npgsql;
using PPB.DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PPB.DatabaseRepositories
{
    public class UserReps : DBConnection 
    {


        public UserReps()
        {

            this.TblName = "Users";

        }

        public DataTable Athenticate(UserDataObj user)
        {
           

            var dataTable = new DataTable();

            string strCmd = string.Format("select * from  public.\"{0}\" where ltrim(rtrim(username)) = '{1}' and  ltrim(rtrim(password)) = '{2}' ", TblName, user.Username.Trim(), user.Password.Trim());

            using (NpgsqlCommand cmd = new NpgsqlCommand(strCmd, GetConnection()))
            {

                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    dataTable.Load(reader);

                }


            }

            return dataTable;
        }

        //// Update Point
        //public void UpdatePoint(string userID, int pointToAdd)
        //{


        //    string strCmd = string.Format("Update  public.\"{0}\"  set  \"point\" = \"point\" + {1}  where \"userID\" = {2}", TblName,  pointToAdd.ToString(), userID);

        //    using (NpgsqlCommand cmd = new NpgsqlCommand(strCmd, GetConnection()))
        //    {

        //        cmd.ExecuteNonQuery();

        //    }

        //}

        // Set Admin and add Point
        public  void SetAdminUpdatePoint(UserDataObj user, int pointToAdd)
        {

            string strCmd = string.Format("Update  public.\"{0}\"  set  \"isAdmin\" = 1, \"point\" = \"point\" + {1}   where \"userID\" = {2}", TblName, pointToAdd.ToString(), user.UserID.ToString());

            using (NpgsqlCommand cmd = new NpgsqlCommand(strCmd, GetConnection()))
            {

                cmd.ExecuteNonQuery();

            }

        }

        // Reset  admin 
        public  void ResetAllAdmin()
        {


            string strCmd = string.Format ("Update  public.\"{0}\"  set  \"isAdmin\" = 0 ", TblName);

            using (NpgsqlCommand cmd = new NpgsqlCommand(strCmd, GetConnection()))
            {

                cmd.ExecuteNonQuery();

            }

        }


        public  DataTable GetScores() //OK
        {

            var dataTable = new DataTable();

            string strCmd = string.Format("select username, point from  public.\"{0}\"  ", TblName); ;

            using (NpgsqlCommand cmd = new NpgsqlCommand(strCmd, GetConnection()))
            {

                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    dataTable.Load(reader);
                }

            }

            return dataTable;

        }

        public string IsUserAdmin(UserDataObj user) //OK
        {

            string retVaue = "false";


            string strCmd = string.Format("select \"isAdmin\"  from  public.\"{0}\" where \"userID\" = {1} ", TblName, user.UserID.ToString());

            using (NpgsqlCommand cmd = new NpgsqlCommand(strCmd, GetConnection()))
            {

                retVaue = cmd.ExecuteScalar().ToString().Trim();

            }

            return retVaue;

        }

        public DataTable GetUserRec(UserDataObj user) //OK
        {


            var dataTable = new DataTable();

            string strCmd = string.Format("select * from  public.\"{0}\" where \"userID\" = {1} ", TblName, user.UserID.ToString());

            using (NpgsqlCommand cmd = new NpgsqlCommand(strCmd, GetConnection()))
            {

                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    dataTable.Load(reader);
                }


            }

            return dataTable;

        }

        // Define Set / Update Set
        public string UpdateSet(UserDataObj user) //OK
        {

            string retValue = " Your Set added succesfully";

           try
            {
                string strCmd = string.Format("Update  public.\"{0}\"   set set = '{1}'  where \"userID\" = {2}", TblName, user.Set.Trim(), user.UserID.ToString());

                using (NpgsqlCommand cmd = new NpgsqlCommand(strCmd, GetConnection()))
                {

                    cmd.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {

                retValue = ex.Message ;
            }

            return retValue;
        }

        public UserDataObj Insert(UserDataObj user) //OK
        {

            string strCmd = string.Format("Insert into public.\"{0}\" (username, password, set, point, \"isAdmin\") values ( '{1}', '{2}', '{3}', {4}, '0') ", TblName, user.Username.Trim(), user.Password.Trim(), user.Set, user.Point);

            using (NpgsqlCommand cmd = new NpgsqlCommand(strCmd, GetConnection()))
            {

                cmd.ExecuteNonQuery();

                cmd.CommandText = string.Format("select max(\"userID\") from  public.\"{0}\" ", TblName );

                user.UserID  = (long)cmd.ExecuteScalar();

            }

            return user;

        }

        public void Update(UserDataObj user) //OK
        {

            //string strCmd = "Update  public.\"Users\"  set username = @username, name=@name, password=@password where  \"userID\" = @userID";

            string strCmd = string.Format("Update  public.\"{0}\"   set username = '{1}', password = '{2}'  where \"userID\" = {2}", TblName, user.Username.Trim(), user.Password.Trim(), user.UserID.ToString());

            using (NpgsqlCommand cmd = new NpgsqlCommand(strCmd, GetConnection()))
            {

                //cmd.Parameters.AddWithValue("@userID", this.UserID);

                //cmd.Parameters.AddWithValue("@username", this.Username );

                //cmd.Parameters.AddWithValue("@password", this.Password );


                cmd.ExecuteNonQuery();

            }


        }
    }

}