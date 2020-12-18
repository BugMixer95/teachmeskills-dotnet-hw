using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.IO;

namespace HW3_TextReader
{
    class Program
    {
        static void Main(string[] args)
        {
            // setting up encoding to Unicode to make cyrillic letters inputted correctly
            Console.InputEncoding = Encoding.Unicode;

            // declaring variables
            string userTextSource = null;
            StringBuilder userText = new StringBuilder(userTextSource);

            int startPoint;

            bool programIsStarted = true;
            bool userCommandCheck;
            bool textIsSelected;
            bool userActionIsDone;

            bool cyrillicCheck;
            bool emptyTextCheck;
            string cyrillicPattern = @"\p{IsCyrillic}";

            string userChoice = null;
            string userAction = null;

            string currentDirectory = Directory.GetCurrentDirectory();
            string fileDirectory;
            string filePath;

            int punctuationMarksQty;
            string[] punctuationIndexes;

            // main app cycle, when it goes to end it asks user if he want to exit the app or not
            do
            {
                // just beautiful header
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("TextReader ");
                Console.ResetColor();
                Console.WriteLine("[Version 1.0]");
                Console.WriteLine("(c) 2020 TMS .NET Courses. All rights reserved. \n");

                // start menu
                Console.WriteLine("Welcome to the TextReader! \nThis program allows you to process your text and do some actions with it. " +
                    "\nYou can type your own text or enter any text in the file. \n(file is placed in \\file folder in the root folder of the program)");
                Console.WriteLine("\nTo start working please choose what you want to do (without quotes): " +
                    "\n* Enter text manually - 'manual'" +
                    "\n* Choose text file    - 'file'");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("FYI: Text cannot be empty or contain cyrillic characters!");
                Console.ResetColor();
                Console.Write("Your input: ");

                // menu where user can choose whether he wants to enter text manually or to take text from file
                userCommandCheck = false;

                while (!userCommandCheck)
                {
                    userChoice = Console.ReadLine().ToLower().Trim();

                    switch (userChoice)
                    {
                        case "manual":
                            Console.WriteLine("\nYou've decided to enter your text manually.");
                            userCommandCheck = true;
                            break;

                        case "file":
                            Console.WriteLine("\nYou've decided to read text from a file.");
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

                // here text is being inputted by user OR read from file + check for cyrillic symbols is made
                textIsSelected = false;

                while (!textIsSelected)
                {
                    // block of code for manual text entering
                    if (userChoice == "manual")
                    {
                        Console.Write("Your input: ");
                        userTextSource = Console.ReadLine();

                        cyrillicCheck = Regex.IsMatch(userTextSource, cyrillicPattern);
                        emptyTextCheck = string.IsNullOrEmpty(userTextSource) || string.IsNullOrWhiteSpace(userTextSource);

                        if (cyrillicCheck)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nYour text contains cyrillic symbols! Please enter text once again: ");
                            Console.ResetColor();
                        }
                        else if (emptyTextCheck)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nYou've entered empty text! Please enter text once again: ");
                            Console.ResetColor();
                        }
                        else
                        {
                            userText.Clear();
                            userText.Insert(0, userTextSource);

                            textIsSelected = true;
                        }
                    }

                    // block of code for reading text from file
                    else if (userChoice == "file")
                    {
                        int directoryIndex = currentDirectory.IndexOf("bin\\");
                        fileDirectory = currentDirectory.Remove(directoryIndex) + "file";
                        filePath = fileDirectory + "\\text.txt";

                        // program will create needed directory with default text file if specified directory doesn't exist
                        if (!Directory.Exists(fileDirectory))
                        {
                            Directory.CreateDirectory(fileDirectory);
                            File.WriteAllText(filePath, "Hello World!", Encoding.Unicode);

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("FYI: Due to absence of needed directory a default one with default text file was created.");
                            Console.ResetColor();
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();

                            using (StreamReader sr = new StreamReader(filePath, Encoding.Unicode))
                            {
                                userTextSource = sr.ReadToEnd();
                                textIsSelected = true;

                                break;
                            }
                        }

                        // otherwise program will read the "text.txt" file from the directory
                        bool textFromFileCheck = false;

                        while (!textFromFileCheck)
                        {
                            // reading text from file and validating it
                            try
                            {
                                using (StreamReader sr = new StreamReader(filePath, Encoding.Unicode))
                                {
                                    userTextSource = sr.ReadToEnd();
                                }

                                cyrillicCheck = Regex.IsMatch(userTextSource, cyrillicPattern);
                                emptyTextCheck = string.IsNullOrEmpty(userTextSource) || string.IsNullOrWhiteSpace(userTextSource);

                                if (cyrillicCheck)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\nFile contains cyrillic symbols!");
                                    Console.ResetColor();
                                    Console.WriteLine("Please enter text without cyrillic symbols in the file, save it and press any key to try again.");
                                    Console.ReadKey();
                                }
                                else if (emptyTextCheck)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\nFile doesn't contain any text!");
                                    Console.ResetColor();
                                    Console.WriteLine("Please enter text in the file, save it and press any key to try again.");
                                    Console.ReadKey();
                                }
                                else
                                {
                                    userText.Clear();
                                    userText.Insert(0, userTextSource);

                                    textIsSelected = true;
                                }

                                textFromFileCheck = true;
                            }

                            // in case of FileNotFoundException a new default file will be created
                            catch (FileNotFoundException)
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("File \"text.txt\" is missing! Program will create a new file with default text.");
                                Console.ResetColor();

                                File.WriteAllText(filePath, "Hello World!");
                            }

                            // in case of any exception it will be caught
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                    }
                }

