namespace PPB
{

    public  class Settings
    {
        public string DbName { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
        public string Hostname { get; set; }
        public string Port { get; set; }
        public int PortToListen { get; set; }

        public string Token { get; set; } 

        public  Settings()       
        {
            Hostname = "localhost";

            Port = "5432";

            DbName = "partyBattle";

            UserID = "postgres";

            Password = "213213213";

            PortToListen = 8080;

            Token = "sUZaHFWuCu1ekyBnnd46Lw==";
        }

        //// database access
        //public const string HOST        = "localhost";
        //public const string PORT        = "5432";
        //public const string DATABASE    = "MCTG";
        //public const string USERNAME    = "MCTGAdmin";
        //public const string PASSWORD    = "123";

        //public const int LISTENINGPORT  = 10002;
        //public const string PROTOCOL    = "HTTP/1.1";
        //public const string HOMEPAGE    = "../../../../home.txt";


        //// game
        //public const int COINS          = 20;
        //public const int PACKAGECOST    = 5;
        //public const int PACKAGESIZE    = 5;
        //public const int DECKSIZE       = 4;

        //public const int POINTS         = 100;
        //public const int POINTSPERLOSE  = -3;
        //public const int POINTSPERWIN   = 5;
    }
}
