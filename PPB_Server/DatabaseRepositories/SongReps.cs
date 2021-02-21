using Npgsql;
using PPB.DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PPB.DatabaseRepositories
{
    public class SongReps : DBConnection 
    {


        public SongReps()
        {

            this.TblName = "Songs";

        }


        public  DataTable GetUserLibrary(UserDataObj user) //OK
        {

            var dataTable = new DataTable();

            string strCmd = string.Format("select * from  public.\"{0}\" where \"userID\" = {1} ", this.TblName, user.UserID.ToString());

            using (NpgsqlCommand cmd = new NpgsqlCommand(strCmd, GetConnection()))
            {

                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    dataTable.Load(reader);
                }


            }

            return dataTable;

        }
        public  DataTable GetPlayList() //OK
        {

            var dataTable = new DataTable();

            string strCmd = string.Format("select * from  public.\"{0}\" where \"inPlaylist\" = 1 " , this.TblName);

            using (NpgsqlCommand cmd = new NpgsqlCommand(strCmd, GetConnection()))
            {

                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    dataTable.Load(reader);
                }


            }

            return dataTable;

        }
      
        public  string PutInPlayList(SongDataObj song) //OK
        {
            string retValue = string.Empty;

            string strCmd = string.Format("Update  public.\"{0}\"  set \"inPlaylist\" = 1  where \"songID\" = {1}", this.TblName, song.SongID.ToString());

            using (NpgsqlCommand cmd = new NpgsqlCommand(strCmd, GetConnection()))
            {

                cmd.ExecuteNonQuery();
            }

            return retValue;

        }
        public  string Delete(SongDataObj song) //OK
        {
            string retValue = string.Empty;

            string strCmd = string.Format("Delete From public.\"{0}\"  where \"songID\" = {1}", this.TblName , song.SongID.ToString());

            using (NpgsqlCommand cmd = new NpgsqlCommand(strCmd, GetConnection()))
            {

                cmd.ExecuteNonQuery();
            }

            return retValue;

        }
        public  string RemoveSongFromPlayList(SongDataObj song) //OK
        {
            string retValue = string.Empty;

            string strCmd = string.Format("Update  public.\"{0}\"  set inPlayList = 0  where \"songID\" = {1}", this.TblName, song.SongID.ToString());

            using (NpgsqlCommand cmd = new NpgsqlCommand(strCmd, GetConnection()))
            {

                cmd.ExecuteNonQuery();
            }

            return retValue;

        }

        public SongDataObj Insert(SongDataObj song) //OK
        {

            string strCmd = string.Format("Insert into public.\"{0}\" (\"userID\", name, artist, album, \"inPlaylist\" ) values ( {1}, '{2}', '{3}', '{4}', '0') ", this.TblName, song.UserID, song.Name.Trim(), song.Artist.Trim(), song.Album.Trim());

            using (NpgsqlCommand cmd = new NpgsqlCommand(strCmd, GetConnection()))
            {
                cmd.ExecuteNonQuery();

                cmd.CommandText = "select max(\"songID\") from  public.\"Songs\" ";

                song.SongID = (long)cmd.ExecuteScalar();
            }

            return song;
        }

        public void Update(SongDataObj song) //OK
        {

            string strCmd = string.Format ("Update  public.\"{0}\"  set userID = @UserID, name=@name, artist=@artist, album=@album, inPlaylist=@inPlaylist where songID = @songID ", this.TblName);

            using (NpgsqlCommand cmd = new NpgsqlCommand(strCmd, GetConnection()))
            {

                //cmd.CommandText = "Update  public.\"Songs\"  set userID = @UserID, name=@name, artist=@artist, album=@album where songID = @songID ";

                cmd.Parameters.AddWithValue("@songID", song.SongID);

                cmd.Parameters.AddWithValue("@userID", song.UserID);

                cmd.Parameters.AddWithValue("@artist", song.Artist);

                cmd.Parameters.AddWithValue("@album", song.Album);

                cmd.Parameters.AddWithValue("@inPlaylist", song.InPlaylist );

                cmd.ExecuteNonQuery();

            }


        }

    }

}