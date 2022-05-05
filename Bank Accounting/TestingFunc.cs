using System;
using System.Data;
using System.Data.SQLite;

namespace Bank_Accounting
{
    internal class DBFunctions
    {
        Database db = new Database();

        public void InsertDB()
        {
            Console.WriteLine("Working DB");
            string query = "INSERT INTO users (name, surname, card_number, card_cvc, card_pin, money) VALUES ('Maciej', 'Szustakowski', '1111222233334444', '123', '1234', '543098123.09')";
            SQLiteCommand cmd = new SQLiteCommand(query, db.myConnection);
            cmd.ExecuteNonQuery();
            Console.WriteLine("Executed");
        }

        public void GetFromDB()
        {
            string query = "SELECT name, surname, card_number FROM users WHERE card_number = '1111222233334444'";
            SQLiteCommand cmd = new SQLiteCommand(query, db.myConnection);
            SQLiteDataReader dataX = cmd.ExecuteReader();
            dataX.Read();
            Console.WriteLine(dataX["name"] + " " + dataX["surname"] + " " + dataX["card_number"]);

        }
    }
}