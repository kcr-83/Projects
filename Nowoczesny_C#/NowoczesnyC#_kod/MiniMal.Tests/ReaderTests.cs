using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static MiniMal.Types;
using PowerFP;

namespace MiniMal.Tests
{
    [TestClass]
    public class ReaderTests
    {
        [TestMethod]
        public void ReadAtomTest()
        {
            Assert.IsTrue(Types.MalEqual(Reader.ReadAtom("false"), FalseV));
            Assert.IsTrue(Types.MalEqual(Reader.ReadAtom("true"), TrueV));
            Assert.IsTrue(Types.MalEqual(Reader.ReadAtom("nil"), NilV));

            Assert.IsTrue(Types.MalEqual(Reader.ReadAtom("abc"), new Symbol("abc")));
            Assert.IsTrue(Types.MalEqual(Reader.ReadAtom("\"abc\""), new Str("abc")));
            Assert.IsTrue(Types.MalEqual(Reader.ReadAtom("123"), new Number(123)));

            Assert.ThrowsException<Exception>(() => Reader.ReadAtom("\"abc"));
        }

        [TestMethod]
        public void ReadListTest()
        {
            var tokens = new[] { "a", "123", ")" };
            var reader = new Reader.ReaderObj(tokens);

            var list = (List)Reader.ReadList(reader, ")");

            Assert.IsNotNull(list);
            Assert.AreEqual(ListType.List, list.ListType);
            Assert.AreEqual(2, list.Items.Count());
            Assert.IsTrue(MalEqual(list.Items!.Head, new Symbol("a")));
            Assert.IsTrue(MalEqual(list.Items!.Tail!.Head, new Number(123)));
        }

        [TestMethod]
        public void ListToMapTest()
        {
            var nameProp = new Str("name");
            var name = new Str("adam");
            var ageProp = new Str("age");
            var age = new Number(20);

            Assert.ThrowsException<Exception>(() => Reader.ListToMap(new MalType[] { nameProp }.ToLList()));
            Assert.ThrowsException<Exception>(() => Reader.ListToMap(new MalType[] { nameProp, name, ageProp }.ToLList()));
            Assert.ThrowsException<Exception>(() => Reader.ListToMap(new MalType[] { nameProp, name, age, age }.ToLList()));

            var map = Reader.ListToMap(new MalType[] { nameProp, name, ageProp, age }.ToLList());
            Assert.IsNotNull(map);

            Assert.AreEqual(2, map.Value.Items.Count());
            Assert.IsTrue(map.Value.TryFind(nameProp.Value).IsFound);
            Assert.IsTrue(map.Value.TryFind(ageProp.Value).IsFound);

            Assert.AreEqual(name.Value, ((Str)map.Value.Find(nameProp.Value)).Value);
            Assert.AreEqual(age.Value, ((Number)map.Value.Find(ageProp.Value)).Value);
        }



        [TestMethod]
        public void ReadFormTest()
        {
            Assert.IsTrue(MalEqual(Reader.ReadForm(new Reader.ReaderObj(new[] { "nil", "true" }))!,
                NilV));

            Assert.IsTrue(MalEqual(Reader.ReadForm(new Reader.ReaderObj(new[] { "(", "+", "1", "2", ")" }))!,
                new List(new MalType[] { new Symbol("+"), new Number(1), new Number(2) }.ToLList(), ListType.List)));

            Assert.IsTrue(MalEqual(Reader.ReadForm(new Reader.ReaderObj(new[] { "(", "1", "[", "2", "]", ")" }))!,
                new List(new MalType[]
                {
                    new Number(1),
                    new List(new MalType[] { new Number(2) }.ToLList(), ListType.Vector)
                }.ToLList(), ListType.List)));

            Assert.IsTrue(MalEqual(Reader.ReadForm(new Reader.ReaderObj(new[] { "{", "\"name\"", "\"adam\"", "}" }))!,
                new Map(new Map<string, MalType>(null).Add("name", new Str("adam")))));
        }

        // funkcyjny Reader

        [TestMethod]
        public void ReadList__Test()
        {
            var tokens = new[] { "a", "123", ")", "nil" }.ToLList();

            var result = Reader.ReadList__(tokens, ")");

            Assert.IsNotNull(result.Result);
            Assert.AreEqual(2, result.Result.Count());

            Assert.IsTrue(MalEqual(result.Result!.Head, new Symbol("a")));
            Assert.IsTrue(MalEqual(result.Result!.Tail!.Head, new Number(123)));

            Assert.AreEqual("nil", result.RestTokens!.Head);
        }

        [TestMethod]
        public void ReadForm__Test()
        {
            Assert.IsTrue(MalEqual(Reader.ReadForm__(new[] { "nil", "true" }.ToLList()).Result!,
                NilV));

            Assert.IsTrue(MalEqual(Reader.ReadForm__(new[] { "(", "+", "1", "2", ")" }.ToLList()).Result!,
                new List(new MalType[] { new Symbol("+"), new Number(1), new Number(2) }.ToLList(), ListType.List)));

            Assert.IsTrue(MalEqual(Reader.ReadForm__(new[] { "(", "1", "[", "2", "]", ")" }.ToLList()).Result!,
                new List(new MalType[]
                {
                    new Number(1),
                    new List(new MalType[] { new Number(2) }.ToLList(), ListType.Vector)
                }.ToLList(), ListType.List)));

            Assert.IsTrue(MalEqual(Reader.ReadForm__(new[] { "{", "\"name\"", "\"adam\"", "}" }.ToLList()).Result!,
                new Map(new Map<string, MalType>(null).Add("name", new Str("adam")))));
        }
    }
}