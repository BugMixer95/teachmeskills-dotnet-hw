using System;

namespace HW1_Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            // setting up localization, so now decimal divider is a dot (.)
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            // this variable checks if the app is being started or not 
            bool isProgramStarted = true;

            // declaring of variables
            string userArgument1;
            string userArgument2;
            double argument1 = 0;
            double argument2 = 0;
            double result = 0;
            string userCommand;
            string userMathOperation = null;

            double[] savedResults = new double[5];
            int resultIndex = 0;
            int previousResultsQuantity = 0;

            // main app cycle, when it goes to end it asks user if he want to exit the app or not
            do
            {
                // just beautiful header
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Calculator ");
                Console.ResetColor();
                Console.WriteLine("[Version 1.0.1]");
                Console.WriteLine("(c) 2020 TMS .NET Courses. All rights reserved. \n");

                // this is start menu, where user can either start calulating or exit the app
                Console.WriteLine("Welcome! Please enter command to continue (without quotes): ");
                Console.WriteLine("* Start calculating  - 'start'");
                Console.WriteLine("* Exit application   - 'exit'");
                Console.Write("Your input: ");

                // declaring variable for user input validation + block of code where validation is run 
                bool userCommandCheck = false;

                while (!userCommandCheck)
                {
                    userCommand = Console.ReadLine().ToLower().Trim();

                    switch (userCommand)
                    {
                        case "start":
                            userCommandCheck = true;
                            break;

                        case "exit":
                            Console.WriteLine("\nThank you for using Calculator! Have a nice day!");
                            return;

                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nInvalid input! Please try again.");
                            Console.ResetColor();
                            Console.Write("Your input: ");
                            break;
                    }
                }

                // menu with possible operations
                Console.WriteLine("\nPlease enter an action you want to do (without quotes): " +
                    "\n* addition                   - 'add' or '+'" +
                    "\n* subtraction                - 'sub' or '-'" +
                    "\n* multiplication             - 'mult' or '*'" +
                    "\n* division                   - 'div' or '/'" +
                    "\n* calculate the percentage   - 'perc' or '%'" +
                    "\n* calculate the square root  - 'sqrt'");
                Console.Write("Your input: ");

                // this variable is responsible for the case when user wants to repeat his last operation
                bool operationRepeat = false;

                do
                {
                    // here program checks user input and specifies operation user wants to do
                    userCommandCheck = false;

                    while (!userCommandCheck)
                    {
                        if (userMathOperation == null)
                        {
                            userMathOperation = Console.ReadLine().ToLower().Trim();
                        }

                        switch (userMathOperation)
                        {
                            case "+":
                                Console.WriteLine("\nYou've chosen addition.");
                                Console.WriteLine("Hint: [argument1 + argument2]");
                                userCommandCheck = true;
                                break;

                            case "add":
                                userMathOperation = "+";
                                goto case "+";

                            case "-":
                                Console.WriteLine("\nYou've chosen subtraction.");
                                Console.WriteLine("Hint: [argument1 - argument2]");
                                userCommandCheck = true;
                                break;

                            case "sub":
                                userMathOperation = "-";
                                goto case "-";

                            case "*":
                                Console.WriteLine("\nYou've chosen multiplication.");
                                Console.WriteLine("Hint: [argument1 * argument2]");
                                userCommandCheck = true;
                                break;

                            case "mult":
                                userMathOperation = "*";
                                goto case "*";

                            case "/":
                                Console.WriteLine("\nYou've chosen division.");
                                Console.WriteLine("Hint: [argument1 / argument2]");

                                userCommandCheck = true;
                                break;

                            case "div":
                                userMathOperation = "/";
                                goto case "/";

                            case "%":
                                Console.WriteLine("\nYou've chosen calculation of the percentage.");
                                Console.WriteLine("Hint: [(argument1 / argument2) * 100]");
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.WriteLine("FYI: Your result will be rounded to 4 decimals.");
                                Console.ResetColor();

                                userCommandCheck = true;
                                break;

                            case "perc":
                                userMathOperation = "%";
                                goto case "%";

                            case "sqrt":
                                Console.WriteLine("\nYou've chosen calculation of the square root.");
                                Console.WriteLine("Hint: [sqrt(argument)]");
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.WriteLine("FYI: Your result will be rounded to 4 decimals.\n");
                                Console.ResetColor();

                                userCommandCheck = true;
                                break;

                            default:
                                userMathOperation = null;
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\nInvalid input! Please try again.");
                                Console.ResetColor();
                                Console.Write("Your input: ");
                                break;
                        }
                    }

                    // variable for user input validation
                    bool userArgumentCheck;

                    // here user enters arguments for chosen operation and then these arguments are validated

                    // block of code for every operation type except for extracting a square root
                    if (userMathOperation != "sqrt")
                    {
                        // trying to catch overflow exception
                        try
                        {
                            // entering the first argument
                            do
                            {
                                Console.Write("Please enter your first argument: ");

                                userArgument1 = Console.ReadLine().Replace(',', '.');
                                userArgumentCheck = Double.TryParse(userArgument1, out argument1);

                                if (!userArgumentCheck)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write("Input should have numeric type! ");
                                    Console.ResetColor();
                                }

                            }
                            while (!userArgumentCheck);

                            // variable for checking that user won't divide by zero
                            bool zeroArgumentCheck = false;

                            // entering the second argument
                            do
                            {
                                if (userMathOperation != "/")
                                {
                                    Console.Write("Please enter your second argument: ");
                                }
                                else if (userMathOperation == "/")
                                {
                                    Console.Write("Please enter your non-zero second argument: ");
                                }

                                userArgument2 = Console.ReadLine().Replace(',', '.');
                                userArgumentCheck = double.TryParse(userArgument2, out argument2);

                                if (!userArgumentCheck)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write("Input should have numeric type! ");
                                    Console.ResetColor();
                                }
                                else if (userMathOperation == "/" & argument2 == 0)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write("You cannot divide by zero! ");
                                    Console.ResetColor();
                                }
                                else
                                {
                                    zeroArgumentCheck = true;
                                }
                            }
                            while (!zeroArgumentCheck);
                        }
                        catch (OverflowException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }

                    // block of code for extracting a square root
                    else
                    {
                        bool sqrtArgumentCheck = false;

                        do
                        {
                            Console.Write("Please enter your argument (it should be equal to or above zero): ");

                            userArgument1 = Console.ReadLine().Replace(',', '.');
                            userArgumentCheck = Double.TryParse(userArgument1, out argument1);

                            if (!userArgumentCheck)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("Input should have numeric type! ");
                                Console.ResetColor();
                            }
                            else if (argument1 < 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("You cannot extract a square root from negative number! ");
                                Console.ResetColor();
                            }
                            else
                            {
                                sqrtArgumentCheck = true;
                            }
                        }
                        while (!sqrtArgumentCheck);
                    }

                    // block of code where calculation is being done
                    if (userMathOperation == "+")
                    {
                        result = argument1 + argument2;
                    }
                    else if (userMathOperation == "-")
                    {
                        result = argument1 - argument2;
                    }
                    else if (userMathOperation == "*")
                    {
                        result = argument1 * argument2;
                    }
                    else if (userMathOperation == "/")
                    {
                        result = argument1 / argument2;
                    }
                    else if (userMathOperation == "%")
                    {
                        result = Math.Round((argument1 / argument2) * 100, 4);
                    }
                    else if (userMathOperation == "sqrt")
                    {
                        result = Math.Round(Math.Sqrt(argument1), 4);
                    }

                    // saving result in a savedResults array
                    if (resultIndex == 5)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            savedResults[i] = savedResults[i + 1];
                        }

                        resultIndex--;
                    }

                    savedResults[resultIndex] = result;
                    resultIndex++;

                    if (previousResultsQuantity < 5)
                    {
                        previousResultsQuantity++;
                    }

                    // showing the result to user
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write("Your result is: ");
                    Console.ResetColor();

                    if (userMathOperation == "sqrt")
                    {
                        Console.WriteLine($"the square root of {argument1} are {result} and {result * -1}.");
                    }
                    else if (userMathOperation == "%")
                    {
                        Console.WriteLine($"{argument1} is {result}% of {argument2}");
                    }
                    else
                    {
                        Console.WriteLine($"{argument1} {userMathOperation} {argument2} = {result}.");
                    }

                    // asking user if he want to continue working or not
                    Console.WriteLine("\nYour operation is successful! For further work type one of the following commands (without quotes): " +
                            "\n* Repeat last operation      - 'repeat'" +
                            "\n* Show your previous results - 'prev-res'" +
                            "\n* Main menu                  - 'menu'" +
                            "\n* Exit application           - 'exit'");
                    Console.Write("Your input: ");

                    // checking user input
                    userCommandCheck = false;

                    while (!userCommandCheck)
                    {
                        userCommand = Console.ReadLine().ToLower();

                        switch (userCommand)
                        {
                            case "repeat":
                                operationRepeat = true;
                                userCommandCheck = true;
                                break;

                            case "menu":
                                operationRepeat = false;
                                userMathOperation = null;
                                userCommandCheck = true;
                                break;

                            case "prev-res":
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("\nYour previous results are:");
                                Console.ResetColor();

                                for (int i = 0; i <= (previousResultsQuantity - 1); i++)
                                {
                                    Console.WriteLine(savedResults[i]);
                                }

                                Console.Write("\nType your next command: ");
                                break;

                            case "exit":
                                Console.WriteLine("\nThank you for using Calculator! Have a nice day!");
                                operationRepeat = false;
                                userCommandCheck = true;
                                isProgramStarted = false;
                                break;

                            default:
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\nInvalid input! Please try again.");
                                Console.ResetColor();
                                Console.Write("Your input: ");
                                break;
                        }
                    }
                }
                while (operationRepeat);
            }
            while (isProgramStarted);
        }
    }
}