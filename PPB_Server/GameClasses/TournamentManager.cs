using Newtonsoft.Json;
using Npgsql;
using PPB.DatabaseRepositories;
using PPB.DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace PPB
{

    public static class TournamentManager
    {

        public static string StartToPlay()
        {

            if (BattleSupport.log != string.Empty)
                return BattleSupport.log;

            new UserReps().ResetAllAdmin();

            string log = " \r\n";

            string padLeft = "";

            TPlayerDataObj tPlayer = new TPlayerDataObj();

            tPlayer.UserID = 0;

            DataTable dtPlayers =  new TPlayersReps().GetPlayers(tPlayer);

            dtPlayers.Columns.Add("score", typeof(int));

            foreach (DataRow dr in dtPlayers.Rows)
            {
                dr["score"] = 0;
            }

            int cnt = dtPlayers.Rows.Count;

            for (int i = 1; i < cnt; i++)
            {
                for (int j = 1; i + j < cnt + 1; j++)
                {

                    int result = Fight(dtPlayers.Rows[i - 1]["set"].ToString(), dtPlayers.Rows[i - 1 + j]["set"].ToString());

                    if (result == 1)
                    {
                        dtPlayers.Rows[i - 1]["score"] = int.Parse(dtPlayers.Rows[i - 1]["score"].ToString()) + 1;

                        log += dtPlayers.Rows[i - 1]["username"].ToString() + " defeated " + dtPlayers.Rows[i - 1 + j]["username"].ToString() + " \r\n";
                    }
                    else if (result == 2)
                    {
                        dtPlayers.Rows[i - 1 + j]["score"] = int.Parse(dtPlayers.Rows[i - 1 + j]["score"].ToString()) + 1;

                        log += padLeft + padLeft + dtPlayers.Rows[i - 1 + j]["username"].ToString() + " defeated " + dtPlayers.Rows[i - 1 ]["username"].ToString() + " \r\n"; 
                    }
                    else // 0
                    {
                        log += padLeft + "draw between " + dtPlayers.Rows[i - 1 + j]["username"].ToString() + " and " + dtPlayers.Rows[i - 1]["username"].ToString() + " \r\n";
                    }
                }
            }

            

            int indexOfMaxScore = 0;


            int maxScore = int.Parse(dtPlayers.Rows[0]["score"].ToString());

            for (int i = 1; i < dtPlayers.Rows.Count; i++)

            {

                int nextScore = int.Parse(dtPlayers.Rows[i]["score"].ToString());

                if (nextScore > maxScore)
                {
                    maxScore = nextScore;

                    indexOfMaxScore = i;

                }

            }

            string winerUserID = dtPlayers.Rows[indexOfMaxScore]["userID"].ToString();

            UserDataObj user = new UserDataObj();

            user.UserID = long.Parse(winerUserID);

            new UserReps().SetAdminUpdatePoint(user, 5);

            log += Environment.NewLine;

            log += "---------------------------------------------------------------" + Environment.NewLine;

            log += padLeft + dtPlayers.Rows[indexOfMaxScore]["username"].ToString() + " has won the Tournament and is now the Administator" + Environment.NewLine;

            log += "---------------------------------------------------------------" + Environment.NewLine;

        //  Players.EmptyTournament();

            BattleSupport.log = log;

            return log;
        }

        public  static int Fight(string set1, string set2)
        {
            int retValue = 0;

            List<string> pSet1 = BattleSupport.MakeListFromString(set1);

            List<string> pSet2 = BattleSupport.MakeListFromString(set2);

            int scorep1 = 0;

            int scorep2 = 0;

            int result = 0;

            for (int i = 0; i < 5; i++)
            {

                result = CalculateRoundWinner(pSet1[i], pSet2[i]);

                if (result == 1)
                    scorep1++;
                else if (result == 2)
                    scorep2++;

            }

            if (scorep1 > scorep2)
                retValue = 1;
            else if (scorep2 > scorep1)
                retValue = 2;

            return retValue;

        }



        // private static int Fight ()

        public static int CalculateRoundWinner(string a, string b)
        {
            if (a == "R" && (b == "S" || b == "L")) return 1;
            else if (a == "R" && (b == "V" || b == "P")) return 2;
            else if (a == "P" && (b == "R" || b == "V")) return 1;
            else if (a == "P" && (b == "L" || b == "S")) return 2;
            else if (a == "S" && (b == "L" || b == "P")) return 1;
            else if (a == "S" && (b == "V" || b == "R")) return 2;
            else if (a == "L" && (b == "P" || b == "V")) return 1;
            else if (a == "L" && (b == "S" || b == "R")) return 2;
            else if (a == "V" && (b == "S" || b == "R")) return 1;
            else if (a == "V" && (b == "P" || b == "L")) return 2;
            else return 0;
        }

    }
}
