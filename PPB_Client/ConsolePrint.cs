using System;
using System.Collections.Generic;
using System.Data;

namespace PPB_ClientTest
{

    public static class ConsolePrint
    {

        public static void PrintLoginMenu()
        {

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow  ;

            Console.WriteLine("  Menu :");
            Console.WriteLine("---------------");
            Console.ForegroundColor = ConsoleColor.White ;
            Console.WriteLine("   1 ) sign in ");
            Console.WriteLine("   2 ) sign up ");
            Console.WriteLine("   3 ) quit ");
            Console.Write("\r\nSelect an option: ");

        }

        public static void PrintDecisionMenu(bool isAdmin)
        {

           // Console.Clear();
            Console.WriteLine("  Menu :");
            Console.WriteLine("  ---------------");
            Console.WriteLine("   1 ) Manage Library ");
            Console.WriteLine("   2 ) Edit Profile ");
            Console.WriteLine("   3 ) Define Set ");
            Console.WriteLine("   4 ) Enter Tournament  ");
            Console.WriteLine("   5 ) Scoreboard  ");
            if (isAdmin)
            {
                Console.WriteLine("   6 ) Manage Playlist  ");
                Console.WriteLine("   7) Quit     ");
            }
            else
            {
                Console.WriteLine("   6 ) Quit     ");
            }
   
            Console.Write("\r\nSelect an option: ");

        }


        public static void PrintEditProfileMenu()
        {
            Console.WriteLine("  Menu :");
            Console.WriteLine("  ---------------");
            Console.WriteLine("   1 ) New Username ");
            Console.WriteLine("   2 ) New Password ");
            Console.WriteLine("   3 ) Back ");
            Console.Write("\r\nSelect an option: ");

        }

        public static void PrintEditSongMenu()
        {

           
            Console.WriteLine("  Menu :");
            Console.WriteLine("  ---------------");
            Console.WriteLine("   1 ) Add Song ");
            Console.WriteLine("   2 ) Delete Song ");
            Console.WriteLine("   3 ) Back  ");
            Console.Write("\r\nSelect an option: ");

        }

        public static void PrintManagePlaylistMenu()
        {

            Console.WriteLine("  Menu :");
            Console.WriteLine("  ---------------");
            Console.WriteLine("   1 ) Add Song To Playlist ");
            Console.WriteLine("   2 ) Remove Song  From Playlist ");
            Console.WriteLine("   3 ) Back  ");
            Console.Write("\r\nSelect an option: ");

        }

        public static void PrintEnterTournamentMenu()
        {

            Console.WriteLine("  ---------------");
            Console.WriteLine("   1 ) Start Tournament ");
            Console.WriteLine("   2 ) Back  ");
            Console.Write("\r\nSelect an option: ");

        }

        public static void PrintPlayers(DataTable dtPlayers)
        {
            Console.Clear();

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine(AlignCentre("  List of Players in Tournament", 50));


            PrintLine(51);

            List<string> columns = new List<string>();

            foreach (DataColumn dc in dtPlayers.Columns)
            {
                columns.Add(dc.Caption);

            }

            PrintRow(columns, 50);

            PrintLine(51);

            Console.ForegroundColor = ConsoleColor.White;

            foreach (DataRow dr in dtPlayers.Rows)
            {
                columns.Clear();

                foreach (DataColumn dc in dtPlayers.Columns)
                {
                    columns.Add(dr[dc.ColumnName].ToString());

                }
                PrintRow(columns, 50);
            }

            PrintLine(51);

            Console.WriteLine();
        }

        public static void PrintPlayList(DataTable dtPlayList)
        {
            Console.Clear();

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine(AlignCentre("Playlist", 67));

            PrintLine(67);

            List<string> columns = new List<string>();

            foreach (DataColumn dc in dtPlayList.Columns)
            {
                columns.Add(dc.Caption);

            }

            PrintRow(columns, 67);

            PrintLine(67);

            Console.ForegroundColor = ConsoleColor.White;

            foreach (DataRow dr in dtPlayList.Rows)
            {
                columns.Clear();

                foreach (DataColumn dc in dtPlayList.Columns)
                {
                    columns.Add(dr[dc.ColumnName].ToString().Trim());

                }
                PrintRow(columns, 67);
            }

            PrintLine(67);

            Console.WriteLine();
        }

        public static void PrintMyLibrary(DataTable dtMyGallary)
        {
            Console.Clear();

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine(AlignCentre("List Of Songs In Your Library", 70));

            PrintLine(67);

            List<string> columns = new List<string>();

            foreach (DataColumn dc in dtMyGallary.Columns)
            {
                columns.Add(dc.Caption);

            }

            PrintRow(columns, 67);

            PrintLine(67);

            Console.ForegroundColor = ConsoleColor.White;

            foreach (DataRow dr in dtMyGallary.Rows)
            {
                columns.Clear();

                foreach (DataColumn  dc in dtMyGallary.Columns)
                {
                    columns.Add(dr[dc.ColumnName ].ToString());
                 
                }
                PrintRow(columns, 67);
            }

            PrintLine(67);

            Console.WriteLine();
        }

        static void PrintLine(int width)
        {
            Console.WriteLine("  " + new string('-', width));
        }

        static void PrintRow(List<string> columns, int tableWidth)
        {
            int width = (tableWidth - columns.Count ) / columns.Count;

            string row = "|";

            foreach (string column in columns)
            {
                row += AlignCentre(column, width) + "|";
            }

            Console.WriteLine("  " +row);
        }
         
        static string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }
    }
}
