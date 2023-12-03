using System.Numerics;

namespace AdventOfCode2023.Day03;

internal class Day03
{
    const string inputPath = @"Day03/Input.txt";

    internal static void Task1and2()
    {
        List<String> lines = [.. File.ReadAllLines(inputPath)];
        Dictionary<Vector2, List<int>> gears = [];
        int partNumSum = 0;

        for (int y = 0; y < lines.Count; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                if (!Char.IsDigit(lines[y][x])) continue;

                string number = $"{lines[y][x]}";
                int numLength = 0;
                bool numEnded = false;

                while (!numEnded)
                {
                    numLength++;
                    if (x + numLength < lines[y].Length && Char.IsDigit(lines[y][x + numLength]))
                        number += lines[y][x + numLength];
                    else
                        numEnded = true;
                }

                int subStringStart = x - 1 >= 0 ? x - 1 : 0;
                int subStringEnd = (x + numLength + 1 < lines[y].Length ? x + numLength + 1 : x + numLength) - subStringStart;
                string lineTop = (y - 1 >= 0 ? lines[y - 1].Substring(subStringStart, subStringEnd) : string.Empty);
                string lineMiddle = lines[y].Substring(subStringStart, subStringEnd);
                string lineBottom = (y + 1 < lines.Count ? lines[y + 1].Substring(subStringStart, subStringEnd) : string.Empty);

                (bool possible, char symbol, int xRelative, int yRelative) parsedNumber = IsPartNumber(lineTop, lineMiddle, lineBottom);

                if (parsedNumber.possible)
                {
                    int numberInt = int.Parse(number);
                    partNumSum += numberInt;
                    if (parsedNumber.symbol == '*')
                    {
                        if (x - 1 < 0) parsedNumber.xRelative++;
                        Vector2 gearPos = new Vector2(x + parsedNumber.xRelative, y + parsedNumber.yRelative);
                        if (gears.ContainsKey(gearPos))
                            gears[gearPos].Add(numberInt);
                        else
                            gears.Add(gearPos, [numberInt]);
                    }
                }

                x += numLength;
            }
        }

        Console.WriteLine($"Task 1: {partNumSum}");

        int gearScore = 0;

        foreach (KeyValuePair<Vector2, List<int>> gear in gears)
        {
            if (gear.Value.Count == 2)
                gearScore += gear.Value.Aggregate(1, (a, b) => a * b);
        }

        Console.WriteLine($"Task 2: {gearScore}");
    }

    private static (bool possible, char symbol, int xRelative, int yRelative) IsPartNumber(string lineTop, string lineMiddle, string lineBototm)
    {
        for (int x = 0; x < lineTop.Length; x++)
        {
            if (lineTop[x] != '.' && !Char.IsDigit(lineTop[x])) 
                return (true, lineTop[x], x - 1, -1);
        }

        for (int x = 0; x < lineMiddle.Length; x++)
        {
            if (lineMiddle[x] != '.' && !Char.IsDigit(lineMiddle[x])) 
                return (true, lineMiddle[x], x - 1, 0);
        }

        for (int x = 0; x < lineBototm.Length; x++)
        {
            if (lineBototm[x] != '.' && !Char.IsDigit(lineBototm[x]))
                return (true, lineBototm[x], x - 1, 1);
        }

        return (false, '.', int.MinValue, int.MinValue);
    }
}
