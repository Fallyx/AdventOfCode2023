using System.Numerics;

namespace AdventOfCode2023.Day10;

internal class Day10
{
    const string inputPath = @"Day10/Input.txt";

    internal static void Task1and2()
    {
        List<String> lines = [.. File.ReadAllLines(inputPath)];
        Dictionary<Vector2, Pipe> pipes = [];
        Vector2 sPos = new();
        HashSet<Vector2> visited = [];

        for(int y = 0; y < lines.Count; y++)
        {
            for (int x = 0; x < lines[0].Length; x++)
            {
                if (lines[y][x] == '.')
                    continue;
                else if (lines[y][x] == 'S')
                    sPos = new Vector2(x, y);
                else
                {
                    Vector2 pos = new(x, y);
                    pipes.Add(pos, new Pipe(lines[y][x], pos, GetNeighbors(lines[y][x])));
                }
            }
        }

        pipes.Add(sPos, CalculateStart(sPos, pipes));

        Console.WriteLine();

        bool[] fartestPlaceFound = [false, false];
        int stepsFromStart = 0;
        visited.Add(sPos);
        Vector2[] nextPos = pipes[sPos].Neighbors;
        int fartest = 0;
        while (fartestPlaceFound.All(b => b == false))
        {
            stepsFromStart++;

            Pipe pipe = pipes[nextPos[0]];
            if (pipe.StepsFromStart > stepsFromStart)
            {
                pipe.StepsFromStart = stepsFromStart;
                visited.Add(nextPos[0]);
                if (fartest < stepsFromStart)
                    fartest = stepsFromStart;
            }
            else
            {
                fartestPlaceFound[0] = true;
            }

            nextPos[0] = pipes[nextPos[0]].GetNextNeighbor(visited);

            pipe = pipes[nextPos[1]];
            if (pipe.StepsFromStart > stepsFromStart)
            {
                pipe.StepsFromStart = stepsFromStart;
                visited.Add(nextPos[1]);
                if (fartest < stepsFromStart)
                    fartest = stepsFromStart;
            }
            else
            {
                fartestPlaceFound[1] = true;
            }

            nextPos[1] = pipes[nextPos[1]].GetNextNeighbor(visited);
        }

        Console.WriteLine($"Task 1: {fartest}");

        List<Vector2> keysToRemove = pipes.Keys.Except(visited).ToList();
        foreach(Vector2 key in keysToRemove)
        {
            pipes.Remove(key);
        }

        HashSet<Vector2> inLoop = [];
        for(int y = 0; y < lines.Count; y++)
        {
            bool isInside = false;
            char enterChar = '?';
            for (int x = 0; x < lines[0].Length; x++)
            {
                Vector2 currentPos = new(x, y);
                if (!pipes.ContainsKey(currentPos) && isInside)
                {
                    inLoop.Add(currentPos);
                }
                else if (pipes.TryGetValue(currentPos, out Pipe currentPipe))
                {
                    if (enterChar == '?') enterChar = currentPipe.Symbol;
                    if ((currentPipe.Symbol == '|') || (enterChar == 'F' && currentPipe.Symbol == 'J') || (enterChar == 'L' && currentPipe.Symbol == '7'))
                    {
                        isInside = !isInside;
                        enterChar = '?';
                    }
                    else if ((enterChar == 'L' && currentPipe.Symbol == 'J') || (enterChar == 'F' && currentPipe.Symbol == '7'))
                    {
                        enterChar = '?';
                    }
                }
            }
        }

        Console.WriteLine($"Task 2: {inLoop.Count}");
    }

