using System;
using System.Data;
using System.Data.SQLite;

namespace Bank_Accounting
{
    internal class DBFunctions
    {
        Database db = new Database();

        public void Register(string name, string surname, float money)
        {
            Start:
            var rand = new Random();
            string cardNumber1 = rand.Next(10000000, 99999999).ToString();
            string cardNumber2 = rand.Next(10000000, 99999999).ToString();

            string cardNumber = cardNumber1 + cardNumber2;

            string cvcNumber = rand.Next(100, 999).ToString();
            string pinNumber = rand.Next(1000, 9999).ToString();

            try
            {
                string query = "INSERT INTO users (name, surname, card_number, card_cvc, card_pin, money) VALUES ('" + name + "', '" + surname + "', '"+ cardNumber +"', '" + cvcNumber + "', '" + pinNumber + "', '" + money + "')";
                SQLiteCommand cmd = new SQLiteCommand(query, db.myConnection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                goto Start;
            }
            
        }

        public void Login(string cardNumber, string cvcNumber, string pinNumber)
        {
            try
            {
                string query = "SELECT name, surname, money FROM users WHERE card_number = '" + cardNumber + "' AND card_cvc = '" + cvcNumber + "' AND card_pin = '" + pinNumber + "'";
                SQLiteCommand cmd = new SQLiteCommand(query, db.myConnection);
                SQLiteDataReader dataX = cmd.ExecuteReader();
                dataX.Read();
                Console.WriteLine(dataX["name"] + " " + dataX["surname"] + " " + dataX["money"]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Złe dane, zacznij od nowa");
                Console.ReadLine();
            }
            

        }
    }
}