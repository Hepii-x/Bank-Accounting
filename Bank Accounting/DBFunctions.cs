using System;
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
            // Generating data
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
            catch
            {
                goto Start;
            }
            Console.WriteLine($"Zarejestrowano!\nTwoje dane\nNumer karty: {cardNumber}\nKod cvc: {cvcNumber}\nKod pin: {pinNumber}");
            Console.WriteLine("Zaloguj ponownie.");
            

            
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
                // Get money from db
                float money = float.Parse(Console.ReadLine());
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
                    // Update money in db
                    string query2 = "UPDATE users SET money = '"+ moneyLeft +"' WHERE card_number = '"+ cardNumber +"'";
                    SQLiteCommand cmd2 = new SQLiteCommand(query2, db.myConnection);
                    cmd2.ExecuteNonQuery();
                    Console.WriteLine("Wypłacono: " + money);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Podaj poprawną kwotę!");
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
                // Get money from db
                float money = float.Parse(Console.ReadLine());
                string query = "SELECT money FROM users WHERE card_number = '" + cardNumber + "'";
                SQLiteCommand cmd = new SQLiteCommand(query, db.myConnection);
                SQLiteDataReader data = cmd.ExecuteReader();
                data.Read();
                double moneyLeft = (double)data["money"];
                double actualMoney = money + moneyLeft;
                actualMoney = Math.Round(actualMoney, 2);

                // Update money in db
                string query2 = "UPDATE users SET money = '" + actualMoney + "' WHERE card_number = '" + cardNumber + "'";
                SQLiteCommand cmd2 = new SQLiteCommand(query2, db.myConnection);
                cmd2.ExecuteNonQuery();
                Console.WriteLine("Wpłacono: " + money);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Podaj poprawną kwotę!");
                goto Start;
            }
        }

        public void TransferMoney (string cardNumber1)
        {
            Console.Clear();
            Start:
            Console.WriteLine("Podaj numer karty na którą chcesz przelać: ");
            string cardNumber2 = Console.ReadLine();
            Console.WriteLine("Podaj kwotę przelewu: ");

            try
            {
                double yourMoney = double.Parse(Console.ReadLine());

                // Check if have money
                string query1 = "SELECT money FROM users WHERE card_number = '" + cardNumber1 + "'";
                SQLiteCommand cmd1 = new SQLiteCommand(query1, db.myConnection);
                SQLiteDataReader data1 = cmd1.ExecuteReader();
                data1.Read();
                double actualMoney = (double)data1["money"];
                double moneyLeft = actualMoney - yourMoney;

                if (yourMoney > actualMoney)
                {
                    Console.WriteLine("Nie wystarczająca ilość środków");
                    goto Start;
                }


                // Get second user data
                string query2 = "SELECT name, surname, money FROM users WHERE card_number = '" + cardNumber2 + "'";
                SQLiteCommand cmd2 = new SQLiteCommand(query2, db.myConnection);

                SQLiteDataReader data2 = cmd2.ExecuteReader();
                data2.Read();
                double secondMoney = (double)data2["money"];

                double newMoney = secondMoney + yourMoney;
                newMoney = Math.Round(newMoney, 2);
                // Change first user money in db
                moneyLeft = Math.Round(moneyLeft, 2);
                string query3 = "UPDATE users SET money = '" + moneyLeft + "' WHERE card_number = '" + cardNumber1 + "'";
                SQLiteCommand cmd3 = new SQLiteCommand(query3, db.myConnection);
                cmd3.ExecuteNonQuery();
                // Change second user money in db
                string query4 = "UPDATE users SET money = '" + newMoney + "' WHERE card_number = '" + cardNumber2 + "'";
                SQLiteCommand cmd4 = new SQLiteCommand(query4, db.myConnection);
                cmd4.ExecuteNonQuery();
                Console.WriteLine("Przelano: " + yourMoney + " dla " + data2["name"] + " " + data2["surname"]);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Dane niepoprawne!");
                goto Start;
            }
        }
    }
}