using System;

namespace HW2_Matrix
{
    class Program
    {
        static void Main(string[] args)
        {
            // setting up localization, so now decimal divider is a dot (.)
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            // variable that specifies if the program is running or not
            bool isProgramStarted = true;

            // declaring variables
            string userColumns;
            string userRows;
            string userElement;
            string userCommandInput;
            long columns;
            long rows;
            long amountOfElements;

            double temp;

            bool userInputIsDone;
            bool userRangeInputConvert;
            bool userElementInputConvert;
            bool userCommandCheck;

            // these are used if user chooses autoentering of elements
            string userRandMin;
            string userRandMax;
            int randMin;
            int randMax;

            // main app cycle, when it goes to end it asks user if he want to exit the app or not
            do
            {
                // saying hello to user
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Create Your Own Matrix! ");
                Console.ResetColor();
                Console.WriteLine("[Version 1.0]");
                Console.WriteLine("(c) 2020 TMS .NET Courses. All rights reserved. \n");

                Console.WriteLine("Welcome! In this program you can create your own matrix!");
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("// You've already got it if you can read program title.");
                Console.ResetColor();
                Console.WriteLine("Let's get started!");
                Console.WriteLine();

                // validation of rows inputted by user
                do
                {
                    userInputIsDone = false;
                    Console.Write("Type number of rows your matrix will have: ");
                    userRows = Console.ReadLine();
                    userRangeInputConvert = long.TryParse(userRows, out rows);

                    if (!userRangeInputConvert)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nInput should have numeric type!");
                        Console.ResetColor();
                    }
                    else if (rows <= 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nInput should be more than zero!");
                        Console.ResetColor();
                    }
                    else
                    {
                        userInputIsDone = true;
                    }
                }
                while (!userInputIsDone);

                // validation of columns inputted by user
                do
                {
                    userInputIsDone = false;
                    Console.Write("Type number of columns your matrix will have: ");
                    userColumns = Console.ReadLine();
                    userRangeInputConvert = long.TryParse(userColumns, out columns);

                    if (!userRangeInputConvert)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nInput should have numeric type!");
                        Console.ResetColor();
                    }
                    else if (columns <= 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nInput should be more than zero!");
                        Console.ResetColor();
                    }
                    else
                    {
                        userInputIsDone = true;
                    }
                }
                while (!userInputIsDone);

                // creating matrix with specified range
                double[,] matrix = new double[1, 1];

                try
                {
                    matrix = new double[rows, columns];
                }
                catch (OutOfMemoryException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Not enough memory for completing your operation!");
                    Console.ResetColor();
                    Console.WriteLine();
                }

                // user input of matrix elements
                Console.WriteLine("\nNow you should enter your matrix elements.");

                do
                {
                    bool autoModIsSelected = false;

                    userInputIsDone = false;
                    userCommandInput = null;
                    long quantityOfElements = rows * columns;

                    if (quantityOfElements >= 30)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("Attention! ");
                        Console.ResetColor();
                        Console.WriteLine($"Your matrix will contain {quantityOfElements} elements!");
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.WriteLine("// You are not so enduring as I am, human, " +
                            "so I think it will be hard for you to enter so much values in your matrix.");
                        Console.ResetColor();
                        Console.WriteLine("What would you like to do?" +
                            "\n* to enter all the values manually                     - 'manual'" +
                            "\n* to allow me to create all the values automatically   - 'auto'");
                        Console.Write("Your input (without quotes): ");

                        userCommandCheck = false;

                        while (!userCommandCheck)
                        {
                            userCommandInput = Console.ReadLine().ToLower();

                            switch (userCommandInput)
                            {
                                case "manual":
                                    autoModIsSelected = false;
                                    userCommandCheck = true;
                                    break;

                                case "auto":
                                    autoModIsSelected = true;
                                    userCommandCheck = true;
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

                    // this is run if total quantity of matrix elements < 15 OR if user chooses manual mode
                    if (!autoModIsSelected)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("Hint: Your elements have position in format [row, column].");
                        Console.ResetColor();

                        for (int x = 0; x < rows; x++)
                        {
                            for (int y = 0; y < columns; y++)
                            {
                                do
                                {
                                    Console.Write($"Enter value for element with position [{x}, {y}]: ");

                                    userElement = Console.ReadLine().Replace(',', '.');
                                    userElementInputConvert = Double.TryParse(userElement, out matrix[x, y]);

                                    if (!userElementInputConvert)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("\nInput should have numeric type!");
                                        Console.ResetColor();
                                    }

                                }
                                while (!userElementInputConvert);

                            }
                        }
                    }

                    // and this is done if total quantity of matrix elements >= 15 and user chooses auto mode
                    else if (autoModIsSelected)
                    {
                        // entering min value for Random auto-generating
                        do
                        {
                            Console.Write("Enter min value for auto-generating (only integer values is allowed): ");

                            userRandMin = Console.ReadLine();
                            userElementInputConvert = int.TryParse(userRandMin, out randMin);

                            if (!userElementInputConvert)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\nInput should be of integer type!");
                                Console.ResetColor();
                            }
                        }
                        while (!userElementInputConvert);

                        // entering max value for Random auto-generating
                        do
                        {
                            Console.Write("Enter max value for auto-generating (only integer values is allowed): ");

                            userRandMax = Console.ReadLine();
                            userElementInputConvert = int.TryParse(userRandMax, out randMax);

                            if (!userElementInputConvert)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\nInput should be of integer type!");
                                Console.ResetColor();
                            }
                        }
                        while (!userElementInputConvert);

