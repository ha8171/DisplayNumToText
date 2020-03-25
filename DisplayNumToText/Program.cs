using System;
using DisplayNumToText.Processor;
using DisplayNumToText.Logger;

namespace DisplayNumToText
{
    class Program
    {
        static int low = default(int);
        static int high = default(int);
        static int a = default(int);
        static int b = default(int);

        static string low_input = default(string);
        static string high_input = default(string);
        static string a_input = default(string);
        static string b_input = default(string);
        static string message = default(string);

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Please enter your inputs -");
                try
                {
                    FileLogger.Instance.LogToFile("App started!");
                    Process();
                    FileLogger.Instance.LogToFile($"App ended!");
                    FileLogger.Instance.LogToFile("**********");
                }
                catch (FormatException ex)
                {
                    message = "Error: Please input valid integer numbers!";
                    FileLogger.Instance.LogToFile($"User entered: {low}, {high}, {a}, {b}");

                    LogToConsole(message);
                    FileLogger.Instance.LogToFile($"{message}{Environment.NewLine}{ex.Message}");
                }
                catch (DivideByZeroException ex)
                {
                    message = "Error: Please enter value > 0";
                    LogToConsole(message);
                    FileLogger.Instance.LogToFile($"{message}{Environment.NewLine}{ex.Message}");
                }
                catch (Exception ex)
                {
                    message = "Error: Something went wrong!";
                    LogToConsole(message);
                    FileLogger.Instance.LogToFile($"{message}{Environment.NewLine}{ex.Message}");
                }

                while (true)
                {
                    LogToConsole("Press Enter key to run again...");
                    if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                    {
                        Console.Clear();
                        break;
                    }
                }
            }
        }

        static void Process()
        {
            var result = string.Empty;
            NumberToText<int> numToTextObject = new NumberToText<int>();

            Read();
            numToTextObject.SetValues(low, high, a, b);
            result = numToTextObject.Process();

            LogToConsole(result);
            FileLogger.Instance.LogToFile(result);
        }

        static void Read()
        {
            Console.Write("Low:");
            low_input = Console.ReadLine();
            low = Convert.ToInt32(low_input);

            Console.Write("High:");
            high_input = Console.ReadLine();
            high = Convert.ToInt32(high_input);

            Console.Write("A:");
            a_input = Console.ReadLine();
            a = Convert.ToInt32(a_input);

            Console.Write("B:");
            b_input = Console.ReadLine();
            b = Convert.ToInt32(b_input);
        }

        static void LogToConsole(string message)
        {
            Console.WriteLine();
            Console.WriteLine(message);
        }
    }
}
