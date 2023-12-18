using System.Text;

namespace AdventOfCode2023.Day14;

internal class Day14
{
    const string inputPath = @"Day14/Input.txt";

    internal static void Task1()
    {
        List<String> lines = [.. File.ReadAllLines(inputPath)];
        char[,] platform = new char[lines.Count, lines[0].Length];

        for (int y = 0; y < lines.Count; y++)
        {
            for (int x = 0; x < lines[0].Length; x++)
            {
                platform[y, x] = lines[y][x];
            }
        }

        RollInDirectionNorth(platform);

        int totalLoad = 0;
        for (int y = 0; y < platform.GetLength(0); y++)
        {
            for (int x = 0; x < platform.GetLength(1); x++)
            {
                if (platform[y, x] == 'O')
                    totalLoad += platform.GetLength(0) - y;
            }
        }

        Console.WriteLine($"Task 1: {totalLoad}");
    }

    internal static void Task2()
    {
        List<String> lines = [.. File.ReadAllLines(inputPath)];
        char[,] platform = new char[lines.Count, lines[0].Length];
        List<string> history = [];
        List<char[,]> historyPattern = [];
        int maxCycles = 1000000000;
        int patternCheckCycles = 100;

        for (int y = 0; y < lines.Count; y++)
        {
            for (int x = 0; x < lines[0].Length; x++)
            {
                platform[y, x] = lines[y][x];
            }
        }

        for (int cycles = 0; cycles < patternCheckCycles; cycles++)
        {
            RollInDirectionNorth(platform);
            RollInDirectionWest(platform);
            RollInDirectionSouth(platform);
            RollInDirectionEast(platform);

            history.Add(PlatformToHistory(platform));
            historyPattern.Add(platform);
        }

        int patternLength = FindPatternLength(history);


        platform = historyPattern[history.Count - 1 - ((maxCycles - patternCheckCycles) % 7)];
        int totalLoad = 0;
        for (int y = 0; y < platform.GetLength(0); y++)
        {
            for (int x = 0; x < platform.GetLength(1); x++)
            {
                if (platform[y, x] == 'O')
                    totalLoad += platform.GetLength(0) - y;
            }
        }

        Console.WriteLine($"Task 2: {totalLoad}");
    }

    private static void RollInDirectionNorth(char[,] platform)
    {
        for (int i = 0; i < platform.GetLength(1); i++)
        {
            char[] arr = Enumerable.Range(0, platform.GetLength(0)).Select(x => platform[x, i]).ToArray();
            int lastPosRock = -1;
            for (int a = 0; a < arr.Length; a++)
            {
                if (arr[a] == 'O')
                {
                    arr[a] = '.';
                    arr[lastPosRock + 1] = 'O';
                    platform[a, i] = '.';
                    platform[lastPosRock + 1, i] = 'O';
                    lastPosRock++;
                }
                else if (arr[a] == '#')
                    lastPosRock = a;
            }
        }
    }

    private static void RollInDirectionSouth(char[,] platform)
    {
        for (int i = platform.GetLength(1) - 1; i >= 0; i--)
        {
            char[] arr = Enumerable.Range(0, platform.GetLength(0)).Select(x => platform[x, i]).ToArray();
            int lastPosRock = platform.GetLength(0);
            for (int a = arr.Length - 1; a >= 0; a--)
            {
                if (arr[a] == 'O')
                {
                    arr[a] = '.';
                    arr[lastPosRock - 1] = 'O';
                    platform[a, i] = '.';
                    platform[lastPosRock - 1, i] = 'O';
                    lastPosRock--;
                }
                else if (arr[a] == '#')
                    lastPosRock = a;
            }
        }
    }

    private static void RollInDirectionWest(char[,] platform)
    {
        for (int i = 0; i < platform.GetLength(0); i++)
        {
            char[] arr = Enumerable.Range(0, platform.GetLength(1)).Select(x => platform[i, x]).ToArray();
            int lastPosRock = -1;

            for (int a = 0; a < arr.Length; a++)
            {
                if (arr[a] == 'O')
                {
                    arr[a] = '.';
                    arr[lastPosRock + 1] = 'O';
                    platform[i, a] = '.';
                    platform[i, lastPosRock + 1] = 'O';
                    lastPosRock++;
                }
                else if (arr[a] == '#')
                    lastPosRock = a;
            }
        }
    }

    private static void RollInDirectionEast(char[,] platform)
    {
        for (int i = platform.GetLength(0) - 1; i >= 0; i--)
        {
            char[] arr = Enumerable.Range(0, platform.GetLength(1)).Select(x => platform[i, x]).ToArray();
            int lastPosRock = platform.GetLength(1);

            for (int a = arr.Length - 1; a >= 0; a--)
            {
                if (arr[a] == 'O')
                {
                    arr[a] = '.';
                    arr[lastPosRock - 1] = 'O';
                    platform[i, a] = '.';
                    platform[i, lastPosRock - 1] = 'O';
                    lastPosRock--;
                }
                else if (arr[a] == '#')
                    lastPosRock = a;
            }
        }
    }

    private static string PlatformToHistory(char[,] platform)
    {
        StringBuilder sb = new();
        for (int i = 0; i < platform.GetLength(0); i++)
        {
            char[] arr = Enumerable.Range(0, platform.GetLength(1)).Select(x => platform[i, x]).ToArray();
            sb.Append(new String(arr));
        }

        return sb.ToString();
    }

    private static int FindPatternLength(List<string> history)
    {
        int length = 1;
        string lastHistoryEntry = history[^1];

        for (int i = history.Count - 2; i >= 0; i--)
        {
            if (history[i] == lastHistoryEntry)
                return length;
            else
                length++;
        }

        return length;
    }
}
