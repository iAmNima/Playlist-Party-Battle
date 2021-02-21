using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;

namespace PPB_ClientTest
{
    class Program
    {
       

        static void Main(string[] args)
        {


            //----------------------------------------------
            while (true)
            {
                string jsonUserRec = string.Empty;

                string username = string.Empty;

                string password = string.Empty;


                ConsolePrint.PrintLoginMenu();

                string actionNo = Console.ReadLine();


                switch (actionNo)
                {
                    case "1":
                    case "2":
                        Console.Clear();

                        Console.Write("  Username: ");
                        username = Console.ReadLine();

                        Console.Write("  Password: ");
                        password = Console.ReadLine();

                        jsonUserRec = JSonUtil_Client.GetUserRecAsJson(username, password);

                        bool isAdmin = false;

                        string userID = string.Empty;

                        DataTable dtUserRec;

                        if (actionNo == "1") // sign in
                        {
                            string json_UserRec = BattleSupportClient.MakeHttpWebRequest_Post("User/SignIn", jsonUserRec);

                            dtUserRec = JSonUtil_Client.jsonStringToTable(json_UserRec);

                            userID = dtUserRec.Rows[0]["userID"].ToString();

                            if (dtUserRec.Rows[0]["isAdmin"].ToString().Trim() == "1")
                                isAdmin = true;
                           
                        }

                        else //sign up
                        {

                            string json_UserRec = BattleSupportClient.MakeHttpWebRequest_Post("User/SignUp", jsonUserRec);

                            dtUserRec = JSonUtil_Client.jsonStringToTable(json_UserRec);

                            userID = dtUserRec.Rows[0]["userID"].ToString();
                        }

                        string jsonUserID = JSonUtil_Client.GetAsJson_Simple("userID", userID);

                      

                        if (userID != "-1")
                        {

                            bool whileDecision = true;

                            while (whileDecision)
                            {
                                PrintPlayList(string.Empty);

                                Console.ForegroundColor = ConsoleColor.Yellow;

                                Console.WriteLine("  Welcome " + dtUserRec.Rows[0]["username"].ToString());

                                Console.ForegroundColor = ConsoleColor.White;

                                ConsolePrint.PrintDecisionMenu(isAdmin);

                                actionNo = Console.ReadLine();

                                switch (actionNo)
                                {
                                    case "1": // Manage Library

                                        bool whileManaging = true;

                                        while (whileManaging)
                                        {
                                            whileManaging = MngLibrary(userID);
                                        }

                                        break;
                                    case "2": // Edit Profile

                                        bool whileEditingProfile = true;

                                        while (whileEditingProfile)
                                        {
                                            Console.Clear();

                                            ConsolePrint.PrintEditProfileMenu();

                                            actionNo = Console.ReadLine();

                                            if (actionNo == "1") // New Username

                                            {
                                                Console.Write("  New Username: ");
                                                username = Console.ReadLine();

                                            }
                                            else if (actionNo == "2") // new Password

                                            {

                                                Console.Write("  New Password: ");
                                                password = Console.ReadLine();

                                            }
                                            else
                                                whileEditingProfile = false;


                                        }

                                        string strUserRecJson = JSonUtil_Client.GetUserRecAsJson_ToUpdate(userID, username, password);

                                        userID = BattleSupportClient.MakeHttpWebRequest("User/EditProfile", strUserRecJson);

                                        //Console.WriteLine("changes saved!");

                                        break;
                                    case "3":    // Define Set

                                        DefineSet(userID);


                                        break;

                                    case "4": // Enter Tournament


                                        string retJsonReadySetValue = BattleSupportClient.MakeHttpWebRequest("User/GetUserRec", jsonUserID);
                                        DataTable dtReadySet = JSonUtil_Client.jsonStringToTable(retJsonReadySetValue);

                                        List<string> set = BattleSupportClient.MakeListFromString(dtReadySet.Rows[0]["set"].ToString());

                                        if (set.Count < 5)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("  Please define Set first! ");
                                            Console.ForegroundColor = ConsoleColor.White;

                                            Console.WriteLine("  Press Entre to get back ");
                                            Console.ReadLine();


                                            break;
                                        }

                                        // doplicate player must be checked

                                        string retValue = BattleSupportClient.MakeHttpWebRequest("TPlayer/IsPlayerAlreadyIn", jsonUserID);

                                        if (retValue != "true")
                                            BattleSupportClient.MakeHttpWebRequest("TPlayer/EntreTournament", jsonUserID);

                                        string json_Players = BattleSupportClient.MakeHttpWebRequest("TPlayer/GetPlayers", jsonUserID);

                                        DataTable dtPlayers = JSonUtil_Client.jsonStringToTable(json_Players);

                                        ConsolePrint.PrintPlayers(dtPlayers);

                                        ConsolePrint.PrintEnterTournamentMenu();

                                        actionNo = Console.ReadLine();


                                        if (actionNo == "1") // Start Tournament

                                        {



                                            json_Players = BattleSupportClient.MakeHttpWebRequest("TPlayer/GetPlayers", jsonUserID);

                                            dtPlayers = JSonUtil_Client.jsonStringToTable(json_Players);


                                            if (dtPlayers.Rows.Count == 1)
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine(" Sorry ! Cannot start tournament with 1 player. ");
                                                Console.WriteLine(" Press Entre to get back ");
                                                Console.ReadLine();
                                                Console.ForegroundColor = ConsoleColor.White;
                                            }
                                            else
                                            {
                                                BattleSupportClient.MakeHttpWebRequest("TPlayer/SetReadyToPlay", jsonUserID);

                                                int readyToStart = 0;
                                                foreach (DataRow dr in dtPlayers.Rows)
                                                {

                                                    if (int.Parse(dr["ready"].ToString()) == 1)
                                                        readyToStart++;

                                                }
                                                if (dtPlayers.Rows.Count == readyToStart)
                                                {
                                                    retValue = BattleSupportClient.MakeHttpWebRequest("TPlayer/StartTournament", jsonUserID);

                                                    Console.Clear();

                                                    Console.ForegroundColor = ConsoleColor.Yellow;

                                                    Console.WriteLine("    " + retValue);

                                                    Console.ForegroundColor = ConsoleColor.White;
                                                }
                                                else
                                                {
                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                    Console.WriteLine(" Waiting for other players to ready . . . ");
                                                    Console.ForegroundColor = ConsoleColor.White;
                                                    bool allPlayersReady = false;
                                                    while (!allPlayersReady)
                                                    {

                                                        json_Players = BattleSupportClient.MakeHttpWebRequest("TPlayer/GetPlayers", jsonUserID);

                                                        dtPlayers = JSonUtil_Client.jsonStringToTable(json_Players);

                                                        readyToStart = 0;
                                                        foreach (DataRow dr in dtPlayers.Rows)
                                                        {

                                                            if (int.Parse(dr["ready"].ToString()) == 1)
                                                                readyToStart++;

                                                        }

                                                        if (dtPlayers.Rows.Count == readyToStart)
                                                        {
                                                            break;
                                                        }

                                                        Thread.Sleep(2000);
                                                    }

                                                    retValue = BattleSupportClient.MakeHttpWebRequest("TPlayer/StartTournament", jsonUserID);

                                                    
                                                    Console.Clear();

                                                    Console.ForegroundColor = ConsoleColor.Yellow;

                                                    Console.WriteLine("" + retValue);

                                                    Console.ForegroundColor = ConsoleColor.White;

                                                    Console.WriteLine("Tournament has finished. press Enter to continue.");

                                                    Console.ReadLine();


                                                }

                                                 retValue = BattleSupportClient.MakeHttpWebRequest_Post("User/IsUserAdmin", userID);

                                                isAdmin = retValue.Trim() == "1" ? true : false;
                                                

                                                BattleSupportClient.MakeHttpWebRequest("TPlayer/DelFromTournament", jsonUserID);

                                            }
                                        }





                                        break;

                                    case "5":
                                        DisplayScoreboard();
                                        break;
                                    case "6":
                                        if (isAdmin)
                                        {
                                            bool whileManagPlaylist = true;

                                            while (whileManagPlaylist)
                                            {
                                                whileManagPlaylist = MngPlaylist(userID);
                                            }

                                        }
                                        else
                                            Environment.Exit(0);

                                        break;

                                    case "7":

                                        Environment.Exit(0);

                                        break;
                                }
                            }

                        }



                        break;


                    default:
                        return;
                }
            }

        }

        private static void DisplayScoreboard()
        {
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.Write("  Wait a minute . . . ");

            string json_UserScores = BattleSupportClient.MakeHttpWebRequest_Post("User/GetAllScores", string.Empty);

            DataTable dtScoresPlayers = JSonUtil_Client.jsonStringToTable(json_UserScores);

            ConsolePrint.PrintPlayers(dtScoresPlayers);

            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("  Press Entre to get back ");

            Console.ReadLine();
        }

        private static string PrintPlayList(string userID)
        {

            string retValue = string.Empty;

            string json_PlayList = BattleSupportClient.MakeHttpWebRequest_Post("Song/GetPlayList", string.Empty);

            Console.WriteLine();
            Console.WriteLine();

            if (json_PlayList != string.Empty)
            {

                DataTable dtPlayList = JSonUtil_Client.jsonStringToTable(json_PlayList);

                ConsolePrint.PrintPlayList(dtPlayList);


            }
            else
                retValue = " Playlist Is Empty";

            return retValue;

        }

        private static bool MngPlaylist(string userID)
        {
            while (true)
            {
                //PrintPlayList(string.Empty);

                string json_Songs = BattleSupportClient.MakeHttpWebRequest("Song/GetUserLibrary", JSonUtil_Client.GetAsJson_Simple("userID", userID));

                if (json_Songs != string.Empty)
                {

                    DataTable dtMyGallary = JSonUtil_Client.jsonStringToTable(json_Songs);

                    ConsolePrint.PrintMyLibrary(dtMyGallary);

                    ConsolePrint.PrintManagePlaylistMenu();

                    string actionNo = Console.ReadLine();

                    if (actionNo == "1") //Add Song To Playlist
                    {

                        Console.Write("Enter SongID To Add : ");

                        string songID = Console.ReadLine();

                        BattleSupportClient.MakeHttpWebRequest_Post("Song/AddSongToPlayList", songID);

                        continue; ;
                    }
                    else if (actionNo == "2") //Remove Song To Playlist
                    {
                        Console.Write("Enter SongID To Remove : ");

                        string songID = Console.ReadLine();

                        BattleSupportClient.MakeHttpWebRequest_Post("Song/RemoveSongFromPlayList", songID);

                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }

            return false;

        }

        private static void DefineSet(string userID)
        {
            Console.WriteLine();
            Console.WriteLine("  Please define your set from (R, P, S, L, V  ) : ");
            Console.WriteLine(new string('-', 50));
            Console.WriteLine();

            Console.Write("  Element 1: ");
            string set = Console.ReadLine();

            Console.Write("  Element 2: ");
            set = set + "," + Console.ReadLine();

            Console.Write("  Element 3: ");
            set = set + "," + Console.ReadLine();

            Console.Write("  Element 4: ");
            set = set + "," + Console.ReadLine();

            Console.Write("  Element 5: ");
            set = set + "," + Console.ReadLine();

            Dictionary<string, string> userRecDic = new Dictionary<string, string>();

            userRecDic.Add("userID", userID);

            userRecDic.Add("set", set);

            string strUserRecJson = JSonUtil_Client.cnvDicToJsonString(userRecDic);

            userID = BattleSupportClient.MakeHttpWebRequest("User/DefineSet", strUserRecJson);

        }

        private static bool MngLibrary(string userID)
        {
            string json_Songs = BattleSupportClient.MakeHttpWebRequest("Song/GetUserLibrary", JSonUtil_Client.GetAsJson_Simple("userID", userID));

            if (json_Songs != string.Empty)
            {

                DataTable dtMyGallary = JSonUtil_Client.jsonStringToTable(json_Songs);

                ConsolePrint.PrintMyLibrary(dtMyGallary);


            }

            ConsolePrint.PrintEditSongMenu();

            string actionNo = Console.ReadLine();

            if (actionNo == "1") //Add Song

            {

                DataTable SongsDataTable = BuildSongDT(long.Parse(userID));

                string retValue = BattleSupportClient.MakeHttpWebRequest("Song/AddSong", JSonUtil_Client.CnvDataTableToJsonString(SongsDataTable));

                return true;

            }

            else if (actionNo == "2") //Delete Song
            {
                Console.Write("Enter SongID : ");

                string songID = Console.ReadLine();

                string retValue = BattleSupportClient.MakeHttpWebRequest("Song/DeleteSong", JSonUtil_Client.GetAsJson_Simple("songID", songID));

                return true;
            }

            else
                return false;
        }
        private static DataTable BuildSongDT(long userID)
        {
            var SongsDataTable = new DataTable();

            SongsDataTable.Columns.Add("UserID", typeof(long));

            SongsDataTable.Columns.Add("Name", typeof(string));

            SongsDataTable.Columns.Add("Artist", typeof(string));

            SongsDataTable.Columns.Add("Album", typeof(string));

            DataRow drow = SongsDataTable.NewRow();

            drow["UserID"] = userID;

            Console.Write("Song Name : ");
            drow["Name"] = Console.ReadLine();

            Console.Write("Artist Name : ");
            drow["Artist"] = Console.ReadLine();

            Console.Write("Album Name : ");
            drow["Album"] = Console.ReadLine();

            SongsDataTable.Rows.Add(drow);

            return SongsDataTable;
        }
    }
}
