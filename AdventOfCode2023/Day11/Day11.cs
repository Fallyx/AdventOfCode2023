using System.Numerics;

namespace AdventOfCode2023.Day11;

internal class Day11
{
    const string inputPath = @"Day11/Input.txt";

    internal static void Task1and2()
    {
        List<String> lines = [.. File.ReadAllLines(inputPath)];
        List<Vector2> galaxies = [];

        for (int y = 0; y < lines.Count; y++)
        {
            for (int x = 0; x < lines[0].Length; x++)
            {
                if (lines[y][x] == '#')
                    galaxies.Add(new Vector2(x, y));
            }
        }

        List<Vector2> galaxiesTask1 = ExpandGalaxies(galaxies, 1, lines[0].Length, lines.Count);
        List<Vector2> galaxiesTask2 = ExpandGalaxies(galaxies, 999999, lines[0].Length, lines.Count);

        long sumDistanceTask1 = 0;
        long sumDistanceTask2 = 0;
        for (int i = 0; i < galaxies.Count - 1; i++)
        {
            for (int x = i + 1; x < galaxies.Count; x++)
            {
                sumDistanceTask1 += (long)(Math.Abs(galaxiesTask1[i].X - galaxiesTask1[x].X) + Math.Abs(galaxiesTask1[i].Y - galaxiesTask1[x].Y));
                sumDistanceTask2 += (long)(Math.Abs(galaxiesTask2[i].X - galaxiesTask2[x].X) + Math.Abs(galaxiesTask2[i].Y - galaxiesTask2[x].Y));
            }
        }

        Console.WriteLine($"Task 1: {sumDistanceTask1}");
        Console.WriteLine($"Task 2: {sumDistanceTask2}");
    }

    private static List<Vector2> ExpandGalaxies(List<Vector2> galaxies, int expandBy, int maxX, int maxY)
    {
        List<Vector2> expandedGalaxies = galaxies;
        for (int y = maxY - 1; y >= 0; y--)
        {
            if (!expandedGalaxies.Any(g => g.Y == y))
            {
                List<Vector2> tmp = [];
                foreach (Vector2 galaxy in expandedGalaxies)
                {
                    if (y <= galaxy.Y)
                        tmp.Add(new Vector2(galaxy.X, galaxy.Y + expandBy));
                    else
                        tmp.Add(galaxy);
                }

                expandedGalaxies = tmp;
            }
        }

        for (int x = maxX; x >= 0; x--)
        {
            if (!expandedGalaxies.Any(g => g.X == x))
            {
                List<Vector2> tmp = [];
                foreach (Vector2 galaxy in expandedGalaxies)
                {
                    if (x <= galaxy.X)
                        tmp.Add(new Vector2(galaxy.X + expandBy, galaxy.Y));
                    else
                        tmp.Add(galaxy);
                }

                expandedGalaxies = tmp;
            }
        }

        return expandedGalaxies;
    }
}
