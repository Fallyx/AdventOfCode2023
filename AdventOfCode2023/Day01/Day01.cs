namespace AdventOfCode2023.Day01;

internal class Day01
{
    const string inputPath = @"Day01/Input.txt";
    private static readonly Dictionary<string, int> letters = new()
    {
        { "one", 1 }, { "1", 1 },
        { "two", 2 }, { "2", 2 },
        { "three", 3 }, { "3", 3 },
        { "four", 4 }, { "4", 4 },
        { "five", 5 }, { "5", 5 },
        { "six", 6 }, { "6", 6 },
        { "seven", 7 }, { "7", 7 },
        { "eight", 8 }, { "8", 8 },
        { "nine", 9 }, { "9", 9 }
    };

    internal static void Task1and2() 
    {
        List<String> lines = [.. File.ReadAllLines(inputPath)];

        int calibValueTask1 = 0;
        int calibValueTask2 = 0;

        foreach (String line in lines)
        {
            int first = line.First(Char.IsDigit) - 48;
            int last = line.Last(Char.IsDigit) - 48;

            calibValueTask1 += (first * 10 + last);
            calibValueTask2 += StringToDigits(line);
        }

        Console.WriteLine($"Task 1: {calibValueTask1}");
        Console.WriteLine($"Task 2: {calibValueTask2}");
    }

    private static int StringToDigits(string line)
    {
        int idxFirst = int.MaxValue;
        int idxLast = int.MinValue;
        int first = 0;
        int last = 0;

        foreach(KeyValuePair<String, int> keyValue in letters)
        {
            int idx1 = line.IndexOf(keyValue.Key);
            int idx2 = line.LastIndexOf(keyValue.Key);

            if (idx1 >= 0 && idx1 < idxFirst)
            {
                idxFirst = idx1;
                first = keyValue.Value * 10;
            }

            if (idx2 >= 0 && idx2 > idxLast)
            {
                idxLast = idx2;
                last = keyValue.Value;
            }
        }

        return first + last;
    }
}
