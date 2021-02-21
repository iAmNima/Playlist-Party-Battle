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
    public class TPlayer : EndPointSupport
    {
        public string RetValue { get; set; }

        public TPlayer(Dictionary<string, string> urlParams)
        {

            switch (urlParams["actionMethod"])
            {

                case "DelFromTournament":
                    RetValue = DelFromTournament(urlParams["data"]);
                    break;

                case "StartTournament":
                    RetValue = StartTournament(urlParams["data"]);
                    break;

                case "GetPlayers":
                    RetValue = GetPlayers(urlParams["data"]);
                    break;

                case "EntreTournament":
                    RetValue = EntreTournament(urlParams["data"]);
                    break;

                case "SetReadyToPlay":
                    RetValue = SetReadyToPlay(urlParams["data"]);
                    break;


                case "IsPlayerAlreadyIn":
                    RetValue = IsPlayerAlreadyIn(urlParams["data"]);
                    break;
            }

        }

        public static string DelFromTournament(string json_userID)

        {

            long userID = long.Parse(GetSingleValueFromJson(json_userID, "userID"));


            TPlayerDataObj tPlayer = new TPlayerDataObj();

            tPlayer.UserID = userID;

            new TPlayersReps().Delete(tPlayer);

            return string.Empty;

        }
        public static string StartTournament(string json_userID)//TODO no need parameter

        {
            //TODO  no need
            long userID = long.Parse(GetSingleValueFromJson(json_userID, "userID"));

            //TPlayerDataObj tPlayer = new TPlayerDataObj();

            //tPlayer.UserID = userID;

            //new TPlayersReps().StartToPlay(tPlayer);

            return TournamentManager.StartToPlay();



        }

        public static string GetPlayers(string json_userID)

        {
            long userID = long.Parse(GetSingleValueFromJson(json_userID, "userID"));

            TPlayerDataObj tPlayer = new TPlayerDataObj();

            tPlayer.UserID = userID;

            DataTable dtPlayers = new TPlayersReps().GetPlayers(tPlayer);

            return CnvDataTableToJsonString(dtPlayers);

        }

        public static string EntreTournament(string json_userID) //OK

        {

            string retValue = string.Empty;

            TPlayerDataObj tPlayer = new TPlayerDataObj();

            foreach (DataRow dr in jsonStringToTable(json_userID).Rows)
            {

                // New Player added to Table TournamentPlayers

                tPlayer.UserID = long.Parse(dr["userID"].ToString());

                new TPlayersReps().Insert(tPlayer);

                retValue = JSonUtil_Server.GetAsJson_Simple("userID", tPlayer.UserID.ToString());
            }

            return retValue;

        }

        public string SetReadyToPlay(string json_userID) //OK

        {

            long userID = long.Parse(JSonUtil_Server.GetSingleValueFromJson(json_userID, "userID"));

            TPlayerDataObj tPlayer = new TPlayerDataObj();

            tPlayer.UserID = userID;


            new TPlayersReps().SetReadyToPlay(tPlayer);

            return string.Empty;

        }

        public string IsPlayerAlreadyIn(string json_userID)

        {

            long userID = long.Parse(JSonUtil_Server.GetSingleValueFromJson(json_userID, "userID"));

            TPlayerDataObj tPlayer = new TPlayerDataObj();

            tPlayer.UserID = userID;


            string retValue = new TPlayersReps().IsPlayerAlreadyIn(tPlayer);

            return retValue;

        }
    }
}