                // menu with user actions
                userCommandCheck = false;

                while (!userCommandCheck)
                {
                    // all possible actions
                    Console.Clear();
                    Console.WriteLine("MENU:" +
                        "\n* Show words that contain the biggest amount of digits                   - 'max-digits'" +
                        "\n* Show the longest word and how many times it's presented in your text   - 'longest-word'" +
                        "\n* Replace all numbers to words (e.g. 0 - zero, etc.)                     - 'num-to-words'" +
                        "\n* Show all interrogative and exclamation sentences                       - 'show-?!'" +
                        "\n* Show all sentences without comma                                       - 'show-no-commas'" +
                        "\n* Show words that start and end with the same symbol                     - 'same-symbol'" +
                        "\n* Show your source text                                                  - 'source'" +
                        "\n* Go back to the main menu to change text or text source                 - 'main-menu'" +
                        "\n* Exit the program                                                       - 'exit'");
                    Console.Write("Your input: ");

                    // refreshing user text
                    userText.Clear();
                    userText.Insert(0, userTextSource);

                    // parsing user's text to separate sentences
                    char[] chars = UserTextToChars(userTextSource);
                    punctuationMarksQty = CountSentences(chars);
                    punctuationIndexes = FindPunctuationIndexes(userTextSource, punctuationMarksQty, chars);

                    // parsing user's text to array with separate words and calculating total amount of words
                    string[] words = userText.ToString().Split(' ', '.', ',', '!', '?', ':', ';', '\n', '\t');
                    int wordsAmount = words.Length;

                    // beginning of the cycle
                    userActionIsDone = false;

                    while (!userActionIsDone)
                    {
                        userAction = Console.ReadLine().ToLower().Trim();

                        // declaring variables for sentence actions
                        int startIndex;
                        int endIndex;
                        int outputLength;
                        string outputSentence;
                        bool isOnlyPunctuationMarks;

                        switch (userAction)
                        {
                            // finding words with the biggest amount of digits in them
                            case "max-digits":

                                // creating an array which will represent amount of words with digits
                                int[] digits = new int[wordsAmount];

                                for (int i = 0; i < wordsAmount; i++)
                                {
                                    int digitCount = 0;
                                    int wordLength = words[i].Length;
                                    char[] symbols = words[i].ToCharArray();

                                    for (int s = 0; s < wordLength; s++)
                                    {
                                        if (char.IsDigit(symbols[s]))
                                        {
                                            digitCount++;
                                        }
                                    }

                                    digits[i] = digitCount;
                                }

                                // finding max amount of digits in words and total amount of maxes
                                int digitsMax = digits.Max();
                                int digitsMaxTotal = 0;

                                for (int i = 0; i < wordsAmount; i++)
                                {
                                    if (digits[i] == digitsMax)
                                    {
                                        digitsMaxTotal++;
                                    }
                                }

                                // putting words with max amount of digits into new array
                                string[] wordsWithDigitsAll = new string[digitsMaxTotal];
                                startPoint = 0;

                                for (int j = 0; j < digitsMaxTotal; j++)
                                {
                                    for (int i = startPoint; i < wordsAmount; i++)
                                    {
                                        if (digits[i] == digitsMax && wordsWithDigitsAll[j] == null)
                                        {
                                            wordsWithDigitsAll[j] = words[i];
                                            startPoint = i + 1;
                                        }
                                    }
                                }

                                // showing user the result

                                // when user's text doesn't contain words with digits he receives appropriate message
                                if (digitsMax == 0)
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.Write("\nDone! ");
                                    Console.ResetColor();
                                    Console.WriteLine("Your text doesn't contain words with digits.");
                                }

                                // else (when text contains words with digits) - deleting duplicates and showing user the result
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("\nDone! Here is your result: ");
                                    Console.ResetColor();

                                    string[] wordsWithDigits = DeleteWordDuplicates(wordsWithDigitsAll, true);
                                    int wordsWithDigitsQty = wordsWithDigits.Length;

                                    for (int i = 0; i < wordsWithDigitsQty; i++)
                                    {
                                        // calculating how many times word is presented in the text
                                        int amount = 0;

                                        for (int j = 0; j < digitsMaxTotal; j++)
                                        {
                                            if (wordsWithDigits[i].ToUpper() == wordsWithDigitsAll[j].ToUpper())
                                            {
                                                amount++;
                                            }
                                        }

                                        // calculating how much digits does the word have
                                        int digitsCount = 0;

                                        for (int k = 0; k < wordsAmount; k++)
                                        {
                                            if (wordsWithDigits[i].ToUpper() == words[k].ToUpper())
                                            {
                                                digitsCount = digits[k];
                                            }
                                        }

                                        Console.WriteLine($"Word {wordsWithDigits[i]} has {digitsCount} digit(s) in it. " +
                                            $"It's presented {amount} time(s) in your text.");
                                    }
                                }

                                Console.WriteLine("\nPress any key to get back to the menu.");
                                Console.ReadKey();

                                userActionIsDone = true;
                                break;

                            // finding the longest words in the text and showing how much duplicates it has
                            case "longest-word":

                                // creating an array with the length of each separate word
                                int[] lettersAmount = new int[wordsAmount];

                                for (int i = 0; i < wordsAmount; i++)
                                {
                                    lettersAmount[i] = words[i].Length;
                                }

                                // finding max length
                                int lettersMax = lettersAmount.Max();

                                // exiting the cycle when user's text contain word with 0 symbols (rare case)
                                if (lettersMax == 0)
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("\nYour text doesn't contain words.");
                                    Console.ResetColor();

                                    Console.WriteLine("\nPress any key to get back to the menu.");
                                    Console.ReadKey();

                                    userActionIsDone = true;
                                    break;
                                }

