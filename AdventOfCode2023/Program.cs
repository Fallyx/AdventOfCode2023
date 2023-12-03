using AdventOfCode2023.Day01;
using System.Diagnostics;

namespace AdventOfCode2023
{
    internal class Program
    {
        private static readonly Stopwatch stopwatch = new();

        static void Main(string[] args)
        {
            string? input;
            if (args.Length != 0) 
            {
                input = args[0];
            }
            else
            {
                Console.WriteLine("Run a day [1-25]:");
                input = Console.ReadLine();
                Console.WriteLine("-----------------");
            }

            bool success = int.TryParse(input, out int day);
            if (!success) return;

            RunDay(day);
        }

        private static void RunDay(int day)
        {
            stopwatch.Start();

            switch (day)
            {
                case 1:
                    Day01.Day01.Task1and2();
                    break;
                case 2:
                    Day02.Day02.Task1and2();
                    break;
                case 3:
                    Day03.Day03.Task1and2();
                    break;
                default:
                    break;
            }

            stopwatch.Stop();
            Console.WriteLine($"Day {day:D2} finished in {stopwatch.Elapsed}");
        }
    }
}
