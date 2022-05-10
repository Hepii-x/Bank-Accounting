﻿using System;

namespace Bank_Accounting
{    
    class Program
    {
        static void Main(string[] args)
        {
            bool repeat = false;
            DBFunctions db = new DBFunctions();
            Start:
            Console.WriteLine("1. Zaloguj");
            Console.WriteLine("2. Zarejestruj");
            string choice = Console.ReadLine();
            Console.Clear();
            if (choice == "1")
            {
                Console.WriteLine("Podaj numer karty: ");
                string cardNumber = Console.ReadLine();
                Console.WriteLine("Podaj kod cvc karty: ");
                string cvcNumber = Console.ReadLine();
                Console.WriteLine("Podaj kod pin karty: ");
                string pinNumber = Console.ReadLine();

                Start2:
                bool loggedIn = db.Login(cardNumber, cvcNumber, pinNumber);
                if (loggedIn)
                {
                    Console.WriteLine("1. Wypłata środków\n2. Wpłata środków\n3. Przelew");
                    string choice2 = Console.ReadLine();
                    if (choice2 == "1")
                    {
                        db.Withdraw(cardNumber);
                        repeat = Continue();
                    }
                    else if (choice2 == "2")
                    {
                        db.Deposit(cardNumber);
                        repeat = Continue();
                    }
                    else if (choice2 == "3")
                    {
                        db.TransferMoney(cardNumber);
                        repeat = Continue();
                    }
                    else
                    {
                        Console.WriteLine("Nie wybrano opcji!");
                        goto Start2;
                    }
                    if (repeat)
                        goto Start2;
                }
                else if (!loggedIn)
                    goto Start;

            }
            else if (choice == "2")
            {
                Console.WriteLine("Podaj imię: ");
                string name = Console.ReadLine();
                Console.WriteLine("Podaj nazwisko: ");
                string surname = Console.ReadLine();
                Console.WriteLine("Ilość pieniędzy do wpłaty: ");
                MoneyRegister:
                try
                {
                    float money = float.Parse(Console.ReadLine());
                    db.Register(name, surname, money);
                }
                catch
                {
                    Console.WriteLine("Niepoprawna kwota. Podaj jeszcze raz");
                    goto MoneyRegister;
                }

                
                goto Start;
            }
            
            else
            {
                Console.WriteLine("Nie wybrano opcji!");
                goto Start;
            }
        }

        static bool Continue()
        {
            Console.WriteLine("Kontyunować? T/N");
            string choice = Console.ReadLine().ToLower();
            if (choice == "t")
                return true;
            else
            {
                System.Environment.Exit(0);
                return false;
            }

        }


    }
}
