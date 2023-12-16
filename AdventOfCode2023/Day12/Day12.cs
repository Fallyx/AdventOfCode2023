namespace AdventOfCode2023.Day12;

internal class Day12
{
    const string inputPath = @"Day12/Input.txt";
    private static readonly Dictionary<String, long> visited = [];

    internal static void Task1and2()
    {
        List<String> lines = [.. File.ReadAllLines(inputPath)];
        long sumArrangementsTask1 = 0;
        long sumArrangementsTask2 = 0;

        foreach (string line in lines)
        {
            string[] split = line.Split(' ');
            Springs springs = new(split[0], split[1].Split(',').Select(int.Parse).ToList());

            long arrangements = GetArrangements(springs);
            sumArrangementsTask1 += arrangements;

            springs = new(split[0], split[1].Split(',').Select(int.Parse).ToList(), 5);
            arrangements = GetArrangements(springs);
            sumArrangementsTask2 += arrangements;
        }

        Console.WriteLine($"Task 1: {sumArrangementsTask1}");
        Console.WriteLine($"Task 2: {sumArrangementsTask2}");
    }

    private static long GetArrangements(Springs springs)
    {
        string key = springs.ToString();
        if (visited.ContainsKey(key))
        {
            return visited[key];
        }

        long arrangements = IsValid(springs);
        visited.Add(key, arrangements);
        return arrangements;
    }


    private static long IsValid(Springs springs)
    {
        while(true)
        {
            if (springs.DamagedSprings.Count == 0)
                return springs.Spring.Contains('#') ? 0 : 1;
            else if (springs.Spring.Length == 0)
                return 0;
            else if (springs.Spring.StartsWith('.'))
                springs.Spring = springs.Spring[1..];
            else if (springs.Spring.StartsWith('?'))
            {
                long arrangements = 0;
                Springs broken = new($"#{springs.Spring[1..]}", [.. springs.DamagedSprings]);
                arrangements += GetArrangements(broken);
                Springs working = new($".{springs.Spring[1..]}", [.. springs.DamagedSprings]);
                arrangements += GetArrangements(working);
                return arrangements;
            }
            else if (springs.Spring.StartsWith('#'))
            {
                if (springs.DamagedSprings.Count == 0)
                    return 0; 
                else if (springs.Spring.Length < springs.DamagedSprings[0])
                    return 0;
                else if (springs.Spring.Take(springs.DamagedSprings[0]).Contains('.'))
                    return 0;
                else if (springs.DamagedSprings.Count > 1)
                {
                    if (springs.Spring.Take(springs.DamagedSprings[0] + 1).Last() == '#')
                        return 0;
                    else if (springs.Spring.Length < springs.DamagedSprings[0] + 1)
                        return 0;

                    springs.Spring = springs.Spring[(springs.DamagedSprings[0] + 1)..];
                    springs.DamagedSprings.RemoveAt(0);
                }
                else
                {
                    springs.Spring = springs.Spring[springs.DamagedSprings[0]..];
                    springs.DamagedSprings.RemoveAt(0);
                }
            }
        }
    }

    private class Springs
    {
        public String Spring { get; set; }
        public List<int> DamagedSprings { get; set; }

        public Springs(string row, List<int> damaged, int unfold = 1)
        {
            if (unfold == 1)
            {
                Spring = row;
                DamagedSprings = damaged;
            }
            else
            {
                Spring = string.Join('?', Enumerable.Repeat(row, unfold));
                DamagedSprings = Enumerable.Repeat(damaged, unfold).SelectMany(d => d).ToList();
            }
            
        }

        public override string ToString()
        {
            return $"{Spring}_{string.Join('-', DamagedSprings)}";
        }
    }
}
