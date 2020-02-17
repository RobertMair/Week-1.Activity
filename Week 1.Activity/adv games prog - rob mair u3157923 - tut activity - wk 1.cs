using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week_1.Activity
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Week_1.Activity";

            // *********    MAIN PROGRAM LOOP   *********

            // Declare and initialise program globals.
            bool endProgram = false;
            while (!endProgram)
            {
                ProgramHeader(); // Render program header text.
                uint[]
                    userNumbers =
                        GetUserNumbers(); // Allows user to define number of positive numbers to enter and then enter them.
                Tuple<List<uint>, List<uint>>
                    tuple = OddEvenNumber(
                        userNumbers); // Determines which numbers occur ODD and EVEN number of times in user list and returns two lists accordingly.

                // Display numbers that occurred an odd number of times in the user list.
                List<uint> oddCountNums = tuple.Item1;
                ListPrint(oddCountNums, "ODD");

                // Display numbers that occurred an even number of times in the user list.
                List<uint> evenCountNums = tuple.Item2;
                ListPrint(evenCountNums, "EVEN");

                endProgram = EndProgMenu(); // End program user menu.
            } // endwhile(!endProgram).

            Console.Write("\nThank you for using this program, press any key to fully exit...");
            Console.ReadKey();
        }


        // *********    METHODS    *********

        // Program header.
        static void ProgramHeader()
        {
            // Program title.
            Console.WriteLine("\nAdvanced Games Programming (9746)");
            Console.WriteLine("Week 1 Programming Activity");
            Console.WriteLine("Dev: Robert Mair (SID: u3157923)");

            // Program purpose.
            Console.WriteLine(
                "\nIn an array containing positive integer numbers, there are numbers which occur odd and even number of times.");
            Console.WriteLine(
                "This program allows a user to enter an array length of their choice, then gets the array of positive numbers");
            Console.WriteLine("from the user and finds the numbers that occurred an odd and even number of times.");
        }

        // Obtain and sort positive integers from the user.
        static uint[] GetUserNumbers()
        {
            // Obtain how many positive integers the user wishes to input.
            uint arraySize = NumInput("\nPlease enter how many positive integer numbers you wish to enter --> ");
            Console.WriteLine("\nYou entered an array length of {0}.\n", arraySize);

            // Create array based on user size required.
            uint[] userNumbers = new uint[arraySize];

            // Populate array with unsigned integers entered by user.
            for (int i = 0; i < userNumbers.Length; i++)
            {
                userNumbers[i] = NumInput(string.Format("Please enter {0} of {1} positive integers --> \t", i + 1,
                    userNumbers.Length));
            }

            // Sort array from minimum to maximum numbers and display sorted array to console.
            Array.Sort(userNumbers);
            string text = "";
            Console.Write("\nYour numbers from minimum to maximum are: \t");
            for (int i = 0; i < userNumbers.Length; i++)
            {
                Console.Write(text + userNumbers[i]);
                text = (i == userNumbers.Length - 2) ? " and " : ", ";
            }
            Console.Write(".\n");
            return userNumbers;
        }

        // Algorithm to determine which numbers in the user list occurred an odd or even number of times.
        static Tuple<List<uint>, List<uint>> OddEvenNumber(uint[] userNumbers)
        {
            // The algorithm takes the first value in the array, then sequentially checks the next value against the previous value.
            // If the next value equals the previous value, a counter is incremented by one.
            // When the next value in the array does not equal the prior value in the array, if the counter is odd then the prior value
            // is added to the list for numbers occuring an odd number of times, however if the counter is even then the prior value
            // is added to the list for numbers occuring an even number of times.
            // If the algorithm is checking the last value in the array which doesn't equal the prior value in the array, it will automatically
            // add this value to the list for numbers occuring an odd number of times.

            List<uint>
                oddCountNums =
                    new List<uint>(); // List for numbers that occurred an odd number of times in the user list. 
            List<uint>
                evenCountNums =
                    new List<uint>(); // List for numbers that occurred an even number of times in the user list. 
            uint value = userNumbers[0];
            int count = 0;

            for (int i = 0; i < userNumbers.Length; i++)
            {
                if (userNumbers[i] == value)
                {
                    count++;
                }
                // Console.WriteLine("i = {0}, value = {1}, userNumber[{2}] = {3}, count = {4}", i, value, i, userNumbers[i], count); // For debugging purposes only.
                if (userNumbers[i] != value || i == userNumbers.Length - 1)
                {
                    if ((count % 2 != 0))
                    {
                        oddCountNums.Add(value);
                    }
                    else
                    {
                        evenCountNums.Add(value);
                    }

                    if (i == userNumbers.Length - 1 && userNumbers[i] != value)
                    {
                        oddCountNums.Add(userNumbers[i]);
                    }

                    count = 1;
                    value = userNumbers[i];
                }
            }
            return Tuple.Create(oddCountNums, evenCountNums);
        }

        // Test user input as a positive integer.
        static uint NumInput(string text)
        {
            bool testOK = false;
            string input;
            uint number;
            Console.Write(text);
            do
            {
                input = Console.ReadLine();
                if (UInt32.TryParse(input, out number) && number > 0)
                {
                    testOK = true;
                }
                else
                {
                    Console.Write("Input invalid, please try again --> ");
                }
            } while (testOK == false);
            return number;
        }

        // Display numbers that occurred an odd/even number of times in the user array.
        static void ListPrint(List<uint> numList, string text)
        {
            string plural = (numList.Count > 1) ? "s " : " ";
            Console.Write("\nNumber{0}entered an {1} number of times:  \t", plural, text);
            if (numList.Count == 0)
            {
                Console.WriteLine("No number!");
            }
            else
            {
                text = "";
                for (int i = 0; i < numList.Count; i++)
                {
                    Console.Write(text + numList[i]);
                    text = (i == numList.Count - 2) ? " and " : ", ";
                }
                Console.Write(".\n");
            }
        }

        // End program user menu.
        static bool EndProgMenu()
        {
            bool inputValid = false; // Used to test user input is valid.
            bool endProgram = false; // Used to determine if exiting the program or not.
            Console.WriteLine();
            while (!inputValid)
            {
                Console.Write("Please enter (R) Run program again or (X) eXit program ==> ");
                while (Console.KeyAvailable) Console.ReadKey(); //Clear buffer
                var getChar = Console.ReadKey();
                var menuChoice = char.ToLower(getChar.KeyChar);
                ClearLine();
                if (menuChoice == 'r')
                {
                    Console.Clear();
                    inputValid = true;
                }
                else if (menuChoice == 'x')
                {
                    endProgram = true;
                    break;
                }
                else
                {
                    Console.Write("Invalid input. ");
                }
            }
            return endProgram;
        }

        // Clear current console line.
        public static void ClearLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop - (Console.WindowWidth >= Console.BufferWidth ? 1 : 0));
        }
    }
}