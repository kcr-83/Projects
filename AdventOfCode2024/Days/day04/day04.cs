public class Day04
{
    public void Run()
    {
        Console.WriteLine("Day 04");
        char[,] grid = ReadGridFromFile("Days/day04/input.txt");

        string word = "XMAS";
        int occurrences = CountWordOccurrences(grid, word);

        Console.WriteLine($"Liczba wystąpień '{word}': {occurrences}");

        int xmasOccurrences = CountXMASOccurrences(grid);

        Console.WriteLine($"Liczba wystąpień XMAS: {xmasOccurrences}");
    }

    char[,] ReadGridFromFile(string filePath)
    {
        // Odczyt wszystkich linii z pliku
        string[] lines = File.ReadAllLines(filePath);

        // Określenie wymiarów tablicy
        int rows = lines.Length;
        int cols = lines[0].Length;

        // Tworzenie tablicy char[,]
        char[,] grid = new char[rows, cols];

        // Wypełnianie tablicy znakami z pliku
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                grid[row, col] = lines[row][col];
            }
        }

        return grid;
    }

    int CountWordOccurrences(char[,] grid, string word)
    {
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);
        int wordLength = word.Length;
        int count = 0;

        // Kierunki: (rowDir, colDir)
        int[,] directions =
        {
            { 0, 1 }, // Prawo
            { 0, -1 }, // Lewo
            { 1, 0 }, // Dół
            { -1, 0 }, // Góra
            { 1, 1 }, // Ukośnie w dół w prawo
            { 1, -1 }, // Ukośnie w dół w lewo
            { -1, 1 }, // Ukośnie w górę w prawo
            { -1, -1 } // Ukośnie w górę w lewo
        };

        // Sprawdź każdy punkt jako początek słowa
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                for (int d = 0; d < directions.GetLength(0); d++)
                {
                    int rowDir = directions[d, 0];
                    int colDir = directions[d, 1];
                    if (CheckWord(grid, word, row, col, rowDir, colDir))
                    {
                        count++;
                    }
                }
            }
        }

        return count;
    }

    bool CheckWord(char[,] grid, string word, int startRow, int startCol, int rowDir, int colDir)
    {
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);
        int wordLength = word.Length;

        for (int i = 0; i < wordLength; i++)
        {
            int newRow = startRow + i * rowDir;
            int newCol = startCol + i * colDir;

            // Sprawdź granice
            if (newRow < 0 || newRow >= rows || newCol < 0 || newCol >= cols)
            {
                return false;
            }

            // Sprawdź, czy litera pasuje
            if (grid[newRow, newCol] != word[i])
            {
                return false;
            }
        }

        return true;
    }
    int CountXMASOccurrences(char[,] grid)
    {
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);
        int count = 0;

        // Iteracja przez każdy punkt jako potencjalny środek litery X
        for (int row = 1; row < rows - 1; row++)
        {
            for (int col = 1; col < cols - 1; col++)
            {
                // Sprawdzenie wzoru X-MAS
                if (IsXMAS(grid, row, col))
                {
                    count++;
                }
            }
        }

        return count;
    }

    bool IsXMAS(char[,] grid, int centerRow, int centerCol)
    {
        // Środek X musi być 'A'
        if (grid[centerRow, centerCol] != 'A') return false;

        // Sprawdź cztery możliwe "X-MAS":
        //  1. Lewy górny -> Prawy dolny, Lewy dolny -> Prawy górny
        //  2. I odwrotności

        string diagonal1 = $"{grid[centerRow - 1, centerCol - 1]}{grid[centerRow, centerCol]}{grid[centerRow + 1, centerCol + 1]}";
        string diagonal2 = $"{grid[centerRow - 1, centerCol + 1]}{grid[centerRow, centerCol]}{grid[centerRow + 1, centerCol - 1]}";

        // "MAS" lub "SAM" w przekątnych
        return (diagonal1 == "MAS" || diagonal1 == "SAM") &&
               (diagonal2 == "MAS" || diagonal2 == "SAM");
    }
}
