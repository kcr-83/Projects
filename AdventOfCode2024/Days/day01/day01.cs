//https://adventofcode.com/2024/day/1
public class Day01
{
    public void Run()
    {
        Console.WriteLine("Day 01");
        var input = File.ReadAllLines("Days/day01/input.txt");
        var leftList = new List<int>();
        var rightList = new List<int>();
        for (var i = 0; i < input.Length; i++)
        {
            var line = input[i];
            var split = line.Split(' ');
            var left = int.Parse(split[0]);
            var right = int.Parse(split[3]);
            leftList.Add(left);
            rightList.Add(right);
        }
        leftList = leftList.OrderBy(x => x).ToList();
        //Console.WriteLine($"left: {string.Join(",", leftList)}");
        rightList = rightList.OrderBy(x => x).ToList();
        //Console.WriteLine($"right: {string.Join(",", rightList)}");
        var sum = 0;
        for (var i = 0; i < input.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {leftList[i]} - {rightList[i]} = {rightList[i] - leftList[i]}. SUM: {sum}");
            sum += Math.Abs(rightList[i] - leftList[i]);
        }
        Console.WriteLine($"part one sum: {sum}");
        //part two
        var sumTwo = 0;
        for (var i = 0; i < leftList.Count; i++)
        {
            var leftNumber = leftList[i];
            var countOfNumberInRightList = rightList.Count(x => x == leftNumber);
            sumTwo += leftNumber *countOfNumberInRightList;
        }
        Console.WriteLine($"part two sum: {sumTwo}");
    }
}