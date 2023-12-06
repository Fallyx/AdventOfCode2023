namespace AdventOfCode2023.Day06;

internal class Day06
{
    const string inputPath = @"Day06/Input.txt";

    internal static void Task1()
    {
        List<String> lines = [.. File.ReadAllLines(inputPath)];
        List<int> time = lines[0].Split(":", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[1].Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse).ToList();
        List<int> distanceToBeat = lines[1].Split(":", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[1].Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse).ToList();
        long numberScore = 1;

        for (int i = 0; i < time.Count; i++)
        {
            long raceScore = CalculateRace(time[i], distanceToBeat[i]);

            if (raceScore > 0) numberScore *= raceScore;
        }

        Console.WriteLine($"Task 1: {numberScore}");

        long timeTask2 = int.Parse(lines[0].Split(":")[1].Replace(" ", ""));
        long distanceToBeatTask2 = long.Parse(lines[1].Split(":")[1].Replace(" ", ""));

        Console.WriteLine($"Task 2: {CalculateRace(timeTask2, distanceToBeatTask2)}");
    }

    private static long CalculateRace(long time, long distanceToBeat)
    {
        long raceScore = 0;
        for (long x = 1; x < time; x++)
        {
            if (distanceToBeat < x * (time - x))
                raceScore++;
        }

        return raceScore;
    }
}