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
        }
    }
}
