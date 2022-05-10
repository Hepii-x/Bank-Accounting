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

        public bool Login(string cardNumber, string cvcNumber, string pinNumber)
        {
            try
            {
                string query = "SELECT name, surname, money FROM users WHERE card_number = '" + cardNumber + "' AND card_cvc = '" + cvcNumber + "' AND card_pin = '" + pinNumber + "'";
                SQLiteCommand cmd = new SQLiteCommand(query, db.myConnection);
                SQLiteDataReader dataX = cmd.ExecuteReader();
                dataX.Read();
                Console.WriteLine("Witaj "+ dataX["name"]+ " "+ dataX["surname"] +"\n Posiadasz "+ dataX["money"] +"PLN środków na koncie");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Złe dane, zacznij od nowa");
                Console.WriteLine(ex);
                Console.ReadLine();
                return false;
            }
        }

        public void Withdraw(string cardNumber)
        {
            Console.Clear();
            Start:
            Console.WriteLine("Jaką kwotę chcesz wypłacić?");
            try
            {
                float money = float.Parse(Console.ReadLine());
                // Get money from DB
                string query = "SELECT money FROM users WHERE card_number = '" + cardNumber + "'";
                SQLiteCommand cmd = new SQLiteCommand(query, db.myConnection);
                SQLiteDataReader data = cmd.ExecuteReader();
                data.Read();
                double actualMoney = (double)data["money"];
                
                double moneyLeft = actualMoney - money;
                moneyLeft = Math.Round(moneyLeft, 2);
                Console.WriteLine(moneyLeft);
                if (money <= 0)
                {
                    Console.WriteLine("Nie posiadasz wystarczających środków na koncie!");
                }
                else
                {
                    string query2 = "UPDATE users SET money = '"+ moneyLeft +"' WHERE card_number = '"+ cardNumber +"'";
                    SQLiteCommand cmd2 = new SQLiteCommand(query2, db.myConnection);
                    cmd2.ExecuteNonQuery();
                    Console.WriteLine("Wypłacono: " + money);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Podaj poprawną kwotę!");
                Console.WriteLine(ex);
                goto Start;
            }
        }

        public void Deposit(string cardNumber)
        {
            Console.Clear();
            Start:
            Console.WriteLine("Jaką kwotę chcesz wpłacić?");
            try
            {
                float money = float.Parse(Console.ReadLine());
                string query = "SELECT money FROM users WHERE card_number = '" + cardNumber + "'";
                SQLiteCommand cmd = new SQLiteCommand(query, db.myConnection);
                SQLiteDataReader data = cmd.ExecuteReader();
                data.Read();
                double moneyLeft = (double)data["money"];
                double actualMoney = money + moneyLeft;
                actualMoney = Math.Round(actualMoney, 2);

                string query2 = "UPDATE users SET money = '" + actualMoney + "' WHERE card_number = '" + cardNumber + "'";
                SQLiteCommand cmd2 = new SQLiteCommand(query2, db.myConnection);
                cmd2.ExecuteNonQuery();
                Console.WriteLine("Wpłacono: " + money);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("Podaj poprawną kwotę!");
                goto Start;
            }
        }
    }
}