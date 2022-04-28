using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static MiniMal.Types;
using PowerFP;
using System.Globalization;

namespace MiniMal
{
    public static class Reader
    {
        private static NumberFormatInfo NumberFormat = new NumberFormatInfo() { NumberGroupSeparator = ".", NumberDecimalSeparator = "," };

        public static MalType? ReadText(string text) =>
            Tokenize(text)
                .Pipe(tokens => new ReaderObj(tokens.ToArray()))
                .Pipe(reader => ReadForm(reader));

        internal class ReaderObj
        {
            private readonly string[] _tokens;
            private int _positon = 0;

            public ReaderObj(string[] tokens) => _tokens = tokens;

            public string? Next() => _positon == _tokens.Length ? null : _tokens[_positon++];
            public string? Peek() => _positon == _tokens.Length ? null : _tokens[_positon];
        }

        internal static MalType? ReadForm(ReaderObj reader) =>
            reader.Next().Pipe(token => token switch
            {
                null => null,
                "(" or "[" => ReadList(reader, token == "(" ? ")" : "]"),
                "{" => ReadList(reader, "}").Pipe(list => ListToMap(((List)list).Items)),
                _ => ReadAtom(token)
            });

        internal static MalType ReadList(ReaderObj reader, string endOfListToken, LList<MalType>? currentList = null) =>
            ReadForm(reader).Pipe(mal =>
            mal switch
            {
                null => throw new Exception("List is not closed"),
                Symbol symbol when symbol.Name == endOfListToken =>
                    new List(currentList, endOfListToken == ")" ? ListType.List : ListType.Vector),
                _ => ReadList(reader, endOfListToken, currentList.Concat(new LList<MalType>(mal, null)))  // lista jest kopiowna
            });


        #region funkcyjny Reader

        public static MalType? ReadText__(string text) =>
             Tokenize(text).Pipe(tokens => ReadForm__(tokens.ToLList()).Result);


        internal record ReadFormResult(MalType? Result, LList<string>? RestTokens) { }

        internal static ReadFormResult ReadForm__(LList<string>? tokens) =>
            tokens switch
            {
                null => new ReadFormResult(null, null),
                (var token, var restTokens) => token switch
                {
                    "(" or "[" => ReadList__(restTokens, token == "(" ? ")" : "]")
                         .Pipe((r => new ReadFormResult(new List(r.Result, token == "(" ? ListType.List : ListType.Vector), r.RestTokens))),
                    "{" => ReadList__(restTokens, "}")
                         .Pipe((r => new ReadFormResult(ListToMap(r.Result), r.RestTokens))),
                    _ => new ReadFormResult(ReadAtom(token), restTokens)
                },
            };


        internal record ReadListResult(LList<MalType>? Result, LList<string>? RestTokens) { }

        internal static ReadListResult ReadList__(LList<string>? tokens, string endOfListToken) =>
            ReadForm__(tokens) switch
            {
                { Result: null } => throw new Exception("List is not closed"),
                { Result: Symbol symbol } r when symbol.Name == endOfListToken => new ReadListResult(null, r.RestTokens),
                { Result: var mal } r => ReadList__(r.RestTokens, endOfListToken).Pipe(r => r with { Result = new(mal, r.Result) })
            };

        #endregion


        internal static MalType ReadAtom(string token) =>
            token switch
            {
                "true" => TrueV,
                "false" => FalseV,
                "nil" => NilV,
                _ when Double.TryParse(token, NumberStyles.Any, NumberFormat, out var doubleValue) => new Number(doubleValue),
                _ when token.FirstOrDefault() == '"' => token.Length > 1 && token.LastOrDefault() == '"'
                    ? new Str(token[1..^1])
                    : throw new Exception($"String value '${token}' in not closed"),
                _ => new Symbol(token)
            };

        internal static Map ListToMap(LList<MalType>? mals) =>
            // new Map(new(MalsToKeyValuePairs(mals))); 
            // nie mozemy jawnie przekazac listy par Key+Value jak wyzej, poniewaz lista musi byc posortowana po kluczu
            new Map(MapM.MapFrom(MalsToKeyValuePairs(mals)));

        static LList<(string Key, MalType Value)>? MalsToKeyValuePairs(LList<MalType>? mals)
            => mals switch
            {
                null => null,
                (Str Key, (var Value, var Rest)) => new((Key.Value, Value), MalsToKeyValuePairs(Rest)),
                _ => throw new Exception($"Invalid Map '{Printer.JoinWithSeparator(mals)}', odd number of elements or key is not a 'Str' type"),
            };

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