                                // finding amount of max length
                                int lettersMaxTotal = 0;

                                for (int i = 0; i < wordsAmount; i++)
                                {
                                    if (lettersAmount[i] == lettersMax)
                                    {
                                        lettersMaxTotal++;
                                    }
                                }

                                // putting words with max length into new array
                                string[] longestWordsAll = new string[lettersMaxTotal];
                                startPoint = 0;

                                for (int j = 0; j < lettersMaxTotal; j++)
                                {
                                    for (int i = startPoint; i < wordsAmount; i++)
                                    {
                                        if (lettersAmount[i] == lettersMax && longestWordsAll[j] == null)
                                        {
                                            longestWordsAll[j] = words[i];
                                            startPoint = i + 1;
                                        }
                                    }
                                }

                                // deleting duplicates of longest words, calculating how many of them are in the text and showing user the result
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("\nDone! Here is your result: ");
                                Console.ResetColor();

                                string[] longestWords = DeleteWordDuplicates(longestWordsAll, true);
                                int longestWordsQty = longestWords.Length;

                                for (int i = 0; i < longestWordsQty; i++)
                                {
                                    int amount = 0;
                                    int length = longestWords[i].Length;

                                    for (int j = 0; j < lettersMaxTotal; j++)
                                    {
                                        if (longestWords[i].ToUpper() == longestWordsAll[j].ToUpper())
                                        {
                                            amount++;
                                        }
                                    }

                                    Console.WriteLine($"Word {longestWords[i]} ({length} symbols) " +
                                        $"is presented {amount} time(s) in your text.");
                                }

                                Console.WriteLine("\nPress any key to get back to the menu.");
                                Console.ReadKey();

                                userActionIsDone = true;
                                break;

