using PPB.DatabaseRepositories;
using PPB.DataObjects;
using System.Collections.Generic;
using System.Data;

namespace PPB.EndPoint_Handlers
{
    public class Song : EndPointSupport
    {
        public string RetValue { get; set; }
        public Song(Dictionary<string, string> urlParams)
        {

            switch (urlParams["actionMethod"])
            {

                case "GetPlayList":
                    RetValue = GetPlayList();
                    break;

                case "GetUserLibrary":
                    RetValue = GetUserLibrary(urlParams["data"]);
                    break;

                case "AddSong":
                    RetValue = AddSong(urlParams["data"]);
                    break;

                case "DeleteSong":
                    RetValue = DeleteSong(urlParams["data"]);
                    break;

                case "AddSongToPlayList":
                    RetValue = AddSongToPlayList(urlParams["data"]);
                    break;

                case "RemoveSongFromPlayList":
                    RetValue = RemoveSongFromPlayList(urlParams["data"]);
                    break;



            }

        }
        public static string GetUserLibrary(string json_userID) //OK

        {

            long userID = long.Parse(GetSingleValueFromJson(json_userID, "userID"));

            UserDataObj user = new UserDataObj();

            user.UserID = userID;

            DataTable dtSongs = new SongReps().GetUserLibrary(user);

            return CnvDataTableToJsonString(dtSongs);

        }
        public static string GetPlayList() //OK

        {

            DataTable dtPlayList = new SongReps().GetPlayList();

            return CnvDataTableToJsonString(dtPlayList);

        }
        public static string AddSong(string json_SongRec) //OK

        {
            DataRow dr = jsonStringToTable(json_SongRec).Rows[0];

            SongDataObj song = new SongDataObj();

            song.UserID = long.Parse(dr["userID"].ToString());

            song.Name = dr["Name"].ToString();

            song.Artist = dr["Artist"].ToString();

            song.Album = dr["Album"].ToString();

            new SongReps ().Insert (song);

            return "The Song added successfully";

        }
        public static string DeleteSong(string json_songID) //OK

        {
            long songID = long.Parse(GetSingleValueFromJson(json_songID, "songID"));

            SongDataObj song = new SongDataObj();

            song.SongID = songID;

            new SongReps().Delete(song);

           // PPB.GameClasses.Song.Delete(songID);

            return "The Song seleted successfully";

        }
        public static string AddSongToPlayList(string songID) //OK

        {

            SongDataObj song = new SongDataObj();

            song.SongID = long.Parse(songID);

            new SongReps().PutInPlayList(song);

           // PPB.GameClasses.Song.PutInPlayList(long.Parse(songID));

            return "The Song added successfully";

        }
        public static string RemoveSongFromPlayList(string songID)//OK

        {
            SongDataObj song = new SongDataObj();

            song.SongID = long.Parse(songID);

            new SongReps().RemoveSongFromPlayList(song);

           // PPB.GameClasses.Song.RemoveSongFromPlayList(long.Parse(songID));

            return "The Song removed successfully";

        }

    }
}
