


public class Day02
{
    public void Run()
    {
        Console.WriteLine("Day 02");
        var input = File.ReadAllLines("Days/day02/input.txt");
        var inputList = new Dictionary<int, int[]>(1000);
        for (var i = 0; i < input.Length; i++)
        {
            inputList.Add(i, input[i].Split(' ').Select(int.Parse).ToArray());
        }
        var safListCount = 0;
        for (var i = 0; i < inputList.Count; i++)
        {
            var levels = inputList[i];
            if (IsSafe(levels)|| CanBeMadeSafe(levels)) safListCount++;
        }
        Console.WriteLine(safListCount);
    }
    bool IsSafe(int[] levels)
    {
        bool isIncreasing = true;
        bool isDecreasing = true;

        for (int i = 1; i < levels.Length; i++)
        {
            int diff = levels[i] - levels[i - 1];

            // Sprawdź, czy różnica jest w zakresie 1-3
            if (Math.Abs(diff) < 1 || Math.Abs(diff) > 3)
                return false;

            // Sprawdź kierunek zmian
            if (diff > 0)
                isDecreasing = false;
            else if (diff < 0)
                isIncreasing = false;
        }

        // Raport jest bezpieczny, jeśli jest monotoniczny (rosnący lub malejący)
        return isIncreasing || isDecreasing;
    }
    bool CanBeMadeSafe(int[] levels)
    {
        for (int i = 0; i < levels.Length; i++)
        {
            // Tworzymy nową tablicę z pominięciem jednego elementu
            var reducedLevels = levels.Where((_, index) => index != i).ToArray();

            // Sprawdzamy, czy po usunięciu poziomu raport staje się bezpieczny
            if (IsSafe(reducedLevels))
                return true;
        }

        return false;
    }
}