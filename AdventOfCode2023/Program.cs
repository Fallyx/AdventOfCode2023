﻿using AdventOfCode2023.Day01;
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
                case 4:
                    Day04.Day04.Task1and2();
                    break;
                case 5:
                    Day05.Day05.Task1and2();
                    break;
                case 6:
                    Day06.Day06.Task1and2();
                    break;
                case 7:
                    Day07.Day07.Task1and2();
                    break;
                case 8:
                    Day08.Day08.Task1and2();
                    break;
                case 9:
                    Day09.Day09.Task1and2();
                    break;
                case 10:
                    Day10.Day10.Task1and2();
                    break;
                case 11:
                    Day11.Day11.Task1and2();
                    break;
                case 12:
                    Day12.Day12.Task1and2();
                    break;
                case 13:
                    Day13.Day13.Task1and2();
                    break;
                case 14:
                    Day14.Day14.Task1();
                    Day14.Day14.Task2();
                    break;
                case 15:
                    Day15.Day15.Task1and2();
                    break;
                case 16:
                    Day16.Day16.Task1and2();
                    break;
                default:
                    break;
            }

            stopwatch.Stop();
            Console.WriteLine($"Day {day:D2} finished in {stopwatch.Elapsed}");
        }
    }
}