    private static Vector2[] GetNeighbors(char pipe)
    {
        Vector2[] neighbors = new Vector2[2];

        if (pipe == '|')
        {
            neighbors[0] = new Vector2(0, -1);
            neighbors[1] = new Vector2(0, 1);
        }
        else if (pipe == '-')
        {
            neighbors[0] = new Vector2(-1, 0);
            neighbors[1] = new Vector2(1, 0);
        }
        else if (pipe == 'L')
        {
            neighbors[0] = new Vector2(0, -1);
            neighbors[1] = new Vector2(1, 0);
        }
        else if (pipe == 'J')
        {
            neighbors[0] = new Vector2(0, -1);
            neighbors[1] = new Vector2(-1, 0);
        }
        else if (pipe == '7')
        {
            neighbors[0] = new Vector2(0, 1);
            neighbors[1] = new Vector2(-1, 0);
        }
        else if (pipe == 'F')
        {
            neighbors[0] = new Vector2(1, 0);
            neighbors[1] = new Vector2(0, 1);
        }

        return neighbors;
    }

    private static Pipe CalculateStart(Vector2 sPos, Dictionary<Vector2, Pipe> pipes)
    {
        Vector2[] neighbors = new Vector2[2];
        char c = 'S';
        bool isTop = false;
        bool isBottom = false;
        bool isLeft = false;
        bool isRight = false;
        
        if (pipes.TryGetValue(new Vector2(sPos.X, sPos.Y - 1), out Pipe top))
        {
            if (top.Symbol == '|' || top.Symbol == '7' || top.Symbol == 'F')
                isTop = true;
        }

        if (pipes.TryGetValue(new Vector2(sPos.X, sPos.Y + 1), out Pipe bottom))
        {
            if (bottom.Symbol == '|' || bottom.Symbol == 'L' || bottom.Symbol == 'J')
                isBottom = true;
        }

        if (pipes.TryGetValue(new Vector2(sPos.X - 1, sPos.Y), out Pipe left))
        {
            if (left.Symbol == '-' || left.Symbol == 'L' || left.Symbol == 'F')
                isLeft = true;
        }

        if (pipes.TryGetValue(new Vector2(sPos.X + 1, sPos.Y), out Pipe right))
        {
            if (right.Symbol == '-' || right.Symbol == '7' || right.Symbol == 'J')
                isRight = true;
        }

        if (isTop)
        {
            neighbors[0] = new Vector2(0, -1);
            if (isBottom)
            {
                neighbors[1] = new Vector2(0, 1);
                c = '|';
            }
            else if (isLeft)
            {
                neighbors[1] = new Vector2(- 1, 0);
                c = 'J';
            }
            else if (isRight)
            {
                neighbors[1] = new Vector2(1, 0);
                c = 'L';
            }
        }
        else if (isBottom)
        {
            neighbors[0] = new Vector2(0, 1);
            if (isLeft)
            {
                neighbors[1] = new Vector2(-1, 0);
                c = '7';
            }
            else if (isRight)
            {
                neighbors[1] = new Vector2(+1, 0);
                c = 'F';
            }
        }
        else
        {
            neighbors[0] = new Vector2(-1, 0);
            neighbors[1] = new Vector2(1, 0);
            c = '-';
        }

        return new Pipe(c, sPos, neighbors, 0);
    }

    private class Pipe
    {
        public char Symbol { get; set; }
        public Vector2 Position { get; set; }
        public Vector2[] Neighbors { get; set; }
        public int StepsFromStart { get; set; }

        public Pipe(char symbol, Vector2 pos, Vector2[] neighbors, int stepsFromStart = int.MaxValue)
        {
            Symbol = symbol;
            Position = pos;
            Neighbors = new Vector2[2];
            Neighbors[0] = new Vector2(pos.X + neighbors[0].X, pos.Y + neighbors[0].Y);
            Neighbors[1] = new Vector2(pos.X + neighbors[1].X, pos.Y + neighbors[1].Y);
            StepsFromStart = stepsFromStart;
        }

        public Vector2 GetNextNeighbor(HashSet<Vector2> visited)
        {
            if (visited.Contains(Neighbors[0]))
                return Neighbors[1];
            else
                return Neighbors[0];
        }
    }
}
