namespace AdventOfCode2023.Day09;

internal class Day09
{
    const string inputPath = @"Day09/Input.txt";

    internal static void Task1and2()
    {
        List<String> lines = [.. File.ReadAllLines(inputPath)];
        int sumPredictionValues = 0;
        int sumHistoryValues = 0;

        foreach (String line in lines)
        {
            List<int[]> oasis = [];
            oasis.Add(line.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse).ToArray());

            while (!oasis.Last().All(n => n == 0))
            {
                int[] newNums = new int[oasis.Last().Length - 1];
                for (int i = 0; i < newNums.Length; i++)
                {
                    newNums[i] = oasis.Last()[i + 1] - oasis.Last()[i];
                }
                oasis.Add(newNums);
            }

            int extrapolatedNumberRight = 0;
            int extrapolatedNumberLeft = 0;
            for (int i = oasis.Count - 2; i >= 0; i--)
            {
                extrapolatedNumberRight += oasis[i].Last();
                extrapolatedNumberLeft = oasis[i].First() - extrapolatedNumberLeft;
            }

            sumPredictionValues += extrapolatedNumberRight;
            sumHistoryValues += extrapolatedNumberLeft;
        }

        Console.WriteLine($"Task 1: {sumPredictionValues}");
        Console.WriteLine($"Task 2: {sumHistoryValues}");
    }
}
