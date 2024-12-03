namespace AdventOfCode2022;

public static class Day03
{
    const string InputFilePath = @".\Day03\input.txt";

    public static string[] DivideInHalf(string input)
    {
        return new string[]
        {
            input.Substring(0, input.Length / 2),
            input.Substring(input.Length / 2)
        };
    }

    public static string FindCommonType(string input1, string input2)
    {
        return input1.First(c => input2.Contains(c)).ToString();
    }

    public static string FindCommonTreeType(string input1, string input2, string input3)
    {
        return input1.First(c => input2.Contains(c) && input3.Contains(c)).ToString();
    }

    public static string PartOne(string[] input)
    {
        var item = input.Select(s => DivideInHalf(s)).Select(s => FindCommonType(s[0], s[1]));
        return ConvertToNumber(string.Join("", item));
    }

    public static string PartTwo(string[] input)
    {
        var item = input
            .Select((s, index) => new { Value = s, Index = index })
            .GroupBy(x => x.Index / 3)
            .Select(
                s =>
                    FindCommonTreeType(
                        s.ElementAt(0).Value,
                        s.ElementAt(1).Value,
                        s.ElementAt(2).Value
                    )
            );
        return ConvertToNumber(string.Join("", item));
    }

    public static string ConvertToNumber(string input)
    {
        var indexSmall = 1;
        var indexBig = 27;
        var alphabetSmall = Enumerable
            .Range('a', 'z' - 'a' + 1)
            .Select(c => (char)c)
            .ToDictionary(i => (char)i, i => indexSmall++);
        var alphabetBig = Enumerable
            .Range('A', 'Z' - 'A' + 1)
            .Select(c => (char)c)
            .ToDictionary(i => (char)i, i => indexBig++);
        var sum = input
            .Select(s => alphabetSmall.ContainsKey(s) ? alphabetSmall[s] : alphabetBig[s])
            .Sum();
        return sum.ToString();
    }

    public static string Run()
    {
        var input = File.ReadAllLines(InputFilePath);
        return PartTwo(input);
    }
}
