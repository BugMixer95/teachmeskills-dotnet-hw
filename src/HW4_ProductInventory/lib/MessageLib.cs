using System;
using System.Configuration;

namespace HW4_ProductInventory.lib
{
    public class MessageLib
    {
        private static readonly string appName = ConfigurationManager.AppSettings.Get("appName");

        public static void SayHello()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(appName);
            Console.ResetColor();
            Console.WriteLine(" [Version 1.0]");
            Console.WriteLine("(c) 2020 TMS .NET Courses. All rights reserved. \n");

            Console.WriteLine("Welcome to the first online console liquor store in the world!");
            Console.WriteLine("We will help you to create a special mood for your party by adding different type of drinks to your cart.");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("(Opportunity to order selected bunch of liquor will be added in future versions.)");
            Console.ResetColor();

            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
        }

        public static void Menu()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("To perform an action please type a number.");
            Console.ResetColor();
            Console.WriteLine("\nPossible actions are: ");

            Console.WriteLine("1 - add Beer to your cart");
            Console.WriteLine("2 - add Wine to your cart");
            Console.WriteLine("3 - add Vodka to your cart");
            Console.WriteLine("4 - add Gin to your cart");
            Console.WriteLine("5 - add Cognac to your cart");
            Console.WriteLine("6 - add Whisky to your cart");
            Console.WriteLine("7 - show your cart");
            Console.WriteLine("8 - exit program");

            Console.Write("Your input: ");
        }

        public static void SayGoodbye()
        {
            Console.Write("\nThank you for using ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(appName);
            Console.ResetColor();
            Console.WriteLine("! \nCome back soon!");
        }

        public static void AskHowMuchBottles() => Console.Write("\nPlease enter amount of bottles you want to add to your cart: ");

        public static void AddExtraSortOfBeer()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nDo you want to add specific type of beer?");
            Console.ResetColor();

            Console.WriteLine("1 - add Light Beer");
            Console.WriteLine("2 - add Dark Beer");
            Console.WriteLine("3 - add Wheat Beer");
            Console.WriteLine("4 - skip (add just total amount)");

            Console.Write("Your input: ");
        }

        public static void EmptyCart()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nYour cart is empty!");
            Console.ResetColor();
        }
    }
}