                        for (int x = 0; x < rows; x++)
                        {
                            for (int y = 0; y < columns; y++)
                            {
                                Random rand = new Random();
                                matrix[x, y] = rand.Next(randMin, randMax);
                            }
                        }
                    }

                    userInputIsDone = true;
                }
                while (!userInputIsDone);

                userInputIsDone = false;

                do
                {
                    // menu with further work
                    Console.WriteLine("\nDone! For further work type one of the following commands (without quotes): " +
                        "\n* Display the matrix                                 - 'display'" +
                        "\n* Calculate amount of positive elements              - 'calc-pos'" +
                        "\n* Calculate amount of negative elements              - 'calc-neg'" +
                        "\n* Calculate amount of zero elements                  - 'calc-zero'" +
                        "\n* Sort matrix elements line by line (ascending)      - 'sort-asc'" +
                        "\n* Sort matrix elements line by line (desceniding)    - 'sort-desc'" +
                        "\n* Inverse of matrix elements line by line            - 'inverse'" +
                        "\n* Create new matrix                                  - 'restart'" +
                        "\n* Show all available commands                        - 'help'" +
                        "\n* Exit program                                       - 'exit'");

                    // checking user input
                    userCommandCheck = false;

                    while (!userCommandCheck)
                    {
                        Console.Write("\nYour input: ");
                        userCommandInput = Console.ReadLine().ToLower();

                        switch (userCommandInput)
                        {
                            // displaying matrix in the console
                            case "display":
                                for (int x = 0; x < rows; x++)
                                {
                                    for (int y = 0; y < columns; y++)
                                    {
                                        Console.Write($"{matrix[x, y]} \t");
                                    }
                                    Console.WriteLine();
                                }
                                break;

                            // calculating amount of positive elements
                            case "calc-pos":
                                amountOfElements = 0;
                                for (int x = 0; x < rows; x++)
                                {
                                    for (int y = 0; y < columns; y++)
                                    {
                                        if (matrix[x, y] > 0)
                                        {
                                            amountOfElements++;
                                        }
                                    }
                                }
                                Console.WriteLine($"Amount of positive elements: {amountOfElements}.");
                                break;

                            // calculating amount of negative elements
                            case "calc-neg":
                                amountOfElements = 0;
                                for (int x = 0; x < rows; x++)
                                {
                                    for (int y = 0; y < columns; y++)
                                    {
                                        if (matrix[x, y] < 0)
                                        {
                                            amountOfElements++;
                                        }
                                    }
                                }
                                Console.WriteLine($"Amount of negative elements: {amountOfElements}.");
                                break;

                            // calculating amount of zero elements
                            case "calc-zero":
                                amountOfElements = 0;
                                for (int x = 0; x < rows; x++)
                                {
                                    for (int y = 0; y < columns; y++)
                                    {
                                        if (matrix[x, y] == 0)
                                        {
                                            amountOfElements++;
                                        }
                                    }
                                }
                                Console.WriteLine($"Amount of zero elements: {amountOfElements}.");
                                break;

                            case "sort-asc":
                                for (int x = 0; x < rows; x++)
                                {
                                    for (int y = 0; y < columns; y++)
                                    {
                                        for (int t = 0; t < columns; t++)
                                        {
                                            if (matrix[x, t] >= matrix[x, y])
                                            {
                                                temp = matrix[x, t];
                                                matrix[x, t] = matrix[x, y];
                                                matrix[x, y] = temp;
                                            }
                                        }
                                    }
                                }
                                Console.WriteLine("Done!");
                                break;

                            case "sort-desc":
                                for (int x = 0; x < rows; x++)
                                {
                                    for (int y = 0; y < columns; y++)
                                    {
                                        for (int t = 0; t < columns; t++)
                                        {
                                            if (matrix[x, t] <= matrix[x, y])
                                            {
                                                temp = matrix[x, t];
                                                matrix[x, t] = matrix[x, y];
                                                matrix[x, y] = temp;
                                            }
                                        }
                                    }
                                }
                                Console.WriteLine("Done!");
                                break;

                            case "inverse":
                                long rowsCenter = columns / 2;
                                for (int x = 0; x < rows; x++)
                                {
                                    for (int y = 0; y < rowsCenter; y++)
                                    {
                                        temp = matrix[x, y];
                                        matrix[x, y] = matrix[x, (columns - y - 1)];
                                        matrix[x, (columns - y - 1)] = temp;
                                    }
                                }
                                Console.WriteLine("Done!");
                                break;

                            case "help":
                                Console.WriteLine("For further work type one of the following commands (without quotes): " +
                                    "\n* Display the matrix                                 - 'display'" +
                                    "\n* Calculate amount of positive elements              - 'calc-pos'" +
                                    "\n* Calculate amount of negative elements              - 'calc-neg'" +
                                    "\n* Calculate amount of zero elements                  - 'calc-zero'" +
                                    "\n* Sort matrix elements line by line (ascending)      - 'sort-asc'" +
                                    "\n* Sort matrix elements line by line (desceniding)    - 'sort-desc'" +
                                    "\n* Inverse of matrix elements line by line            - 'inverse'" +
                                    "\n* Create new matrix                                  - 'restart'" +
                                    "\n* Show all available commands                        - 'help'" +
                                    "\n* Exit program                                       - 'exit'");
                                break;

                            case "restart":
                                userCommandCheck = true;
                                userInputIsDone = true;
                                break;

                            case "exit":
                                return;

                            default:
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("\nInvalid input! Please try again.");
                                Console.ResetColor();
                                break;
                        }
                    }
                }
                while (!userInputIsDone);

            }
            while (isProgramStarted);

        }
    }
}
