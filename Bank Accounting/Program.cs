using System;
using System.Data.SQLite;

namespace Bank_Accounting
{    
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("KEKW");
            Console.WriteLine("Gigachad");
            DBFunctions db = new DBFunctions();

            //db.InsertDB();
            db.GetFromDB();

            Console.ReadLine();
        }

        
    }
}
