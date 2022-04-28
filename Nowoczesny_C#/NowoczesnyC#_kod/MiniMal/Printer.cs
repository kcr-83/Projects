using System.Linq;
using PowerFP;
using static MiniMal.Types;

namespace MiniMal
{
    public static class Printer
    {
        public static string PrintStr(this MalType? mal) =>
            mal switch
            {
                True => "true",
                False => "false",
                Nil => "nil",

                Number(var Value) => Value.ToString(),
                Symbol(var Name) => Name,
                Str(var Value) => $"\"{Value}\"",

                List(var Items, var ListType) =>
                    $"{(ListType == ListType.List ? "(" : "[")}{string.Join(" ", Items.Select(PrintStr).ToEnumerable())}{(ListType == Types.ListType.List ? ")" : "]")}",

                Map(var Value) =>
                    $"{{{string.Join(" ", Value.Items.Select(kv => $"\"{kv.Key}\" {PrintStr(kv.Value)}").ToEnumerable())}}}",

                Fn => "#<function>",

                _ => ""
            };

        public static string JoinWithSeparator(this LList<MalType>? mals, string separator = " ") =>
            string.Join(separator, mals.Select(Printer.PrintStr).ToEnumerable());
    }
}