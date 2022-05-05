using System;
using System.Data.SQLite;

namespace Bank_Accounting
{    
    class Program
    {
        public Database db = new Database();

        static void Main(string[] args)
        {
            Database db = CreateDatabase();
            string loggedIn = DoLogin();
            if (loggedIn == "0")
                Environment.Exit(0);

            Console.WriteLine("Stan konta: " + GetMoneyDeposited(loggedIn) + " PLN");
            Console.WriteLine("1. Wypłać pieniądze");
            Console.WriteLine("2. Wpłać pieniądze");

            string choice = Console.ReadLine();
            if (choice == "1")
                WithdrawMoney(loggedIn);
            else if (choice == "2")
                DepositMoney(loggedIn);

            Console.ReadLine();
        }


            

            Console.ReadLine();
        }


        static Database OpenConnection()
        {
            Database databaseObject = new Database();
            return databaseObject;
        }

        static string DoLogin()
        {
            Console.WriteLine("Podaj numer karty: ");
            string cardNumber = Console.ReadLine();
            Console.WriteLine("Podaj kod cvc: ");
            string cvcNumber = Console.ReadLine();
            Console.WriteLine("Podaj kod pin: ");
            string pinNumber = Console.ReadLine();

            bool verified = Verification(cardNumber, cvcNumber, pinNumber);

            if (!verified)
            {
                Console.WriteLine("Złe dane!");
                return "0";
            }


            else
            {
                Console.WriteLine("Logowanie pomyślne!");
                return "unique-id";
            }

        }

        bool Verification (string cardNumber, string cvcNumber, string pinNumber)
        {
            string query = "SELECT name FROM sqlite_master WHERE name = 'users'";
            SQLiteCommand cmd = new SQLiteCommand(query, );
            if (cardNumber == "1111222233334444")
            {
                if (cvcNumber == "123")
                {
                    if (pinNumber == "1234")
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                return false;
        }

        static float GetMoneyDeposited (string uniqueId)
        {
            if (uniqueId == "unique-id")
                return (float)12.33;
            else
                return 0;
        }

        static void WithdrawMoney(string uniqueId)
        {
            float accountMoney = GetMoneyDeposited(uniqueId);

            Console.WriteLine("Ile pieniędzy chcesz wypłacić?");
            float moneyToWithdraw = float.Parse(Console.ReadLine());

            float moneyLeft = accountMoney - moneyToWithdraw;

            if (moneyLeft < 0)
                Console.WriteLine("Wprowadzona kwota jest za wysoka!");
            else
                Console.WriteLine("Pomyślnie wypłacono " + moneyToWithdraw + " PLN. Pozostała kwota " + moneyLeft + " PLN.");
        }

        static void DepositMoney(string uniqueId)
        {
            float accountMoney = GetMoneyDeposited(uniqueId);
            Console.WriteLine("Ile chcesz wpłacić?");
            float moneyToDeposit = float.Parse(Console.ReadLine());
            accountMoney += moneyToDeposit;

            Console.WriteLine("Wpłacono " + moneyToDeposit + " PLN. Aktualny stan konta to " + accountMoney + " PLN.");
        }
    }
}
