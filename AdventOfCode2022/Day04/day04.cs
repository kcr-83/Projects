using System.Text.Json;

namespace AdventOfCode2022;

public static class Day04
{
    const string InputFilePath = @".\Day04\input.txt";

    class AssignmentPairs
    {
        public int[] First { get; set; }
        public int[] Second { get; set; }
        public bool FullContains { get; set; }
    }

    public static string Run()
    {
        var input = File.ReadAllLines(InputFilePath);
        return PartTwo(input);
    }

    public static string PartTwo(string[] input)
    {
        return JsonSerializer.Serialize(
            input
                .Select(s => GeneratePair(s))
                .Select(s => CheckAnyContains(s))
                .Count(s => s.FullContains)
        );
    }

    public static string PartOne(string[] input)
    {
        return JsonSerializer.Serialize(
            input
                .Select(s => GeneratePair(s))
                .Select(s => CheckFullContains(s))
                .Count(s => s.FullContains)
        );
    }

    static AssignmentPairs GeneratePair(string input)
    {
        var pairs = input.Split(',');
        return new AssignmentPairs()
        {
            First = GenerateNumbers(pairs[0]),
            Second = GenerateNumbers(pairs[1]),
            FullContains = false
        };
    }

    static AssignmentPairs CheckFullContains(AssignmentPairs pair)
    {
        pair.FullContains = pair.First.All(x => pair.Second.Contains(x));
        if (!pair.FullContains)
            pair.FullContains = pair.Second.All(x => pair.First.Contains(x));
        return pair;
    }

    static AssignmentPairs CheckAnyContains(AssignmentPairs pair)
    {
        pair.FullContains = pair.First.Any(x => pair.Second.Contains(x));
        if (!pair.FullContains)
            pair.FullContains = pair.Second.Any(x => pair.First.Contains(x));
        return pair;
    }

    public static int[] GenerateNumbers(string input)
    {
        var numbers = input.Split('-');
        var first = Convert.ToInt32(numbers[0]);
        var second = Convert.ToInt32(numbers[1]);
        return Enumerable.Range(first, second - first + 1).ToArray();
    }
}
