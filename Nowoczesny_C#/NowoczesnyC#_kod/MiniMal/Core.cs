
using PowerFP;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using static MiniMal.Printer;
using static MiniMal.Types;

namespace MiniMal
{
    public static class Core
    {
        private static Map<Symbol, MalType>? ns = null;

        // property (instead of field because) order of static members initialization matters
        public static Map<Symbol, MalType> Ns => ns ?? (ns = MapM.MapFrom(LListM.LListFrom
        (
            (new Symbol("+"), new Fn(args => ExecuteArithmeticFn(args, (a, b) => a + b)) as MalType),
            (new Symbol("-"), new Fn(args => ExecuteArithmeticFn(args, (a, b) => a - b))),
            (new Symbol("*"), new Fn(args => ExecuteArithmeticFn(args, (a, b) => a * b))),
            (new Symbol("/"), new Fn(args => ExecuteArithmeticFn(args, (a, b) => a / b))),

            (new Symbol("<"), new Fn(args => ExecuteComparisonFn(args, (a, b) => a < b))),
            (new Symbol("<="), new Fn(args => ExecuteComparisonFn(args, (a, b) => a <= b))),
            (new Symbol(">"), new Fn(args => ExecuteComparisonFn(args, (a, b) => a > b))),
            (new Symbol(">="), new Fn(args => ExecuteComparisonFn(args, (a, b) => a >= b))),


            (new Symbol("list"), new Fn(ListFn)),
            (new Symbol("vector"), new Fn(VectorFn)),
            (new Symbol("cons"), new Fn(ConsFn)),
            (new Symbol("concat"), new Fn(ConcatFn)),
            (new Symbol("conj"), new Fn(ConjFn)),
            (new Symbol("count"), new Fn(CountFn)),
            (new Symbol("first"), new Fn(FirstFn)),
            (new Symbol("rest"), new Fn(RestFn)),
            (new Symbol("nth"), new Fn(NthFn)),
            (new Symbol("empty?"), new Fn(IsEmptyFn)),
            (new Symbol("list?"), new Fn(IsListFn)),
            (new Symbol("vec"), new Fn(VecFn)),

            (new Symbol("assoc"), new Fn(AssocFn)),
            (new Symbol("dissoc"), new Fn(DissocFn)),
            (new Symbol("get"), new Fn(GetFn)),
            (new Symbol("contains?"), new Fn(ContainsFn)),
            (new Symbol("keys"), new Fn(KeysFn)),
            (new Symbol("vals"), new Fn(ValsFn)),
            (new Symbol("hash-map"), new Fn(HashMapFn)),

            (new Symbol("str"), new Fn(StrFn)),
            (new Symbol("println"), new Fn(PrintLnFn)),
            (new Symbol("="), new Fn(EqualsFn)),
            (new Symbol("nil?"), new Fn(IsOfType<Nil>())),
            (new Symbol("true?"), new Fn(IsOfType<True>())),
            (new Symbol("false?"), new Fn(IsOfType<False>())),
            (new Symbol("symbol?"), new Fn(IsOfType<Symbol>())),
            (new Symbol("number?"), new Fn(IsOfType<Number>())),
            (new Symbol("string?"), new Fn(IsOfType<Str>())),
            (new Symbol("map?"), new Fn(IsOfType<Map>())),
            (new Symbol("vector?"), new Fn(IsVectorFn)),
            (new Symbol("fn?"), new Fn(IsFnFn)),
            (new Symbol("sequential?"), new Fn(IsSequentialFn)),

            (new Symbol("read-string"), new Fn(ReadStringFn))
        )));

        internal static MalType ExecuteArithmeticFn(LList<MalType>? args, Func<double, double, double> operation)
            => args switch
            {
                { Tail: { } } => args.Aggregate((totalMal, nextMal) => (totalMal, nextMal) switch
                    {
                        (Number(var total), Number(var next)) => new Number(operation(total, next)),
                        _ => throw new Exception($"All arguments of arithmetic operations must be of the 'Number' type, but got an argument '{(totalMal is not Number ? totalMal : nextMal)}' in {args.JoinWithSeparator(",")}")
                    }),
                _ => throw new Exception($"Arithmetic operation required at least two arguments, but got '{args.Count()}', arguments: {args.JoinWithSeparator(",")}"),
            };