                            // replacing numbers with appropriate words
                            case "num-to-words":
                                userText.Replace("0", "zero");
                                userText.Replace("1", "one");
                                userText.Replace("2", "two");
                                userText.Replace("3", "three");
                                userText.Replace("4", "four");
                                userText.Replace("5", "five");
                                userText.Replace("6", "six");
                                userText.Replace("7", "seven");
                                userText.Replace("8", "eight");
                                userText.Replace("9", "nine");

                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("\nDone! Here is your result: ");
                                Console.ResetColor();
                                Console.WriteLine(userText);

                                Console.WriteLine("\nPress any key to get back to the menu.");
                                Console.ReadKey();

                                userActionIsDone = true;
                                break;

                            // finding and showing all interrogative and exclamation sentences
                            case "show-?!":

                                // interrogative sentences output
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("\nDone! \nHere are your interrogative sentences: ");
                                Console.ResetColor();

                                startIndex = 0;

                                for (int i = 0; i < punctuationMarksQty; i++)
                                {
                                    endIndex = int.Parse(punctuationIndexes[i]);
                                    outputLength = (endIndex - startIndex + 1) == 0 ? 1 : (endIndex - startIndex + 1);

                                    // small check for case when user's text is just one sentence
                                    if (outputLength > (chars.Length - 1))
                                    {
                                        outputLength = endIndex;
                                    }

                                    outputSentence = userTextSource.Substring(startIndex, outputLength);
                                    isOnlyPunctuationMarks = OnlyPunctuationMarksCheck(outputSentence);

                                    if (chars[endIndex] == '?' && !isOnlyPunctuationMarks)
                                    {
                                        Console.WriteLine(outputSentence.Trim());
                                        startIndex = endIndex + 1;
                                    }
                                    else
                                    {
                                        startIndex = endIndex + 1;
                                    }
                                }

                                // exclamation sentences output
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("\nHere are your exclamation sentences: ");
                                Console.ResetColor();

                                startIndex = 0;

                                for (int i = 0; i < punctuationMarksQty; i++)
                                {
                                    endIndex = int.Parse(punctuationIndexes[i]);
                                    outputLength = (endIndex - startIndex + 1) == 0 ? 1 : (endIndex - startIndex + 1);

                                    // small check for case when user's text is just one sentence
                                    if (outputLength > (chars.Length - 1))
                                    {
                                        outputLength = endIndex;
                                    }

                                    outputSentence = userTextSource.Substring(startIndex, outputLength);
                                    isOnlyPunctuationMarks = OnlyPunctuationMarksCheck(outputSentence);

                                    if (chars[endIndex] == '!' && !isOnlyPunctuationMarks)
                                    {
                                        Console.WriteLine(outputSentence.Trim());
                                        startIndex = endIndex + 1;
                                    }
                                    else
                                    {
                                        startIndex = endIndex + 1;
                                    }
                                }

                                Console.WriteLine("\nPress any key to get back to the menu.");
                                Console.ReadKey();

                                userActionIsDone = true;
                                break;

                            // finding and showing all sentences without commas
                            case "show-no-commas":

                                // showing sentences without commas
                                bool commaExists;

                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("\nDone! \nHere are sentences without commas: ");
                                Console.ResetColor();

                                startIndex = 0;

                                for (int i = 0; i < punctuationMarksQty; i++)
                                {
                                    endIndex = int.Parse(punctuationIndexes[i]);
                                    outputLength = (endIndex - startIndex + 1) == 0 ? 1 : (endIndex - startIndex + 1);

                                    // small check for case when user's text is just one sentence
                                    if (outputLength > (chars.Length - 1))
                                    {
                                        outputLength = endIndex;
                                    }

                                    outputSentence = userTextSource.Substring(startIndex, outputLength);
                                    commaExists = outputSentence.Contains(',');
                                    isOnlyPunctuationMarks = OnlyPunctuationMarksCheck(outputSentence);

                                    if (!commaExists && !isOnlyPunctuationMarks)
                                    {
                                        Console.WriteLine(outputSentence.Trim());
                                        startIndex = endIndex + 1;
                                    }
                                    else
                                    {
                                        startIndex = endIndex + 1;
                                    }
                                }

                                Console.WriteLine("\nPress any key to get back to the menu.");
                                Console.ReadKey();

                                userActionIsDone = true;
                                break;

                            // finding words that start and end with the same symbol
                            case "same-symbol":

                                // getting user input and validating it
                                string userString;
                                char userSymbol;

                                Console.Write("\nPlease enter symbol to find: ");

