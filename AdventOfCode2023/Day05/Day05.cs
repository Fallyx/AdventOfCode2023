﻿namespace AdventOfCode2023.Day05;

internal class Day05
{
    const string inputPath = @"Day05/Input.txt";

    internal static void Task1and2()
    {
        List<String> lines = [.. File.ReadAllLines(inputPath)];
        List<long> seeds = lines[0].Split("seeds: ")[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(Int64.Parse).ToList();
        List<AlmanacMap>[] almanacMaps = [ [], [], [], [], [], [], [] ];
        int almanacMapIdx = -1;

        for (int i = 2; i < lines.Count; i++)
        {
            if (lines[i].Length == 0) continue;
            if (lines[i].Contains(" map:"))
            {
                almanacMapIdx++;
                continue;
            }

            AlmanacMap almanacMap = new(lines[i]);
            almanacMaps[almanacMapIdx].Add(almanacMap);
        }

        long minLocation = int.MaxValue;

        foreach(long seed in seeds)
        {
            long currentValue = seed;

            for(int i = 0; i <  almanacMaps.Length; i++)
            {
                AlmanacMap map = almanacMaps[i].FirstOrDefault(x => x.SourceStart <= currentValue && x.SourceEnd >= currentValue, new AlmanacMap());
                currentValue += map.Value;
            }

            if (minLocation > currentValue) minLocation = currentValue;
        }

        Console.WriteLine($"Task 1: {minLocation}");

        minLocation = int.MaxValue;

        for (int i = 0; i < seeds.Count; i += 2)
        {
            List<SeedRange> seedsTask2 = [new(seeds[i], seeds[i] + seeds[i + 1])];

            for (int x = 0; x < almanacMaps.Length; x++)
            {
                List<SeedRange> splitList = [];

                foreach (SeedRange sr in seedsTask2)
                {
                    foreach (AlmanacMap map in almanacMaps[x])
                    {
                        if (sr.Start > map.SourceEnd || sr.End < map.SourceStart)
                        {
                            splitList.Add(sr);
                        }
                        else if (sr.Start > map.SourceStart && sr.End < map.SourceEnd)
                        {
                            splitList.Add(new(sr.Start += map.Value, sr.End += map.Value));
                        }
                        else if (sr.Start > map.SourceStart && sr.End > map.SourceEnd)
                        {
                            splitList.Add(new(sr.Start + map.Value, map.SourceEnd + map.Value));
                            splitList.Add(new(map.SourceEnd + 1, sr.End));
                        }
                        else if (sr.Start < map.SourceStart && sr.End < map.SourceEnd)
                        {
                            splitList.Add(new(sr.Start, map.SourceStart - 1));
                            splitList.Add(new(map.SourceStart + map.Value, sr.End + map.Value));
                        }
                        else if (sr.Start < map.SourceStart && sr.End > map.SourceEnd)
                        {
                            splitList.Add(new(sr.Start, map.SourceStart - 1));
                            splitList.Add(new(map.SourceStart + map.Value, map.SourceEnd + map.Value));
                            splitList.Add(new(map.SourceEnd + 1, sr.End));
                        }
                    }
                }

                seedsTask2 = splitList.Distinct().ToList();
            }

            long minVal = seedsTask2.Min(x => x.Start);
            if (minVal != 0 && minLocation > minVal)
            {
                minLocation = minVal;
            }
        }

        Console.WriteLine($"Task 2: {minLocation}");
    }

    private class AlmanacMap
    {
        public Int64 DestinationStart { get; set; } = 0;
        public Int64 DestinationEnd { get; set; } = 0;
        public Int64 SourceStart { get; set; } = 0;
        public Int64 SourceEnd { get; set; } = 0;
        public Int64 Value { get; set; } = 0;

        public AlmanacMap() { }

        public AlmanacMap(string line)
        {
            long[] values = line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(Int64.Parse).ToArray();

            DestinationStart = values[0];
            DestinationEnd = values[0] + values[2] - 1;
            SourceStart = values[1];
            SourceEnd = values[1] + values[2] - 1;
            Value = DestinationStart - SourceStart;
        }
    }

    private class SeedRange(Int64 start, Int64 end)
    {
        public Int64 Start { get; set; } = start;
        public Int64 End { get; set; } = end;
    }
}
