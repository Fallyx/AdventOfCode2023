using System.Numerics;

namespace AdventOfCode2023.Day03;

internal class Day03
{
    const string inputPath = @"Day03/Input.txt";

    internal static void Task1and2()
    {
        List<String> lines = [.. File.ReadAllLines(inputPath)];
        Dictionary<string, List<Vector2>> parts = [];
        Dictionary<Vector2, char> symbols = [];

        for (int y = 0; y < lines.Count; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                if (lines[y][x] == '.') continue;

                if (!Char.IsDigit(lines[y][x]))
                {
                    symbols.Add(new Vector2(x, y), lines[y][x]);
                } 
                else
                {
                    string number = "" + lines[y][x];

                    int numLength = 0;
                    bool numEnded = false;

                    while (!numEnded)
                    {
                        numLength++;
                        if (x + numLength < lines[y].Length && Char.IsDigit(lines[y][x + numLength]))
                            number = number + lines[y][x + numLength];
                        else
                            numEnded = true;
                    }

                    List<Vector2> adjacents = [];

                    for (int adjY = y - 1; adjY <= y + 1; adjY++)
                    {
                        for (int adjX = x - 1; adjX <= x + numLength; adjX++)
                        {
                            if (adjY == y && adjX >= x && adjX <= x + numLength - 1) continue;

                            adjacents.Add(new Vector2(adjX, adjY));
                        }
                    }

                    parts.Add($"{number}-{x}:{y}", adjacents);
                    x += numLength - 1;
                }
            }
        }

        int partNumSum = 0;

        foreach (KeyValuePair<string, List<Vector2>> part in parts)
        {
            foreach(KeyValuePair<Vector2, char> symbol in symbols)
            {
                if (part.Value.Contains(symbol.Key))
                {
                    partNumSum += int.Parse(part.Key.Split('-')[0]);
                    break;
                }
            }
        }

        Console.WriteLine($"Task 1: {partNumSum}");

        int gearSum = 0;

        foreach (KeyValuePair<Vector2, char> symbol in symbols)
        {
            int gearRatio = 1;
            int gearAdj = 0;
            foreach (KeyValuePair<string, List<Vector2>> part in parts)
            {
                if (part.Value.Contains(symbol.Key))
                {
                    gearAdj++;
                    gearRatio *= int.Parse(part.Key.Split('-')[0]);
                }
            }

            if (gearAdj == 2) gearSum += gearRatio;
        }

        Console.WriteLine($"Task 2: {gearSum}");
    }
}
