using System;

namespace Bank_Accounting
{
    class Program
    {
        static void Main(string[] args)
        {
            DoLogin();
            Console.ReadLine();
        }

        static void DoLogin()
        {
            Console.WriteLine("Podaj numer karty: ");
            string cardNumber = Console.ReadLine();
            Console.WriteLine("Podaj kod cvc: ");
            string cvcNumber = Console.ReadLine();
            Console.WriteLine("Podaj kod pin: ");
            string pinNumber = Console.ReadLine();
            Console.WriteLine(Verification(cardNumber, cvcNumber, pinNumber));
        }

        static bool Verification (string cardNumber, string cvcNumber, string pinNumber)
        {
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
    }
}
