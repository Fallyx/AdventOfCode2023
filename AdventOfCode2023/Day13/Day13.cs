namespace AdventOfCode2023.Day13;

internal class Day13
{
    const string inputPath = @"Day13/Input.txt";

    internal static void Task1and2()
    {
        List<String> lines = [.. File.ReadAllLines(inputPath)];
        char[,] pattern;
        int summarize = 0;
        int summarizeSmudge = 0;

        while (lines.Count > 0)
        {
            List<String> patternStrings = lines.TakeWhile(s => s.Length != 0).ToList();
            lines.RemoveRange(0, (patternStrings.Count + 1) > lines.Count ? patternStrings.Count : patternStrings.Count + 1);
            pattern = new char[patternStrings.Count, patternStrings[0].Length];

            for(int y = 0; y < patternStrings.Count; y++)
            {
                for (int x = 0; x < patternStrings[0].Length; x++)
                {
                    pattern[y, x] = patternStrings[y][x];
                }
            }

            summarize += GetVerticalLineIndexSmudge(pattern, 0);
            summarize += 100 * GetHorizontalLineIndexSmudge(pattern, 0);

            summarizeSmudge += GetVerticalLineIndexSmudge(pattern, 1);
            summarizeSmudge += 100 * GetHorizontalLineIndexSmudge(pattern, 1);
        }

        Console.WriteLine($"Task 1: {summarize}");
        Console.WriteLine($"Task 2: {summarizeSmudge}");
    }

    private static int GetVerticalLineIndexSmudge(char[,] pattern, int smudgeAmount)
    {
        for (int i = 0; i < pattern.GetLength(1) - 1; i++)
        {
            int smudgeCounter = 0;
            char[] col1 = Enumerable.Range(0, pattern.GetLength(0)).Select(x => pattern[x, i]).ToArray();
            char[] col2 = Enumerable.Range(0, pattern.GetLength(0)).Select(x => pattern[x, i + 1]).ToArray();
            smudgeCounter += IsSmudge(col1, col2);

            if (smudgeCounter <= smudgeAmount)
            {
                int max = int.Min(i, pattern.GetLength(1) - 1 - (i + 1));
                bool perfectReflection = true;

                for (int a = 1; a <= max; a++)
                {
                    col1 = Enumerable.Range(0, pattern.GetLength(0)).Select(x => pattern[x, i - a]).ToArray();
                    col2 = Enumerable.Range(0, pattern.GetLength(0)).Select(x => pattern[x, i + 1 + a]).ToArray();
                    smudgeCounter += IsSmudge(col1, col2);

                    if (smudgeCounter > smudgeAmount)
                    {
                        perfectReflection = false;
                        break;
                    }
                }

                if (perfectReflection && smudgeCounter == smudgeAmount)
                    return i + 1;
            }
        }

        return 0;
    }

    private static int GetHorizontalLineIndexSmudge(char[,] pattern, int smudgeAmount)
    {
        for (int i = 0; i < pattern.GetLength(0) - 1; i++)
        {
            int smudgeCounter = 0;
            char[] row1 = Enumerable.Range(0, pattern.GetLength(1)).Select(x => pattern[i, x]).ToArray();
            char[] row2 = Enumerable.Range(0, pattern.GetLength(1)).Select(x => pattern[i + 1, x]).ToArray();
            smudgeCounter += IsSmudge(row1, row2);

            if (smudgeCounter <= smudgeAmount)
            {
                int max = int.Min(i, pattern.GetLength(0) - 1 - (i + 1));
                bool perfectReflection = true;

                for (int a = 1; a <= max; a++)
                {
                    row1 = Enumerable.Range(0, pattern.GetLength(1)).Select(x => pattern[i - a, x]).ToArray();
                    row2 = Enumerable.Range(0, pattern.GetLength(1)).Select(x => pattern[i + 1 + a, x]).ToArray();
                    smudgeCounter += IsSmudge(row1, row2);

                    if (smudgeCounter > smudgeAmount)
                    {
                        perfectReflection = false;
                        break;
                    }
                }

                if (perfectReflection && smudgeCounter == smudgeAmount)
                    return i + 1;
            }
        }

        return 0;
    }

    private static int IsSmudge(char[] arr1, char[] arr2)
    {
        int diffCounter = 0;
        for(int i = 0; i < arr1.Length; i++)
        {
            if (arr1[i] != arr2[i])
                diffCounter++;
        }

        return diffCounter;
    }
}
