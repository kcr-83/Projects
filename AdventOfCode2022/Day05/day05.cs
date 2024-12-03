namespace AdventOfCode2022;

public static class Day05
{
    class Move
    {
        public int Count { get; set; }
        public int From { get; set; }
        public int To { get; set; }
    }

    const string InputFilePath = @".\Day05\input.txt";
    static List<Queue<string>> queues = new List<Queue<string>>();
    static List<Move> moves = new List<Move>();

    public static string Run()
    {
        var input = File.ReadAllLines(InputFilePath);
        PreperInput(input);
        return PartOne();
    }

    public static string PartTwo(string[] input)
    {
        return "";
    }

    public static string PartOne()
    {
        moves.ForEach(m => MoveCrates(m));
        return queues
            .Where(q => q.Count > 0)
            .Select(q => q.Dequeue())
            .Aggregate("", (current, next) => current + next)
            .Replace("[", string.Empty)
            .Replace("]", string.Empty);
    }

    static void PreperInput(string[] input)
    {
        for (int i = 0; i < 9; i++)
        {
            queues.Add(new Queue<string>());
        }
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i].Contains("1"))
                break;
            var parts = input[i].Replace("    ", "[_] ").Split(' ');
            for (int j = 0; j < parts.Length; j++)
            {
                var queue = queues[j];
                queue ??= new Queue<string>();
                queue.Enqueue(parts[j].Replace("[_]", string.Empty));
            }
        }
        for (int j = 0; j < queues.Count; j++)
        {
            var queue = queues[j];
            var queueCount = queue.Count;
            for (int i = 0; i < queueCount; i++)
            {
                var item = queue.Dequeue();
                if (!string.IsNullOrWhiteSpace(item))
                {
                    queue.Enqueue(item);
                }
            }
            queues[j] = new Queue<string>(queue.Reverse());
        }
        foreach (var line in input)
        {
            if (!line.Contains("move"))
                continue;
            moves.Add(
                new Move()
                {
                    Count = Convert.ToInt32(line.Substring(line.IndexOf("move ") + 5, 1)),
                    From = Convert.ToInt32(line.Substring(line.IndexOf("from ") + 5, 1)),
                    To = Convert.ToInt32(line.Substring(line.IndexOf("to ") + 3, 1)),
                }
            );
        }
    }

    static void MoveCrates(Move move)
    {
        Console.WriteLine($"from: {move.From}, To: {move.To}, Count: {move.Count}");
        for (int i = 0; i < move.Count; i++)
        {
            if (queues[move.From - 1].Count != 0)
            {
                var from = queues[move.From - 1].Dequeue();
                queues[move.To - 1].Enqueue(from);
                Console.WriteLine(
                    $"1: {queues[0].Count}, 2: {queues[1].Count}, 3: {queues[2].Count}, 4: {queues[3].Count}, 5: {queues[4].Count}, 6: {queues[5].Count}, 7: {queues[6].Count}, 8: {queues[7].Count}, 9: {queues[8].Count}"
                );
            }
            else
            {
                Console.WriteLine($"Crate {move.From - 1} is empty");
            }
        }
    }
}
