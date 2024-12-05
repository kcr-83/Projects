public class Day05
{
    public void Run()
    {
        Console.WriteLine("Day 05");
        var input = GetInput();
        var rules = input.Item1;
        int sumOfMiddlePages = 0;
        int sumOfMiddlePagesV2 = 0;

        foreach (var update in input.Item2)
        {
            if (IsUpdateValid(update, rules))
            {
                int middleIndex = update.Count / 2;
                sumOfMiddlePages += update[middleIndex];
            }
            if (!IsUpdateValid(update, rules))
            {
                var correctedUpdate = TopologicalSort(update, rules);
                int middleIndex = correctedUpdate.Count / 2;
                sumOfMiddlePagesV2 += correctedUpdate[middleIndex];
            }
        }
        Console.WriteLine($"Sum of middle pages: {sumOfMiddlePages}");
        Console.WriteLine($"Sum of middle pages (V2): {sumOfMiddlePagesV2}");
    }

    bool IsUpdateValid(List<int> update, List<(int Before, int After)> rules)
    {
        var positionMap = update
            .Select((page, index) => new { Page = page, Index = index })
            .ToDictionary(x => x.Page, x => x.Index);

        foreach (var rule in rules)
        {
            if (positionMap.ContainsKey(rule.Before) && positionMap.ContainsKey(rule.After))
            {
                if (positionMap[rule.Before] >= positionMap[rule.After])
                {
                    return false; // Rule violated
                }
            }
        }

        return true; // All rules satisfied
    }

    (List<(int, int)>, List<List<int>>) GetInput()
    {
        var input = File.ReadAllLines("Days/day05/input.txt");
        var rules = new List<(int Before, int After)>();
        var lines = new List<List<int>>();
        var startRules = true;
        var startLines = false;
        for (var i = 0; i < input.Length; i++)
        {
            if (String.IsNullOrWhiteSpace(input[i]))
            {
                startLines = true;
                startRules = false;
                continue;
            }
            if (startRules)
            {
                var split = input[i].Split('|');
                rules.Add((int.Parse(split[0]), int.Parse(split[1])));
            }
            if (startLines)
            {
                var line = input[i].Split(',').Select(int.Parse).ToList();
                lines.Add(new List<int>(line));
            }
        }
        return (rules, lines);
    }
    List<int> TopologicalSort(List<int> update, List<(int Before, int After)> rules)
    {
        // Build graph
        var graph = new Dictionary<int, List<int>>();
        var inDegree = new Dictionary<int, int>();

        foreach (var page in update)
        {
            graph[page] = new List<int>();
            inDegree[page] = 0;
        }

        foreach (var rule in rules)
        {
            if (update.Contains(rule.Before) && update.Contains(rule.After))
            {
                graph[rule.Before].Add(rule.After);
                inDegree[rule.After]++;
            }
        }

        // Topological Sort using Kahn's algorithm
        var queue = new Queue<int>(inDegree.Where(kv => kv.Value == 0).Select(kv => kv.Key));
        var sortedOrder = new List<int>();

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            sortedOrder.Add(current);

            foreach (var neighbor in graph[current])
            {
                inDegree[neighbor]--;
                if (inDegree[neighbor] == 0)
                {
                    queue.Enqueue(neighbor);
                }
            }
        }

        return sortedOrder;
    }
}