        internal static MalType ExecuteComparisonFn(LList<MalType>? args, Func<double, double, bool> comparison)
            => args switch
            {
                (Number { Value: var value1 }, (Number { Value: var value2 }, null)) => comparison(value1, value2) ? TrueV : FalseV,
                _ => throw new Exception($"Number comparison operation requires two arguments of type 'Number', but got {args.JoinWithSeparator(",")}"),
            };


        // ** list

        internal static FnDelegate ListFn = args => new List(args, ListType.List);
        internal static FnDelegate VectorFn = args => new List(args, ListType.Vector);

        internal static FnDelegate ConsFn = args
            => args switch
            {
                (var FirstArg, (List { Items: var Items } ListArg, null)) => new List(new(FirstArg, Items), ListType.List),

                _ => ThrowError("cons", args, "two arguments where the second one must be of type 'list'")
            };

        internal static FnDelegate ConcatFn = ConcatImplFn;
        private static MalType ConcatImplFn(LList<MalType>? args)
            => args switch
            {
                null => new List(null, ListType.List),
                (List { Items: var Items }, var RestArguments) =>
                    new List(Items.Concat((ConcatImplFn(RestArguments) as List)!.Items), ListType.List),
                _ => ThrowError("concat", args, "all arguments to be of type 'list'")
            };


        internal static FnDelegate ConjFn = args
            => args switch
            {
                (List { ListType: ListType.List, Items: var Items }, var NewItems) =>
                    new List(NewItems.Aggregate(Items, (agg, item) => new(item, agg)), ListType.List),
                (List { ListType: ListType.Vector, Items: var Items }, var NewItems) =>
                    new List(Items.Concat(NewItems), ListType.Vector),
                _ => ThrowError("conj", args, "at least one argument of type 'list' or 'vector'")
            };


        internal static FnDelegate CountFn = args
                 => args switch
                 {
                     (Nil, null) => new Number(0),
                     (List { Items: var items }, null) => new Number(items.Count()),
                     _ => ThrowError("count", args, "one argument of type 'list' or 'vector' or 'nil'")
                 };

        internal static FnDelegate FirstFn = args
            => args switch
            {
                (Nil, null) => NilV,
                (List { Items: var Items }, null) => Items == null ? NilV : Items.Head,
                _ => ThrowError("first", args, "one argument of type 'list' or 'vector'")
            };


        internal static FnDelegate RestFn = args
            => args switch
            {
                (Nil, null) => new List(null, ListType.List),
                (List { Items: var Items }, null) => new List(Items?.Tail, ListType.List),
                _ => ThrowError("rest", args, "one argument of type 'list' or 'vector'")
            };

        internal static FnDelegate NthFn = args
            => args switch
            {
                (List { Items: var Items }, (Number { Value: var Index }, null)) => Items.ElementAt((int)Index),
                _ => ThrowError("nth", args, "two arguments where the first one is of type 'list' and the second of type 'number'")
            };

        internal static FnDelegate IsEmptyFn = args
           => args switch
           {
               (List { Items: null }, null) => TrueV,
               (List { }, null) => FalseV,
               _ => ThrowError("empty?", args, "one argument of type 'list' or 'vector'")
           };


        internal static FnDelegate IsListFn = args
            => args switch
            {
                (List { ListType: ListType.List }, null) => TrueV,
                (_, null) => FalseV,
                _ => ThrowError("list?", args, "one argument"),
            };

        internal static FnDelegate VecFn = args
             => args switch
             {
                 (List { ListType: ListType.Vector } vector, null) => vector,
                 (List list, null) => new List(list.Items, ListType.Vector),
                 _ => ThrowError("vec", args, "one arguments of type 'list' or 'vector'")
             };

