namespace AdventOfCode2023.Day04;

internal class Day04
{
    const string inputPath = @"Day04/Input.txt";

    internal static void Task1and2()
    {
        List<String> lines = [.. File.ReadAllLines(inputPath)];
        int[] scratchcards = Enumerable.Repeat(1, lines.Count).ToArray();
        double points = 0;

        for (int i = 0; i < lines.Count; i++)
        {
            string[] numbers = lines[i].Split(":")[1].Split(" | ");
            List<string> winningNums = [.. numbers[0].Split(" ", StringSplitOptions.RemoveEmptyEntries)];
            List<string> cardNums = [.. numbers[1].Split(" ", StringSplitOptions.RemoveEmptyEntries)];
            int winners = winningNums.Where(x => cardNums.Contains(x)).Count();
            if (winners > 0)
            {
                points += Math.Pow(2, winners) / 2;
                for(int x = 1; x <= winners; x++)
                {
                    scratchcards[i + x] += scratchcards[i];
                }
            }
        }

        Console.WriteLine($"Task 1: {points}");
        Console.WriteLine($"Task 2: {scratchcards.Sum()}");
    }
}
