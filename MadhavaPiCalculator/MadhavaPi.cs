using ExtendedNumerics;
using System;
using System.Text;
using static System.Console;
using static System.Math;

namespace CalculatePi
{
    class MadhavaPi
    {
        static int position = 1;
        static ulong truncFactor = (ulong)Pow(10, position);
        static void Main(string[] args)
        {
            ForegroundColor = ConsoleColor.Green;
            StringBuilder output = new StringBuilder();

            decimal reference = 3.1415926535897M;
            decimal pi = (decimal)Sqrt(12);
            int denominator1 = 3;
            int denominator2 = 3;
            int denom2Power = 1;
            int numberOfTerms = 1;
            int solvedCount = 0;

            WriteLine("Enter number of decimal places to calculate Pi to (maximum 13):");
            int numberOfPlaces = int.Parse(ReadLine());
            if (numberOfPlaces > 13) { numberOfPlaces = 13; }
            string[] outputStatements = new string[numberOfPlaces];

            while (solvedCount < numberOfPlaces)
            {
                decimal term = (decimal)Sqrt(12) * (1 / (decimal)(denominator1 * Pow(denominator2, denom2Power)));
                if (numberOfTerms % 2 == 1)
                {
                    term *= -1;
                }
                decimal previousPi = pi;
                pi += term;

                output.Append($"Pi: {pi,5:F16}       Number of terms: {numberOfTerms,5}\n");
                denominator1 += 2;
                ++denom2Power;

                if (MyTruncate(pi) == MyTruncate(reference)
                    && MyTruncate(previousPi) == MyTruncate(reference))
                {
                    outputStatements[position - 1] =
                        ($"pi calculated to {position} decimal places in {numberOfTerms} terms.");
                    truncFactor *= 10;
                    if (position < outputStatements.Length)
                    {
                        ++position;
                    }
                    ++solvedCount;
                }
                ++numberOfTerms;
            }
            WriteLine(output);
            WriteLine($"\n\n");
            WriteLine("Calculated using Madhava's transformation:\n");
            foreach (string s in outputStatements)
            {
                WriteLine(s);
            }
        }
        static decimal MyTruncate(decimal number)
        {
            BigDecimal numerator = Truncate(number * truncFactor);
            return (decimal) numerator / truncFactor;
        }
    }
}
