using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using static MiniMal.Types1;

namespace MiniMal
{
    public static class Reader1
    {
        private static NumberFormatInfo NumberFormat = new NumberFormatInfo() { NumberGroupSeparator = ".", NumberDecimalSeparator = "," };

        internal class ReaderObj
        {
            private readonly string[] _tokens;
            private int _positon = 0;

            public ReaderObj(string[] tokens)
            {
                _tokens = tokens;
            }

            public string? Next() => _positon == _tokens.Length ? null : _tokens[_positon++];
            public string? Peek() => _positon == _tokens.Length ? null : _tokens[_positon];
        }


        public static MalType? ReadText(string text)
        {
            var tokens = Tokenize(text);
            var reader = new ReaderObj(tokens.ToArray());
            var mal = ReadForm(reader);
            return mal;
        }

        internal static MalType? ReadForm(ReaderObj reader)
        {
            var token = reader.Peek();

            if (token == null)
            {
                return null;
            }

            _ = reader.Next();

            if (token == "(" || token == "[")
            {
                return ReadList(reader, token == "(" ? ")" : "]");
            }

            if (token == "{")
            {
                var list = (List)ReadList(reader, "}");
                return ListToMap(list.Items);
            }

            return ReadAtom(token);
        }

        internal static MalType ReadList(ReaderObj reader, string endOfListToken)
        {
            var malTypes = new List<MalType>();

            while (true)
            {
                var mal = ReadForm(reader);
                if (mal == null)
                {
                    throw new Exception("List is not closed");
                }

                if ((mal is Symbol) && ((Symbol)mal).Name == endOfListToken)
                {
                    break;
                }

                malTypes.Add(mal);
            }

            return new List(malTypes.ToArray(), endOfListToken == ")" ? ListType.List : ListType.Vector);
        }

        internal static MalType ReadAtom(string token)
        {
            switch (token)
            {
                case "true": return TrueV;
                case "false": return FalseV;
                case "nil": return NilV;
                default:
                    {
                        double doubleValue;
                        if (Double.TryParse(token, NumberStyles.Any, NumberFormat, out doubleValue))
                        {
                            return new Number(doubleValue);
                        }

                        if (token.FirstOrDefault() == '"')
                        {
                            if (token.Length > 1 && token.LastOrDefault() == '"')
                            {
                                return new Str(token[1..^1]);
                            }
                            throw new Exception($"String value '${token}' in not closed");
                        }
                        return new Symbol(token);
                    }
            }
        }

        internal static Map ListToMap(MalType[] mals)
        {
            var malDictionary = new Dictionary<string, MalType>();
            if (mals.Length % 2 == 1)
            {
                throw new Exception($"Invalid Map '{Printer1.JoinWithSpaces(mals)}', odd number of elements'");
            }

            for (var i = 0; i < mals.Length - 1; i += 2)
            {
                var key = mals[i];
                if (!(key is Str))
                {
                    throw new Exception($"Invalid Map '{Printer1.JoinWithSpaces(mals)}', key '{Printer1.PrintStr(key)}' is not a 'Str' type");
                }
                malDictionary.Add(((Str)key).Value, mals[i + 1]);
            }

            return new Map(malDictionary);
        }

        // private 
        static IEnumerable<string> Tokenize(string str)
        {
            const string pattern = @"[\s ,]*(~@|[\[\]{}()'`~@]|""(?:[\\].|[^\\""])*""?|;.*|[^\s \[\]{}()'""`~@,;]*)";
            var regex = new Regex(pattern);

            foreach (Match match in regex.Matches(str))
            {
                var token = match.Groups[1].Value;
                if (!string.IsNullOrEmpty(token) && !(token[0] == ';'))
                {
                    yield return token;
                }
            }
        }
    }
}