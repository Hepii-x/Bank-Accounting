using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Bank_Accounting
{
    class Database
    {
        public SQLiteConnection myConnection;

        public Database()
        {
            myConnection = new SQLiteConnection("Data Source=users.sqlite3");
            if (!File.Exists("./users.sqlite3"))
            {
                SQLiteConnection.CreateFile("users.sqlite3");
                Console.WriteLine("Database created!");
            }
            string query = "SELECT name FROM sqlite_master WHERE name = 'users'";
            SQLiteCommand cmd = new SQLiteCommand(query, myConnection);
            myConnection.Open();
            var test = cmd.ExecuteScalar();
            if (test == null)
            {
                Console.WriteLine("Database Table not found");
                Console.ReadLine();
                System.Environment.Exit(0);
            }

        }
    }
}
