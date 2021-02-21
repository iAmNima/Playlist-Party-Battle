using Npgsql;
using PPB.DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PPB.DatabaseRepositories
{
    public class TPlayersReps : DBConnection 
    {


        public TPlayersReps()
        {
            this.TblName = "TournamentPlayers";

        }


        public  DataTable GetPlayers(TPlayerDataObj TPlayer) //OK
        {

            var dataTable = new DataTable();

            string strCmd = "select * from  public.\"tournamentplayersvw\"  ";

            using (NpgsqlCommand cmd = new NpgsqlCommand(strCmd, GetConnection()))
            {

                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    dataTable.Load(reader);
                }
                //TODO
                if (TPlayer.UserID  != 0)
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (dr["userID"].ToString().Trim() != TPlayer.UserID.ToString().Trim())
                            dr["set"] = "*****";

                    }
            }

            return dataTable;

        }

        // Set Ready To Play 
        public  void SetReadyToPlay(TPlayerDataObj TPlayer)//OK
        {


            string strCmd = string.Format("Update  public.\"{0}\"  set  \"ready\" = 1  where \"userID\" = {1}", this.TblName, TPlayer.UserID.ToString());

            using (NpgsqlCommand cmd = new NpgsqlCommand(strCmd, GetConnection()))
            {

                cmd.ExecuteNonQuery();

            }

        }

        public  string IsPlayerAlreadyIn(TPlayerDataObj TPlayer)//OK
        {

            string retVaue = "false";


            string strCmd = string.Format("select count(*) from  public.\"{0}\" where \"userID\" = {1} ", this.TblName, TPlayer.UserID.ToString());

            using (NpgsqlCommand cmd = new NpgsqlCommand(strCmd, GetConnection()))
            {

                int cnt = int.Parse(cmd.ExecuteScalar().ToString());
                if (cnt > 0)
                    retVaue = "true";

            }

            return retVaue;

        }

        public void Insert(TPlayerDataObj TPlayer)//OK
        {


            string strCmd = string.Format("Insert into public.\"{0}\" (\"userID\", ready ) values ( {1}, '{2}') ", this.TblName,TPlayer.UserID, TPlayer.Ready);

            using (NpgsqlCommand cmd = new NpgsqlCommand(strCmd, GetConnection()))
            {
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {


                }

            }


        }

        public  string Delete(TPlayerDataObj TPlayer)//OK
        {
            string retValue = string.Empty;

            string strCmd = string.Format("Delete From public.\"{0}\"  where \"userID\" = {1}", this.TblName,TPlayer.UserID.ToString());

            using (NpgsqlCommand cmd = new NpgsqlCommand(strCmd, GetConnection()))
            {

                cmd.ExecuteNonQuery();
            }

            return retValue;

        }
        public  string EmptyTournament()//OK
        {
            string retValue = string.Empty;

            string strCmd = string.Format("Delete From public.\"{0}\" ", this.TblName);

            using (NpgsqlCommand cmd = new NpgsqlCommand(strCmd, GetConnection()))
            {

                cmd.ExecuteNonQuery();
            }

            return retValue;

        }


    }

}