using System.Collections.Generic;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("MiniMal.Tests")]
namespace MiniMal
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
        internal static IEnumerable<string> ProcessItems(string[] items)
        {
            return items.Where(i => !string.IsNullOrEmpty(i)).OrderBy(i => i.Length).Select(i => i.ToUpper());
        }
    }
}