using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static MiniMal.Types1;

namespace MiniMal.Tests
{
    [TestClass]
    public class Reader1Tests
    {
        [TestMethod]
        public void ReadAtomTest()
        {
            Assert.IsTrue(Types1.MalEqual(Reader1.ReadAtom("false"), FalseV));
            Assert.IsTrue(Types1.MalEqual(Reader1.ReadAtom("true"), TrueV));
            Assert.IsTrue(Types1.MalEqual(Reader1.ReadAtom("nil"), NilV));

            Assert.IsTrue(Types1.MalEqual(Reader1.ReadAtom("abc"), new Symbol("abc")));
            Assert.IsTrue(Types1.MalEqual(Reader1.ReadAtom("\"abc\""), new Str("abc")));
            Assert.IsTrue(Types1.MalEqual(Reader1.ReadAtom("123"), new Number(123)));

            Assert.ThrowsException<Exception>(() => Reader1.ReadAtom("\"abc"));
        }

        [TestMethod]
        public void ReadListTest()
        {
            var tokens = new[] { "a", "123", ")" };
            var reader = new Reader1.ReaderObj(tokens);

            var list = (List)Reader1.ReadList(reader, ")");

            Assert.IsNotNull(list);
            Assert.AreEqual(ListType.List, list.ListType);
            Assert.AreEqual(2, list.Items.Length);
            Assert.IsTrue(MalEqual(list.Items[0], new Symbol("a")));
            Assert.IsTrue(MalEqual(list.Items[1], new Number(123)));
        }

        [TestMethod]
        public void ListToMapTest()
        {
            var nameProp = new Str("name");
            var name = new Str("adam");
            var ageProp = new Str("age");
            var age = new Number(20);

            Assert.ThrowsException<Exception>(() => Reader1.ListToMap(new MalType[] { nameProp }));
            Assert.ThrowsException<Exception>(() => Reader1.ListToMap(new MalType[] { nameProp, name, ageProp }));
            Assert.ThrowsException<Exception>(() => Reader1.ListToMap(new MalType[] { nameProp, name, age, age }));

            var map = Reader1.ListToMap(new MalType[] { nameProp, name, ageProp, age });
            Assert.IsNotNull(map);

            Assert.AreEqual(2, map.Value.Count);
            Assert.IsTrue(map.Value.ContainsKey(nameProp.Value));
            Assert.IsTrue(map.Value.ContainsKey(ageProp.Value));
            Assert.AreEqual(name.Value, ((Str)map.Value[nameProp.Value]).Value);
            Assert.AreEqual(age.Value, ((Number)map.Value[ageProp.Value]).Value);
        }


        [TestMethod]
        public void ReadFormTest()
        {
            Assert.IsTrue(MalEqual(Reader1.ReadForm(new Reader1.ReaderObj(new[] { "nil", "true" }))!,
                NilV));

            Assert.IsTrue(MalEqual(Reader1.ReadForm(new Reader1.ReaderObj(new[] { "(", "+", "1", "2", ")" }))!,
                new List(new MalType[] { new Symbol("+"), new Number(1), new Number(2) }, ListType.List)));

            Assert.IsTrue(MalEqual(Reader1.ReadForm(new Reader1.ReaderObj(new[] { "(", "1", "[", "2", "]", ")" }))!,
                new List(new MalType[]
                {
                    new Number(1),
                    new List(new MalType[] { new Number(2) }, ListType.Vector)
                }, ListType.List)));

            Assert.IsTrue(MalEqual(Reader1.ReadForm(new Reader1.ReaderObj(new[] { "{", "\"name\"", "\"adam\"", "}" }))!,
                new Map(new Dictionary<string, MalType>() { { "name", new Str("adam") } })));
        }
    }
}