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
            if (alcohol != null && alcohol._qty > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n{alcohol.GetType().Name}");
                Console.ResetColor();

                Console.WriteLine($"Quantity: {alcohol._qty}");

                double totalPrice = alcohol._qty * alcohol._price;
                Console.WriteLine($"Price: {totalPrice} ({alcohol._price} per bottle)");

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

            for (int i = 0; i < alcohol.Length; i++)
            {
                ShowProductInfo(alcohol[i], ref totalCartPrice);
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\nTotal price: ");
            Console.ResetColor();
            Console.WriteLine(totalCartPrice);
        }
    }
}