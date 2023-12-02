using System.Text.RegularExpressions;

namespace AdventOfCode2023.Day02;

internal class Day02
{
    const string inputPath = @"Day02/Input.txt";

    internal static void Task1and2()
    {
        List<String> lines = [.. File.ReadAllLines(inputPath)];
        Regex blueRgx = new(@"\d+ blue");
        Regex redRgx = new(@"\d+ red");
        Regex greenRgx = new(@"\d+ green");
        int maxBlue = 14;
        int maxRed = 12;
        int maxGreen = 13;
        int sumIds = 0;
        int sumPowerCubes = 0;
    
        foreach(String line in lines)
        {
            (bool possible, int minCubes) blue = ReadCubes(line, blueRgx, maxBlue, 5);
            (bool possible, int minCubes) red = ReadCubes(line, redRgx, maxRed, 4);
            (bool possible, int minCubes) green = ReadCubes(line, greenRgx, maxGreen, 5);

            if (blue.possible && red.possible && green.possible) sumIds += int.Parse(line.Substring(5, line.IndexOf(':') - 5));
            sumPowerCubes += (blue.minCubes * red.minCubes * green.minCubes);
        }

        Console.WriteLine($"Task 1: {sumIds}");
        Console.WriteLine($"Task 2: {sumPowerCubes}");
    }

    private static (bool possible, int minCubes) ReadCubes(String line, Regex rgx, int max, int snipLength)
    {
        bool possible = true;
        int minCubes = 0;
        foreach (Match match in rgx.Matches(line))
        {
            int amountCubes = int.Parse(match.Value.Substring(0, match.Value.Length - snipLength));
            if (possible && max < amountCubes) possible = false;
            if (minCubes < amountCubes) minCubes = amountCubes;
        }

        return (possible, minCubes);
    }
}
