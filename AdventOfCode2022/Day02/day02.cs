namespace AdventOfCode2022;

public static class Day02
{
    const string InputFilePath = @".\Day02\input.txt";

    enum Sign
    {
        Rock = 1,
        Paper = 2,
        Scissors = 3
    }

    static Sign Elf(string line) =>
        line[0] == 'A'
            ? Sign.Rock
            : line[0] == 'B'
                ? Sign.Paper
                : line[0] == 'C'
                    ? Sign.Scissors
                    : throw new ArgumentException(line);

    static Sign Human1(string line) =>
        line[2] == 'X'
            ? Sign.Rock
            : line[2] == 'Y'
                ? Sign.Paper
                : line[2] == 'Z'
                    ? Sign.Scissors
                    : throw new ArgumentException(line);

    static Sign Next(Sign sign) =>
        sign == Sign.Rock
            ? Sign.Paper
            : sign == Sign.Paper
                ? Sign.Scissors
                : sign == Sign.Scissors
                    ? Sign.Rock
                    : throw new ArgumentException(sign.ToString());

    static int Score(Sign elfSign, Sign humanSign) =>
        humanSign == Next(elfSign)
            ? 6 + (int)humanSign
            : // human wins
            humanSign == elfSign
                ? 3 + (int)humanSign
                : // draw
                humanSign == Next(Next(elfSign))
                    ? 0 + (int)humanSign
                    : // elf wins
                    throw new ArgumentException(elfSign.ToString());

    static Sign Human2(string line) =>
        line[2] == 'X'
            ? Next(Next(Elf(line)))
            : // elf wins
            line[2] == 'Y'
                ? Elf(line)
                : // draw
                line[2] == 'Z'
                    ? Next(Elf(line))
                    : // you win
                    throw new ArgumentException(line);

    static int Total(string input, Func<string, Sign> elf, Func<string, Sign> human) =>
        input.Split('\n').Select(line => Score(elf(line), human(line))).Sum();

    public static string Run()
    {
        var input = File.ReadAllText(InputFilePath);
        return Total(input, Elf, Human1) + "\n";
    }

        public static string Run2()
        {
            var input = File.ReadAllText(InputFilePath);
            return Total(input, Elf, Human2) + "\n";
        }
}
