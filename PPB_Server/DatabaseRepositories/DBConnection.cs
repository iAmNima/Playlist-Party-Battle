using Npgsql;

namespace PPB.DatabaseRepositories
{

    public abstract  class DBConnection
    {
       
        public string TblName { get; set; }

        public static NpgsqlConnection NpgsqlConn { get; set; }
        public static NpgsqlConnection GetConnection()
        {

            if (NpgsqlConn == null)
            {

                Settings setting = new Settings();

                string strConn = string.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};", setting.Hostname, setting.Port, setting.UserID, setting.Password, setting.DbName);


                NpgsqlConn = new NpgsqlConnection(strConn);

                NpgsqlConn.Open();

            }


            return NpgsqlConn;
        }


    }
}
