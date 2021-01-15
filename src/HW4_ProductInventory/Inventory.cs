using System;
using System.Collections.Generic;
using System.Text;
using HW4_ProductInventory.products;

namespace HW4_ProductInventory
{
    public class Inventory
    {
        private static void ShowProductInfo(Alcohol alcohol, ref double totalCartPrice)
        {
            // checks if specified good exists in user's cart
            if (alcohol != null && alcohol._qty > 0)
            {
                // showing user name of good and its amount
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n{alcohol.GetType().Name}");
                Console.ResetColor();

                Console.WriteLine($"Quantity: {alcohol._qty}");

                // counting total price of specified good and showing it to user
                double totalPrice = alcohol._qty * alcohol._price;
                Console.WriteLine($"Price: {totalPrice} ({alcohol._price} per bottle)");

                // count total price of the whole cart
                totalCartPrice += totalPrice;
            }
        }

        public static void ShowInventory(params Alcohol[] alcohol)
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Your cart: ");
            Console.ResetColor();

            double totalCartPrice = 0;

            // showing user all his goods in his cart
            for (int i = 0; i < alcohol.Length; i++)
            {
                ShowProductInfo(alcohol[i], ref totalCartPrice);
            }

            // showing user total price of the whole cart
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\nTotal price: ");
            Console.ResetColor();
            Console.WriteLine(totalCartPrice);
        }
    }
}