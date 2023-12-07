namespace AdventOfCode2023.Day07;

internal class Day07
{
    const string inputPath = @"Day07/Input.txt";

    internal static void Task1()
    {
        List<String> lines = [.. File.ReadAllLines(inputPath)];
        List<Player> players = [];

        foreach (String line in lines)
        {
            players.Add(new(line));
        }

        players.Sort();
        int totalWinning = 0;
        for (int i = 0; i < players.Count; i++)
        {
            totalWinning += (i + 1) * players[i].Bid;
        }

        Console.WriteLine($"Task 1: {totalWinning}");

        players = [];

        foreach (String line in lines)
        {
            players.Add(new(line, true));
        }

        players.Sort();
        totalWinning = 0;
        for (int i = 0; i < players.Count; i++)
        {
            totalWinning += (i + 1) * players[i].Bid;
        }

        Console.WriteLine($"Task 2: {totalWinning}");
    }

    private class Player : IComparable<Player>
    {
        private readonly Dictionary<char, int> cardValue = new()
        {
            { 'A', 14 }, { 'K', 13 }, { 'Q', 12 }, { 'J' , 11 }, { 'T', 10 }, { '9', 9 }, { '8', 8 }, { '7', 7 }, { '6', 6 }, { '5', 5 }, { '4', 4 }, { '3', 3 }, { '2', 2 }
        };
        private readonly Dictionary<char, int> cardValueTask2 = new()
        {
            { 'A', 14 }, { 'K', 13 }, { 'Q', 12 }, { 'J' , 1 }, { 'T', 10 }, { '9', 9 }, { '8', 8 }, { '7', 7 }, { '6', 6 }, { '5', 5 }, { '4', 4 }, { '3', 3 }, { '2', 2 }
        };


        public String Hand { get; set; }
        public int[] CardValue { get; set; }
        public int Bid { get; set; }
        public Type HandType { get; set; }

        public Player(String line, bool joker = false)
        {
            string[] split = line.Split(' ');
            Hand = split[0];
            if (!joker)
                CardValue = Hand.Select(c => cardValue[c]).ToArray();
            else
                CardValue = Hand.Select(c => cardValueTask2[c]).ToArray();
            Bid = int.Parse(split[1]);

            Dictionary<int, int> typeDict = [];
            int jMod = 0;
            for (int i = 0; i < CardValue.Length; i++)
            {
                if (CardValue[i] == 1)
                    jMod++;
                else if (typeDict.ContainsKey(CardValue[i]))
                    typeDict[CardValue[i]]++;
                else
                    typeDict.Add(CardValue[i], 1);
            }

            if (jMod == 5)
            {
                HandType = Type.Five;
                return;
            }

            int[] type = [.. typeDict.Select(t => t.Value).OrderByDescending(t => t)];
            type[0] += jMod;

            if (type.Length == 1)
                HandType = Type.Five;
            else if (type.Length == 2 && type[0] == 4)
                HandType = Type.Four;
            else if (type.Length == 2)
                HandType = Type.Full;
            else if (type.Length == 3 && type[0] == 3)
                HandType = Type.Three;
            else if (type.Length == 3)
                HandType = Type.Two;
            else if (type.Length == 4)
                HandType = Type.One;
            else
                HandType = Type.Highest;
        }

        public int CompareTo(Player? other)
        {
            if (HandType != other.HandType)
                return (HandType > other.HandType) ? 1 : -1;

            for (int i = 0; i < 5; i++)
            {
                if (CardValue[i] != other.CardValue[i])
                    return (CardValue[i] > other.CardValue[i]) ? 1 : - 1;
            }

            return 0;
        }
    }

    private enum Type : ushort
    {
        Highest = 1,
        One = 2,
        Two = 3,
        Three = 4,
        Full = 5,
        Four = 6,
        Five = 7,
        Unknown = 0
    }
}
