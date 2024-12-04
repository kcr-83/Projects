using System.Text.RegularExpressions;

public class Day03
{
    public void Run()
    {
        Console.WriteLine("Day 03");
        var input = File.ReadAllText("Days/day03/input.txt");
        var sections = FindCorrectSectionsV2(input);
        var sum = 0;
        foreach (var section in sections)
        {
            sum += section.Item1 * section.Item2;
        }
        Console.WriteLine($"Part one: {sum}");
    }

    (int, int)[] FindCorrectSections(string input)
    {
        var output = new List<(int, int)>();
        var pattern = @"mul\(\d+,\d+\)";
        var numberPattern = @"\d+";
        MatchCollection matches = Regex.Matches(input, pattern);
        foreach (Match match in matches)
        {
            var numbers = Regex.Matches(match.Value, numberPattern);
            var num1 = int.Parse(numbers[0].Value);
            var num2 = int.Parse(numbers[1].Value);
            output.Add((num1, num2));
        }
        return output.ToArray();
    }

    (int, int)[] FindCorrectSectionsV2(string input)
    {
        var output = new List<(int, int)>();
        string pattern = @"(mul\((\d+),(\d+)\))|(do\(\))|(don't\(\))";
        MatchCollection matches = Regex.Matches(input, pattern);
        bool isEnabled = true;
        foreach (Match match in matches)
        {
            if (match.Groups[1].Success) // Dopasowano `mul(int,int)`
            {
                if (isEnabled)
                {
                    int a = int.Parse(match.Groups[2].Value);
                    int b = int.Parse(match.Groups[3].Value);
                    output.Add((a, b));
                }
            }
            else if (match.Groups[4].Success) // Dopasowano `do()`
            {
                isEnabled = true;
            }
            else if (match.Groups[5].Success) // Dopasowano `don't()`
            {
                isEnabled = false;
            }
        }
        return output.ToArray();
    }
}
