namespace AdventOfCode2023.Day15;

internal class Day15
{
    const string inputPath = @"Day15/Input.txt";

    internal static void Task1and2()
    {
        List<String> lines = [.. File.ReadAllLines(inputPath)];
        String[] initSequenceArray = lines[0].Split(',');
        List<HASHMAP> hashMaps = [];
        int sumHashAlg = 0;

        foreach(String initSeq in initSequenceArray)
        {
            sumHashAlg += CalculateHASH(initSeq);

            if (initSeq.Contains('='))
            {
                String[] split = initSeq.Split('=');
                if (hashMaps.Any(hm => hm.Label == split[0]))
                {
                    int idx = hashMaps.FindIndex(hm => hm.Label == split[0]);
                    hashMaps[idx].FocalLength = int.Parse(split[1]);
                }
                else
                {
                    int box = CalculateHASH(split[0]);
                    int slot = hashMaps.Count(hm => hm.Box == box);
                    hashMaps.Add(new HASHMAP(box, slot+1, split[0], split[1]));
                }
            }
            else if (initSeq.Contains('-'))
            {
                String label = initSeq[..^1];
                HASHMAP hashMap = hashMaps.Find(hm => hm.Label == label);
                if (hashMap == null) continue;
                hashMaps.Remove(hashMap);

                foreach(HASHMAP hmap in hashMaps)
                {
                    if (hmap.Box == hashMap.Box && hmap.Slot > hashMap.Slot)
                    {
                        hmap.Slot--;
                    }
                }
            }
        }

        Console.WriteLine($"Task 1: {sumHashAlg}");

        int focusingPower = 0;

        foreach(HASHMAP hmap in hashMaps)
        {
            focusingPower += (hmap.Box + 1) * hmap.Slot * hmap.FocalLength;
        }

        Console.WriteLine($"Task 2: {focusingPower}");
    }

    private static int CalculateHASH(String initSeq)
    {
        int currentValue = 0;
        foreach (char c in initSeq)
        {
            currentValue += c;
            currentValue *= 17;
            currentValue %= 256;
        }

        return currentValue;
    }

    private class HASHMAP
    {
        public int Box { get; set; }
        public int Slot { get; set; }
        public String Label { get; set; }
        public int FocalLength { get; set; }

        public HASHMAP(int box, int slot, String label, String focalLength)
        {
            Box = box;
            Slot = slot;
            Label = label;
            FocalLength = int.Parse(focalLength);
        }
    }
}