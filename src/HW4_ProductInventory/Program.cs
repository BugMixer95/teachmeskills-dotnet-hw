using System;
using System.Configuration;
using HW4_ProductInventory.lib;
using HW4_ProductInventory.products;

namespace HW4_ProductInventory
{
    class Program
    {
        public static void Main(string[] args)
        {
            // setting up localization, just in case
            var appLocale = ConfigurationManager.AppSettings.Get("appLocale");
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(appLocale);

            // declaring variables
            bool isProgramStarted = true;
            bool isCartEmpty = true;

            int userCommandInput;
            int userQtyInput;

            // creating null objects for further work
            Beer beer = null;
            Wine wine = null;
            Vodka vodka = null;
            Gin gin = null;
            Cognac cognac = null;
            Whisky whisky = null;

            // saying hello to user
            Console.Clear();
            MessageLib.SayHello();
            Console.ReadKey();

            do
            {
                // showing menu to user
                Console.Clear();
                MessageLib.Menu();

                // accepting and validating input from user
                ParseUserInputToInt(true, out userCommandInput);

                switch (userCommandInput)
                {
                    // adding beer to cart
                    case (int)MenuActions.AddBeer:
                        MessageLib.AskHowMuchBottles();
                        ParseUserInputToInt(false, out userQtyInput);

                        beer = new Beer(userQtyInput);

                        isCartEmpty = false;

                        break;

                    // adding wine to cart
                    case (int)MenuActions.AddWine:
                        MessageLib.AskHowMuchBottles();
                        ParseUserInputToInt(false, out userQtyInput);

                        wine = new Wine(userQtyInput);

                        isCartEmpty = false;

                        break;

                    // adding vodka to cart
                    case (int)MenuActions.AddVodka:
                        MessageLib.AskHowMuchBottles();
                        ParseUserInputToInt(false, out userQtyInput);

                        vodka = new Vodka(userQtyInput);

                        isCartEmpty = false;

                        break; 

                    // adding gin to cart
                    case (int)MenuActions.AddGin:
                        MessageLib.AskHowMuchBottles();
                        ParseUserInputToInt(false, out userQtyInput);

                        gin = new Gin(userQtyInput);

                        isCartEmpty = false;

                        break;

                    // adding cognac to cart
                    case (int)MenuActions.AddCognac:
                        MessageLib.AskHowMuchBottles();
                        ParseUserInputToInt(false, out userQtyInput);

                        cognac = new Cognac(userQtyInput);

                        isCartEmpty = false;

                        break;

                    // adding whisky to cart
                    case (int)MenuActions.AddWhisky:
                        MessageLib.AskHowMuchBottles();
                        ParseUserInputToInt(false, out userQtyInput);

                        whisky = new Whisky(userQtyInput);

                        isCartEmpty = false;

                        break;

                    // showing user his cart
                    case (int)MenuActions.ShowCart:

                        // if cart is empty show user appropriate message
                        if (isCartEmpty)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("\nYour cart is empty!");
                            Console.ResetColor();

                            Console.WriteLine("\nPress any key to return to menu...");
                            Console.ReadKey();

                            break;
                        }

                        // otherwise user's cart will be processed and shown
                        Inventory.ShowInventory(beer, wine, vodka, gin, cognac, whisky);

                        Console.WriteLine("\nPress any key to return to menu...");
                        Console.ReadKey();

                        break;

                    // exiting the program
                    case (int)MenuActions.Exit:
                        MessageLib.SayGoodbye();
                        return;
                }
            }
            while (isProgramStarted);

            Console.ReadKey();
        }

        private static void ParseUserInputToInt(bool isMainMenu, out int userInputParsed)
        {
            bool validation;

            do
            {
                string userInput = Console.ReadLine();
                validation = int.TryParse(userInput, out userInputParsed);

                if (!validation && !isMainMenu)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nInvalid input! Please type integer value: ");
                    Console.ResetColor();
                    Console.Write("Your input: ");
                }
                else if (!validation && isMainMenu)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nInvalid input! Please type one of the available commands: ");
                    Console.ResetColor();
                    Console.Write("Your input: ");
                }
            }
            while (!validation);
        }

        private enum MenuActions
        {
            AddBeer = 1,
            AddWine,
            AddVodka,
            AddGin,
            AddCognac,
            AddWhisky,
            ShowCart,
            Exit
        }
    }
}