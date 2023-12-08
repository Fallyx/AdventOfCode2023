using System.Collections.Generic;

namespace AdventOfCode2023.Day08;

internal class Day08
{
    const string inputPath = @"Day08/Input.txt";

    internal static void Task1and2()
    {
        List<String> lines = [.. File.ReadAllLines(inputPath)];
        string path = lines[0];
        Dictionary< string, Node> nodes = [];
        Dictionary<string, GhostNode> ghostNodes = [];

        for(int i = 2; i < lines.Count; i++)
        {
            string[] split = lines[i].Split(" = ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            string key = split[0];
            split = split[1].Split(", ");
            Node node = new(split[0][1..], split[1][..3]);
            nodes.Add(key, node);
            if (key.EndsWith('A'))
                ghostNodes.Add(key, new(node, key));
        }

        int idx = 0;
        int steps = 0;
        bool endFound = false;
        string currentNode = "AAA";

        while (!endFound)
        {
            steps++;
            if (path[idx] == 'L')
                currentNode = nodes[currentNode].Left;
            else
                currentNode = nodes[currentNode].Right;

            if (currentNode == "ZZZ")
                endFound = true;
            idx = (idx + 1) % path.Length;
        }
        Console.WriteLine($"Task 1: {steps}");

        idx = 0;
        steps = 0;
        while (ghostNodes.Any(v => v.Value.Found != true))
        {
            steps++;
            foreach(KeyValuePair<string, GhostNode> ghostNode in ghostNodes)
            {
                if (ghostNode.Value.Found)
                    continue;

                if (path[idx] == 'L')
                    ghostNode.Value.CurrentNode = nodes[ghostNode.Value.CurrentNode].Left;
                else
                    ghostNode.Value.CurrentNode = nodes[ghostNode.Value.CurrentNode].Right;

                if (ghostNode.Value.CurrentNode.EndsWith('Z'))
                {
                    ghostNode.Value.Found = true;
                    ghostNode.Value.Steps = steps;
                }
            }

            idx = (idx + 1) % path.Length;
        }

        long[] ghostSteps = ghostNodes.Select(gn => gn.Value.Steps).ToArray();
        
        Console.WriteLine($"Task 2: {ghostSteps.Aggregate((a, b) => a * b / GCD(a, b))}");
    }
    private static long GCD(long a, long b)
    {
        return b == 0 ? a : GCD(b, a % b);
    }

    private class Node(String left, String right)
    {
        public String Left { get; set; } = left;
        public String Right { get; set; } = right;
    }

    private class GhostNode(Node node, String currentNode)
    {
        public Node Node { get; set; } = node;
        public String CurrentNode { get; set; } = currentNode;
        public long Steps { get; set; } = 0;
        public bool Found { get; set; } = false;
    }
}
