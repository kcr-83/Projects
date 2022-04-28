using System.Collections.Generic;
using System.Linq;
using static MiniMal.Types1;

namespace MiniMal
{
    public static class Printer1
    {
        public static string PrintStr(MalType? mal)
        {
            if (mal == null) return "";
            if (mal is True) return "true";
            if (mal is False) return "false";
            if (mal is Nil) return "nil";

            if (mal is Symbol) return ((Symbol)mal).Name;
            if (mal is Str) return $"\"{((Str)mal).Value}\"";
            if (mal is Number) return ((Number)mal).Value.ToString();

            if (mal is List)
            {
                var list = (List)mal;
                var openingBracket = list.ListType == ListType.List ? "(" : "[";
                var closingbracket = list.ListType == ListType.List ? ")" : "]";
                return $"{openingBracket}{string.Join(" ", list.Items.Select(PrintStr))}{closingbracket}";
            }

            if (mal is Map)
            {
                var map = (Map)mal;
                return $"{{{string.Join(" ", map.Value.Select(kv => $"\"{kv.Key}\" {PrintStr(kv.Value)}"))}}}";
            }

            if (mal is Fn) return "#<function>";

            return "";
        }

        public static string JoinWithSpaces(this IEnumerable<MalType> mals, string separator = " ") =>
            string.Join(separator, mals.Select(PrintStr));

    }
}