        // ** map

        internal static FnDelegate AssocFn = args
            => args switch
            {
                (Map { Value: var MapItems }, var NewItems) => new Map(
                    Reader.ListToMap(NewItems).Value.Items.Aggregate(MapItems, (map, kv) => map.Add(kv.Key, kv.Value))),
                _ => ThrowError("assoc", args, "at least one argument of type 'map'")
            };

        internal static FnDelegate DissocFn = args
            => args switch
            {
                (Map { Value: var MapItems }, var DeletedKeys) => new Map(MapM.MapFrom(
                    MapItems.Items.Where(kv => !DeletedKeys.Any(delKey => delKey is Str str && str.Value == kv.Key)))),
                _ => ThrowError("dissoc", args, "at least one argument of type 'map'")
            };


        internal static FnDelegate GetFn = args
            => args switch
            {
                (Nil, _) => NilV,
                (Map { Value: var MapItems }, (Str { Value: var Key }, null)) => MapItems.TryFind(Key, out var Value) ? Value : NilV,
                _ => ThrowError("get", args, "two arguments where the first one must be of type 'map' and the second of type 'keyword' oraz 'string'")
            };

        internal static FnDelegate ContainsFn = args
            => args switch
            {
                (Map { Value: var MapItems }, (Str { Value: var Key }, null)) => MapItems.TryFind(Key, out var Value) ? TrueV : FalseV,
                _ => ThrowError("contains?", args, "two arguments where the first one must be of type 'map' and the second of type 'keyword' oraz 'string'")
            };

        internal static FnDelegate KeysFn = args
            => args switch
            {
                (Map { Value: var MapItems }, null) => new List(MapItems.Items.Select(kv => new Str(kv.Key) as MalType), ListType.List),
                _ => ThrowError("keys", args, "one argument of type 'map'")
            };


        internal static FnDelegate ValsFn = args
            => args switch
            {
                (Map { Value: var MapItems }, null) => new List(MapItems.Items.Select(kv => kv.Value), ListType.List),
                _ => ThrowError("vals", args, "one argument of type 'map'")
            };

        internal static FnDelegate HashMapFn = args => Reader.ListToMap(args);



        // ** utils

        internal static FnDelegate EqualsFn = args
            => args switch
            {
                (var Mal1, (var Mal2, null)) => Types.MalEqual(Mal1, Mal2) ? TrueV : FalseV,
                _ => ThrowError("=", args, "two arguments"),

            };

        internal static FnDelegate StrFn = args => new Str(args.JoinWithSeparator());

        internal static FnDelegate PrintLnFn = args => args.JoinWithSeparator().Pipe(text =>
        {
            Console.WriteLine(text);
            return NilV;
        });

        private static FnDelegate IsOfType<T>() => args => args is (T, null) ? TrueV : FalseV;

        internal static FnDelegate IsVectorFn = args => args is (List { ListType: ListType.Vector }, null) ? TrueV : FalseV;

        internal static FnDelegate IsSequentialFn = args => args is (List, null) ? TrueV : FalseV;

        internal static FnDelegate IsFnFn = args => args is (Fn { IsMacro: false }, null) ? TrueV : FalseV;

        internal static FnDelegate IsMacroFn = args => args is (Fn { IsMacro: true }, null) ? TrueV : FalseV;

        // ** interpreter 

        internal static FnDelegate ReadStringFn = args
            => args switch
            {
                (Str { Value: var strValue }, null) => Reader.ReadText(strValue) ?? NilV,
                _ => ThrowError("read-string", args, "one argument of type 'string'")
            };

        internal static FnDelegate CreateEval(EnvM.Env env) =>
            args => args switch
            {
                (var Mal, null) => EvalM.Eval(Mal, env),
                _ => ThrowError("eval", args, "one argument")
            };


        // private

        private static MalType ThrowError(string functionName, LList<MalType>? args, string message)
        {
            throw new Exception($"'{functionName}' function requires {message}, but got {args.JoinWithSeparator(",")}");
        }
    }
}