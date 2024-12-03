namespace AdventOfCode2022;

public static class Day01
{
    const string InputFilePath = @".\Day01\input.txt";

    public static List<int> Sum()
    {
        var inpout = File.ReadAllLines(InputFilePath);
        var sums = new List<int>();
        var sum = 0;
        foreach (var line in inpout)
        {
            if (!string.IsNullOrEmpty(line))
            {
                sum += Convert.ToInt32(line);
            }
            else
            {
                sums.Add(sum);
                sum = 0;
            }
        }
        return sums;
    }

    public static string Run()
    {
        return Sum().Max().ToString();
    }

    public static string Run2()
    {
        return Sum().OrderByDescending(s => s).Take(3).Sum().ToString();
    }
}
