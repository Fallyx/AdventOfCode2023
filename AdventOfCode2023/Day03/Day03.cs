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

                (bool possible, List<Vector2> relativeCoords) parsedNumber = IsPartNumber(lineTop, lineMiddle, lineBottom);

                if (parsedNumber.possible)
                {
                    int numberInt = int.Parse(number);
                    partNumSum += numberInt;
                    foreach (Vector2 coord in parsedNumber.relativeCoords)
                    {
                        int leftEdgeMod = (x - 1 < 0 ? 1 : 0);
                        Vector2 gearPos = new Vector2(x + coord.X + leftEdgeMod, y + coord.Y);
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

        double gearScore = 0;

        foreach (KeyValuePair<Vector2, List<int>> gear in gears)
        {
            if (gear.Value.Count == 2)
                gearScore += gear.Value.Aggregate(1, (a, b) => a * b);
        }

        Console.WriteLine($"Task 2: {gearScore}");
    }

    private static (bool possible, List<Vector2> relativeCoords) IsPartNumber(string lineTop, string lineMiddle, string lineBototm)
    {
        List<Vector2> relativeCoords = [];
        bool hasSymbols = false;

        for (int x = 0; x < lineTop.Length; x++)
        {
            if (lineTop[x] != '.' && !Char.IsDigit(lineTop[x]))
            {
                hasSymbols = true;
                if (lineTop[x] == '*')
                    relativeCoords.Add(new Vector2(x - 1, -1));
            }
                
        }

        for (int x = 0; x < lineMiddle.Length; x++)
        {
            if (lineMiddle[x] != '.' && !Char.IsDigit(lineMiddle[x]))
            {
                hasSymbols = true;
                if (lineMiddle[x] == '*')
                    relativeCoords.Add(new Vector2(x - 1, 0));
            }
        }

        for (int x = 0; x < lineBototm.Length; x++)
        {
            if (lineBototm[x] != '.' && !Char.IsDigit(lineBototm[x]))
            {
                hasSymbols = true;
                if (lineBototm[x] == '*')
                    relativeCoords.Add(new Vector2(x - 1, 1));
            }
        }

        return (hasSymbols, relativeCoords);
    }
}
