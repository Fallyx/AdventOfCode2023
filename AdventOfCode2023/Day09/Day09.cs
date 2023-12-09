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

            int[] newNums = [1];
            while (!newNums.All(n => n == 0))
            {
                newNums = Sequence(oasis.Last());
                oasis.Add(newNums);
            }

            int extrapolatedNumberRight = 0;
            int extrapolatedNumberLeft = 0;
            for (int i = oasis.Count - 2; i >= 0; i--)
            {
                extrapolatedNumberRight += oasis[i].Last();
                extrapolatedNumberLeft = oasis[i].First() - extrapolatedNumberLeft;
                Console.WriteLine(extrapolatedNumberLeft);
            }

            sumPredictionValues += extrapolatedNumberRight;
            sumHistoryValues += extrapolatedNumberLeft;
        }

        Console.WriteLine($"Task 1: {sumPredictionValues}");
        Console.WriteLine($"Task 2: {sumHistoryValues}");
    }

    private static int[] Sequence(int[] numbers)
    {
        int[] newNumbers = new int[numbers.Length - 1];

        for (int i = 0; i < newNumbers.Length; i++)
        {
            newNumbers[i] = numbers[i + 1] - numbers[i];
        }

        return newNumbers;
    }
}