                                bool symbolParse = false;

                                do
                                {
                                    userString = Console.ReadLine().ToUpper().Trim();
                                    bool symbolToString = char.TryParse(userString, out userSymbol);

                                    if (symbolToString)
                                    {
                                        symbolParse = true;
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Invalid input! ");
                                        Console.ResetColor();
                                    }

                                }
                                while (!symbolParse);

                                // creating clone of words array for comfort further search
                                string[] wordsUpperCase = new string[wordsAmount];
                                Array.Copy(words, wordsUpperCase, wordsAmount);

                                for (int i = 0; i < wordsAmount; i++)
                                {
                                    wordsUpperCase[i] = wordsUpperCase[i].ToUpper();
                                }

                                // calculating amount of needed words
                                int wordsWithSameSymbolsAmount = 0;

                                for (int i = 0; i < wordsAmount; i++)
                                {
                                    if (wordsUpperCase[i].StartsWith(userSymbol) && wordsUpperCase[i].EndsWith(userSymbol))
                                    {
                                        wordsWithSameSymbolsAmount++;
                                    }
                                }

                                // in case when user's text doesn't contain needed words program shows user appropriate message
                                if (wordsWithSameSymbolsAmount == 0)
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.Write("\nDone! ");
                                    Console.ResetColor();
                                    Console.WriteLine($"Your text doesn't contain words starting and ending with '{userSymbol}'.");
                                }

                                // otherwise program will continue to process user's command
                                else
                                {
                                    // creating array with needed words
                                    string[] wordsWithSameSymbolsAll = new string[wordsWithSameSymbolsAmount];
                                    startPoint = 0;

                                    for (int j = 0; j < wordsWithSameSymbolsAmount; j++)
                                    {
                                        for (int i = startPoint; i < wordsAmount; i++)
                                        {
                                            bool symbolCheck = wordsUpperCase[i].StartsWith(userSymbol) && wordsUpperCase[i].EndsWith(userSymbol);

                                            if (symbolCheck && wordsWithSameSymbolsAll[j] == null)
                                            {
                                                wordsWithSameSymbolsAll[j] = words[i];
                                                startPoint = i + 1;
                                            }
                                        }
                                    }

                                    // result text
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine($"\nDone! Here are words starting and ending with '{userSymbol}': ");
                                    Console.ResetColor();

                                    // deleting duplicates
                                    string[] wordsWithSameSymbols = DeleteWordDuplicates(wordsWithSameSymbolsAll, false);
                                    int wordsQty = wordsWithSameSymbols.Length;

                                    // showing user the result
                                    for (int i = 0; i < wordsQty; i++)
                                    {
                                        Console.WriteLine(wordsWithSameSymbols[i]);
                                    }
                                }

                                Console.WriteLine("\nPress any key to get back to the menu.");
                                Console.ReadKey();

                                userActionIsDone = true;
                                break;

                            // showing source text to user
                            case "source":
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("\nHere is your source text: ");
                                Console.ResetColor();
                                Console.WriteLine(userTextSource);

                                Console.WriteLine("\nPress any key to get back to the menu.");
                                Console.ReadKey();

                                userActionIsDone = true;
                                break;

                            case "main-menu":
                                userActionIsDone = true;
                                userCommandCheck = true;

                                break;

                            case "exit":
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("\nThank you for using TextReader! Come back soon!");
                                Console.ResetColor();

                                return;

                            default:
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\nInvalid input! Please try again.");
                                Console.ResetColor();
                                Console.Write("Your input: ");

