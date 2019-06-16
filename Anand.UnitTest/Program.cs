using System;

namespace Anand.UnitTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine(" Testing... to quit please enter Q");
            Console.WriteLine();

            ValidateMonthYear("Jan-2019", "Pass");
            ValidateMonthYear("Ja-2019", "Fail");
            ValidateMonthYear("Feb-2019", "Pass");
            ValidateMonthYear("01-2019", "Fail");

            if (Console.ReadLine().ToLower() == "q") { Environment.Exit(0); }
        }

        private static void ValidateMonthYear(string monthYear, string expectedResult)
        {
            if (MASClient.MAS.ValidateMonthYear(monthYear, out DateTime dt) == (expectedResult == "Pass"))
            {
                Console.WriteLine(" ValidateMonthYear: " + monthYear + "\t Expected Result: " + expectedResult + "\t Test : Passed");
            }

            else
            {
                Console.WriteLine(" ValidateMonthYear: " + monthYear + "\t Expected Result: " + expectedResult + "\t Test : Failed");
            }
        }
    }
}
