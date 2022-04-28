
using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static MiniMal.Types;
using PowerFP;

namespace MiniMal.Tests
{
    [TestClass]
    public class TypesTests
    {
        [TestMethod]
        public void MalEqualTest()
        {
            var fn = new Fn(mals => mals!.Head);
            Assert.IsFalse(Types.MalEqual(fn, fn));

            Assert.IsTrue(Types.MalEqual(FalseV, FalseV));
            Assert.IsFalse(Types.MalEqual(FalseV, fn));

            Assert.IsTrue(Types.MalEqual(TrueV, TrueV));
            Assert.IsFalse(Types.MalEqual(TrueV, fn));

            Assert.IsTrue(Types.MalEqual(NilV, NilV));
            Assert.IsFalse(Types.MalEqual(NilV, fn));

            Assert.IsTrue(Types.MalEqual(new Str("a"), new Str("a")));
            Assert.IsFalse(Types.MalEqual(new Str("a"), new Str("b")));
            Assert.IsFalse(Types.MalEqual(new Str("a"), fn));

            Assert.IsTrue(Types.MalEqual(new Number(1), new Number(1)));
            Assert.IsFalse(Types.MalEqual(new Number(1), new Number(2)));
            Assert.IsFalse(Types.MalEqual(new Number(1), fn));

            Assert.IsTrue(Types.MalEqual(new Symbol("a"), new Symbol("a")));
            Assert.IsFalse(Types.MalEqual(new Symbol("a"), new Symbol("b")));
            Assert.IsFalse(Types.MalEqual(new Symbol("a"), fn));


            var mals = new MalType[] { new Str("name"), NilV }.ToLList();
            Assert.IsTrue(Types.MalEqual(new List(mals, ListType.List), new List(mals.Select(x => x), ListType.List)));
            Assert.IsTrue(Types.MalEqual(new List(mals, ListType.List), new List(mals.Select(x => x), ListType.Vector)));
            Assert.IsFalse(Types.MalEqual(new List(mals, ListType.List), new List(mals.Take(1), ListType.List)));
            Assert.IsFalse(Types.MalEqual(new List(mals, ListType.List), new List(mals.Reverse(), ListType.List)));
            Assert.IsFalse(Types.MalEqual(new List(mals, ListType.List), fn));

            var map = new Map(new(new(("name", NilV), null)));
            Assert.IsTrue(Types.MalEqual(map, new Map(new(map.Value.Items.Select(x => x)))));
            Assert.IsFalse(Types.MalEqual(map, new Map(new(map.Value.Items.Select(x => (x.Key + "!", x.Value))))));
            Assert.IsFalse(Types.MalEqual(map, new Map(new(null))));
            Assert.IsFalse(Types.MalEqual(map, fn));

            Assert.IsTrue(MalEqual(
                new Map(new(new(("name", NilV), new(("age", new Number(1)), null)))),
                new Map(new(new(("age", new Number(1)), new(("name", NilV), null))))));
        }
    }
}