                                break;
                        }
                    }
                }
            }
            while (programIsStarted);
        }

        /// <summary>
        /// Transforms the specified string input into an array of chars.
        /// </summary>
        static char[] UserTextToChars(string userTextSource)
        {
            // counting text length
            char[] chars = userTextSource.ToCharArray();
            Array.Resize(ref chars, chars.Length + 1);

            return chars;
        }

        /// <summary>
        /// Counts amount of sentences in the specified char array.
        /// </summary>
        static int CountSentences(char[] chars)
        {
            // counting amount of all needed punctuation marks ('.','!','?')
            int punctuationMarksQty = 0;

            for (int i = 0; i < chars.Length; i++)
            {
                // this is done for case when we have two or more same marks one after another
                // so to make them be the part of one sentence (like "!!" or "???")
                if (i != 0)
                {
                    if (chars[i] == chars[i - 1])
                    {
                        continue;
                    }
                }

                // counting amount of needed punctuation marks
                if (chars[i] == '.' || chars[i] == '!' || chars[i] == '?')
                {
                    punctuationMarksQty++;
                }
            }

            // case when user's text doesn't contain any punctuation marks like '.', '!' and '?'; if so, then text will be processed as one sentence
            if (punctuationMarksQty == 0)
            {
                punctuationMarksQty = 1;
            }

            return punctuationMarksQty;
        }

        /// <summary>
        /// Finds indexes of dots, exclamation marks and question marks.
        /// </summary>
        static string[] FindPunctuationIndexes(string userTextSource, int sentencesAmount, char[] chars)
        {
            // finding indexes of all needed punctuation marks
            long charsLength = chars.Length - 1;
            string[] punctuationIndexes = new string[sentencesAmount];
            int startPointCondition;
            int startPointToFind;
            int startPoint = 0;

            for (int j = 0; j < sentencesAmount; j++)
            {
                startPointCondition = 0;

                for (int i = startPoint; i < charsLength; i++)
                {
                    if (chars[i] == chars[i + 1])
                    {
                        startPointCondition = i + 1;
                        continue;
                    }
                    else if ((chars[i] == '.' || chars[i] == '!' || chars[i] == '?') && punctuationIndexes[j] == null)
                    {
                        startPointToFind = startPointCondition == 0 ? startPoint : startPointCondition;
                        punctuationIndexes[j] = userTextSource.IndexOf(chars[i], startPointToFind).ToString();
                        startPoint = i + 1;
                        startPointCondition = i + 1;
                    }
                }
            }

            // case when user's text doesn't contain any punctuation marks like '.', '!' and '?'
            // if so, then text will be processed as one sentence
            if (sentencesAmount == 1 && punctuationIndexes[0] == null)
            {
                punctuationIndexes[0] = charsLength.ToString();
            }

            return punctuationIndexes;
        }

        /// <summary>
        /// Checks if the specified string input consists of only dots, exclamation marks and question marks.
        /// </summary>
        /// <returns>
        /// true if all chars are dots, exclamation marks and question marks; otherwise, false.
        /// </returns>
        static bool OnlyPunctuationMarksCheck(string sentence)
        {
            // checking if sentence contains only punctuation marks
            bool onlyPunctuationMarks = false;
            sentence = sentence.Trim();
            int sentenceLength = sentence.Length;
            int punctuationMarksAmount = 0;

            for (int i = 0; i < sentenceLength; i++)
            {
                if (sentence[i] == '?' || sentence[i] == '!' || sentence[i] == '.')
                {
                    punctuationMarksAmount++;
                }
            }

            if (punctuationMarksAmount == sentenceLength)
            {
                onlyPunctuationMarks = true;
            }

            return onlyPunctuationMarks;
        }

        /// <summary>
        /// Removes duplicates in the specified word array.
        /// </summary>
        /// <returns>
        /// array without duplicates set to lower case if true; otherwise, source case will be saved.
        /// </returns>
        static string[] DeleteWordDuplicates(string[] wordsArray, bool setToLowerCase)
        {
            // counting amount of needed words
            int lengthAll = wordsArray.Length;
            string[] arrayCopy;

            if (!setToLowerCase)
            {
                arrayCopy = new string[lengthAll];
                Array.Copy(wordsArray, arrayCopy, lengthAll);
            }
            else
            {
                arrayCopy = wordsArray;
            }

            for (int i = 0; i < lengthAll; i++)
            {
                arrayCopy[i] = arrayCopy[i].ToLower();
            }

            string[] arrayNoDuplicates = arrayCopy.Distinct().ToArray();
            int lengthDistinct = arrayNoDuplicates.Length;

            // creating new array with needed words
            string[] wordsWithNoDuplicates = new string[lengthDistinct];
            wordsWithNoDuplicates[0] = wordsArray[0];
            int startPoint = 1;

            for (int i = 1; i < lengthDistinct; i++)
            {
                for (int j = startPoint; j < lengthAll; j++)
                {
                    if (wordsArray[j].ToUpper() == wordsArray[j - 1].ToUpper())
                    {
                        continue;
                    }
                    else if (wordsWithNoDuplicates[i] == null)
                    {
                        wordsWithNoDuplicates[i] = wordsArray[j];
                        startPoint = j + 1;
                    }
                }
            }

            return wordsWithNoDuplicates;
        }
    }
}