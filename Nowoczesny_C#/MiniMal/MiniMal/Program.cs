using System.Collections.Generic;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("MiniMal.Tests")]
namespace MiniMal
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    string? inputText = Console.ReadLine();
                    Types1.MalType? mal = Reader1.ReadText(inputText!);
                    if (mal != null)
                    {
                        string outputText = Printer1.PrintStr(mal);
                        Console.WriteLine(outputText);
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Error: " + exception.Message);
                }
            }
        }
        internal static IEnumerable<string> ProcessItems(string[] items)
        {
            return items.Where(i => !string.IsNullOrEmpty(i)).OrderBy(i => i.Length).Select(i => i.ToUpper());
        }
    }
}