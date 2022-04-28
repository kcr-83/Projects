using System;
using System.Linq;
using System.Collections.Generic;
using PowerFP;
using System.Globalization;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("MiniMal.Tests")]

namespace MiniMal
{
    class Program
    {
        static void Main(string[] args)
        {
            EnvM.Env env = new EnvM.Env(Core.Ns, null);
            env.Set(new Types.Symbol("eval"), new Types.Fn(Core.CreateEval(env)));

            while (true)
            {
                try
                {
                    string? inputText = Console.ReadLine();
                    Types.MalType? mal = Reader.ReadText(inputText!);
                    if (mal != null)
                    {
                        mal = EvalM.Eval(mal, env);
                        string outputText = Printer.PrintStr(mal);
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
            var q =
                from item in items
                where !string.IsNullOrEmpty(item)
                orderby item.Length
                select item.ToUpper();

            return q;
        }
    }
}

