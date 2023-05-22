namespace MiniMal
{
    public static class Types1
    {
        public abstract class MalType { }
        public enum ListType { List, Vector };
        public delegate MalType FnDelegate(MalType[] args);
        public static bool MalEqual(MalType mal1, MalType mal2)
        {
            if (mal1 is True) return mal2 is True;
            if (mal1 is False) return mal2 is False;
            if (mal1 is Nil) return mal2 is Nil;
            if (mal1 is Str) return mal2 is Str ? ((Str)mal1).Value == ((Str)mal2).Value : false;
            if (mal1 is Symbol) return mal2 is Symbol ? ((Symbol)mal1).Name == ((Symbol)mal2).Name : false;
            if (mal1 is Number) return mal2 is Number ? ((Number)mal1).Value == ((Number)mal2).Value : false;
            //if (mal1 is Fn) return false;

            if (mal1 is List)
            {
                if (!(mal2 is List)) return false;
                MalType[] items1 = ((List)mal1).Items, items2 = ((List)mal2).Items;

                if (items1.Length != items2.Length) return false;

                for (int i = 0; i < items1.Length; i++)
                {
                    if (!MalEqual(items1[i], items2[i])) return false;
                }
                return true;
            }
            if (mal1 is Map)
            {
                if (!(mal2 is Map)) return false;

                var dic1 = ((Map)mal1).Value;
                var dic2 = ((Map)mal2).Value;

                if (dic1.Count != dic2.Count) return false;

                foreach (var keyValue1 in dic1)
                {
                    MalType? value2;
                    if (!dic2.TryGetValue(keyValue1.Key, out value2)) return false;
                    if (!MalEqual(keyValue1.Value, value2)) return false;
                }
                return true;
            }
            return false;

        }
        public class Nil : MalType {  }
        public class True : MalType { }
        public class False : MalType { }
         // consts
        public static True TrueV = new True();
        public static False FalseV = new False();
        public static Nil NilV = new Nil();
        public class Str : MalType
        {
            public string Value { get; set; }

            public Str(string value)
            {
                Value = value;
            }
        }
        public class Number : MalType
        {
            public double Value { get; set; }

            public Number(double value)
            {
                Value = value;
            }
        }
        public class Symbol : MalType
        {
            public string Name { get; set; }

            public Symbol(string name)
            {
                Name = name;
            }
        }
        public class List : MalType
        {
            public MalType[] Items { get; set; }
            public ListType ListType { get; set; }

            public List(MalType[] items, ListType listType)
            {
                Items = items;
                ListType = listType;
            }

        }
        public class Map : MalType
        {
            public Dictionary<string, MalType> Value { get; set; }

            public Map(Dictionary<string, MalType> value)
            {
                Value = value;
            }
        }
        public class Fn : MalType
        {
            public FnDelegate Value { get; set; }

            public Fn(FnDelegate value)
            {
                Value = value;
            }
        }
    }
}