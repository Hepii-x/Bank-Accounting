using System;
using System.Data.SQLite;
using System.IO;

namespace Bank_Accounting
{
    internal class Database
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
                Console.WriteLine("Database table not found");
                query = "CREATE TABLE users (id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT, surname TEXT, card_number TEXT UNIQUE, card_cvc TEXT, card_pin TEXT, money REAL)";
                cmd = new SQLiteCommand(query, myConnection);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Created database table");
            }
        }
    }
}
