using System;
using PowerFP;

namespace MiniMal
{
    public static class Types
    {
        public abstract record MalType { }

        public record Nil : MalType { }
        public record True : MalType { }
        public record False : MalType { }

        public record Number(double Value) : MalType { }
        public record Str(string Value) : MalType { }
        public record Symbol(string Name) : MalType { }

        public enum ListType { List, Vector }
        public record List(LList<MalType>? Items, ListType ListType) : MalType { }

        public record Map(Map<string, MalType> Value) : MalType { }

        public delegate MalType FnDelegate(LList<MalType>? args);
        public record Fn(FnDelegate Value, bool IsMacro = false) : MalType { }

        // consts
        public static True TrueV = new True();
        public static False FalseV = new False();
        public static Nil NilV = new Nil();

        public static bool MalEqual(MalType mal1, MalType mal2) =>
             (mal1, mal2) switch
             {
                 // dla list robimy wyjatek poniewaz 'ListType' nie chcemy uwzgledniac podczas porownywania   
                 (List list1, List list2) => list1.Items.SequenceEqual(list2.Items, MalEqual),
                 // dla mapy kolejnosc par nie ma znaczenia
                 (Map { Value: { Items: var items1 } }, Map { Value: { Items: var items2 } }) =>
                    items1.Count() == items2.Count() &&
                    items1.All(item1 => items2.Any(item2 => item1.Key == item2.Key && MalEqual(item1.Value, item2.Value))),
                 (Fn, Fn) => false,
                 _ => mal1.Equals(mal2)
             };
    }
}
