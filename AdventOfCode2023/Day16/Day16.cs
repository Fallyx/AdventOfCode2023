using System.Numerics;

namespace AdventOfCode2023.Day16;

internal class Day16
{
    const string inputPath = @"Day16/Input.txt";

    internal static void Task1and2()
    {
        List<String> lines = [.. File.ReadAllLines(inputPath)];
        // x, y, direction (0 up, 1 right, 2 down, 3 left)
        List<Vector3> beams = [];
        // x, y, object (1 /, 2 \, 3 -, 4 |)
        Dictionary<Vector2, int> objects = [];
        // x, y, directions
        Dictionary<Vector2, List<float>> energizedTiles = [];
        int maxX = lines[0].Length;
        int maxY = lines.Count;

        for(int y = 0; y < maxY; y++)
        {
            for (int x = 0; x < maxX; x++)
            {
                if (lines[y][x] == '/')
                    objects.Add(new(x, y), 1);
                else if (lines[y][x] == '\\')
                    objects.Add(new(x, y), 2);
                else if (lines[y][x] == '-')
                    objects.Add(new(x, y), 3);
                else if (lines[y][x] == '|')
                    objects.Add(new(x, y), 4);
            }
        }

        int maxTiles = CountEnergizedTiles(new(-1, 0, 1), objects, maxX, maxY);
        // PrintMap(energizedTiles, objects, maxX, maxY);
        Console.WriteLine($"Task 1: {maxTiles}");

        for (int y = 0; y < maxY; y++)
        {
            for (int x = 0; x < maxX; x++)
            {
                int countTiles = 0;

                if (y == 0)
                {
                    countTiles = CountEnergizedTiles(new(x, y - 1, 2), objects, maxX, maxY);
                    maxTiles = (countTiles > maxTiles ? countTiles : maxTiles);
                }
                else if (y == maxY - 1)
                {
                    countTiles = CountEnergizedTiles(new(x, y + 1, 0), objects, maxX, maxY);
                    maxTiles = (countTiles > maxTiles ? countTiles : maxTiles);
                }

                if (x == 0)
                {
                    countTiles = CountEnergizedTiles(new(x - 1, y, 1), objects, maxX, maxY);
                    maxTiles = (countTiles > maxTiles ? countTiles : maxTiles);
                }
                else if (x == maxX - 1)
                {
                    countTiles = CountEnergizedTiles(new(x + 1, y, 3), objects, maxX, maxY);
                    maxTiles = (countTiles > maxTiles ? countTiles : maxTiles);
                }
            }
        }

        Console.WriteLine($"Task 2: {maxTiles}");
    }

    private static int CountEnergizedTiles(Vector3 start, Dictionary<Vector2, int> objects, int maxX, int maxY)
    {
        Dictionary<Vector2, List<float>> energizedTiles = [];
        List<Vector3> beams = [start];

        while (beams.Count > 0)
        {
            List<Vector3> newBeams = [];
            for (int i = 0; i < beams.Count; i++)
            {
                Vector2 currentPos = new(beams[i].X, beams[i].Y);
                float dir = beams[i].Z;
                if (energizedTiles.TryGetValue(currentPos, out List<float> directions))
                {
                    if (directions.Contains(dir))
                    {
                        continue;
                    }

                    directions.Add(dir);
                }
                else if ((currentPos.X >= 0 && currentPos.X < maxX ) && (currentPos.Y >= 0 && currentPos.Y < maxY))
                {
                    energizedTiles.Add(currentPos, [dir]);
                }

                Vector2 nextPos = new(-1, -1);
                if (dir == 0)
                    nextPos = new(currentPos.X, currentPos.Y - 1);
                else if (dir == 1)
                    nextPos = new(currentPos.X + 1, currentPos.Y);
                else if (dir == 2)
                    nextPos = new(currentPos.X, currentPos.Y + 1);
                else if (dir == 3)
                    nextPos = new(currentPos.X - 1, currentPos.Y);

                if (nextPos.X < 0 || nextPos.X >= maxX || nextPos.Y < 0 || nextPos.Y >= maxY)
                {
                    continue;
                }

                if (objects.TryGetValue(nextPos, out int mirrorSplit))
                {
                    if (mirrorSplit == 1)
                    {
                        if (dir == 0)
                            newBeams.Add(new(nextPos.X, nextPos.Y, 1));
                        else if (dir == 1)
                            newBeams.Add(new(nextPos.X, nextPos.Y, 0));
                        else if (dir == 2)
                            newBeams.Add(new(nextPos.X, nextPos.Y, 3));
                        else if (dir == 3)
                            newBeams.Add(new(nextPos.X, nextPos.Y, 2));
                    }
                    else if (mirrorSplit == 2)
                    {
                        if (dir == 0)
                            newBeams.Add(new(nextPos.X, nextPos.Y, 3));
                        else if (dir == 1)
                            newBeams.Add(new(nextPos.X, nextPos.Y, 2));
                        else if (dir == 2)
                            newBeams.Add(new(nextPos.X, nextPos.Y, 1));
                        else if (dir == 3)
                            newBeams.Add(new(nextPos.X, nextPos.Y, 0));
                    }
                    else if (mirrorSplit == 3 && (dir == 0 || dir == 2))
                    {
                        newBeams.Add(new(nextPos.X, nextPos.Y, 1));
                        newBeams.Add(new(nextPos.X, nextPos.Y, 3));
                    }
                    else if (mirrorSplit == 4 && (dir == 1 || dir == 3))
                    {
                        newBeams.Add(new(nextPos.X, nextPos.Y, 0));
                        newBeams.Add(new(nextPos.X, nextPos.Y, 2));
                    }
                    else
                    {
                        newBeams.Add(new(nextPos.X, nextPos.Y, dir));
                    }

                    continue;
                }

                newBeams.Add(new(nextPos.X, nextPos.Y, dir));
            }
            beams = [.. newBeams];
        }

        return energizedTiles.Count;
    }

    private static void PrintMap(Dictionary<Vector2, List<float>> energizedTiles, Dictionary<Vector2, int> objects, int maxX, int maxY)
    {
        Console.WriteLine("\n\n\n");

        for (int y = 0; y < maxY; y++)
        {
            for (int x = 0; x < maxX; x++)
            {
                Vector2 currentPos = new(x, y);
                /*
                if (objects.TryGetValue(currentPos, out int mirrorSplit))
                {
                    if (mirrorSplit == 1)
                        Console.Write('/');
                    else if (mirrorSplit == 2)
                        Console.Write('\\');
                    else if (mirrorSplit == 3)
                        Console.Write('-');
                    else if (mirrorSplit == 4)
                        Console.Write('|');
                }
                else */
                if (energizedTiles.TryGetValue(currentPos, out List<float> tiles))
                {
                    /*
                    if (tiles.Count > 1)
                        Console.Write(tiles.Count);
                    else if (tiles[0] == 0)
                        Console.Write('^');
                    else if (tiles[0] == 1)
                        Console.Write('>');
                    else if (tiles[0] == 2)
                        Console.Write('v');
                    else if (tiles[0] == 3)
                        Console.Write('<');
                    */
                    Console.Write('#');
                }
                else
                {
                    Console.Write('.');
                }

            }

            Console.WriteLine();
        }
    }
}