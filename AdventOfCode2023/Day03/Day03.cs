namespace AdventOfCode2023.Day03;

internal class Day03
{
    const string inputPath = @"Day03/Input.txt";

    internal static void Task1()
    {
        List<String> lines = [.. File.ReadAllLines(inputPath)];
        int partNumSum = 0;

        for(int y = 0; y < lines.Count; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                if (!Char.IsDigit(lines[y][x])) continue;

                string number = "" + lines[y][x];

                int numLength = 0;
                bool numEnded = false;

                while(!numEnded)
                { 
                    numLength++;
                    if (x + numLength < lines[y].Length && Char.IsDigit(lines[y][x + numLength]))
                    {
                        number = number + lines[y][x + numLength];
                    }
                    else
                    { 
                        numEnded = true; 
                    }
                }

                int subStringStart = x - 1 >= 0 ? x - 1 : 0;
                int subStringEnd = x + numLength + 1 < lines[y].Length ? x + numLength + 1 : x + numLength;
                subStringEnd -= subStringStart;
                string lineTop = (y - 1 >= 0 ? lines[y - 1].Substring(subStringStart, subStringEnd) : string.Empty);
                string lineMiddle = lines[y].Substring(subStringStart, subStringEnd);
                string lineBottom = (y + 1 < lines.Count ? lines[y + 1].Substring(subStringStart, subStringEnd) : string.Empty);

                if(IsPartNumber(lineTop, lineMiddle, lineBottom))
                {
                    partNumSum += int.Parse(number);
                }

                x += numLength;
            }
        }

        Console.WriteLine($"Task 1: {partNumSum}");
    }

    private static bool IsPartNumber(string lineTop, string lineMiddle, string lineBototm)
    {
        bool isPartNumber = false;

        if (lineTop.Length > 0)
        {
            for(int x = 0; x < lineTop.Length; x++)
            {
                if (lineTop[x] != '.' && !Char.IsDigit(lineTop[x])) return true;
            }
        }

        for (int x = 0; x < lineMiddle.Length; x++)
        {
            if (lineMiddle[x] != '.' && !Char.IsDigit(lineMiddle[x])) return true;
        }

        if (lineBototm.Length > 0)
        {
            for (int x = 0; x < lineBototm.Length; x++)
            {
                if (lineBototm[x] != '.' && !Char.IsDigit(lineBototm[x])) return true;
            }
        }

        return isPartNumber;
    }
}
