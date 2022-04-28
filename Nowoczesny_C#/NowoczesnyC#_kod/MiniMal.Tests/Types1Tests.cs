
using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static MiniMal.Types1;

namespace MiniMal.Tests
{
    [TestClass]
    public class Types1Tests
    {
        [TestMethod]
        public void MalEqualTest()
        {
            var fn = new Fn(mals => mals[0]);
            Assert.IsFalse(MalEqual(fn, fn));

            Assert.IsTrue(MalEqual(FalseV, FalseV));
            Assert.IsFalse(MalEqual(FalseV, fn));

            Assert.IsTrue(MalEqual(TrueV, TrueV));
            Assert.IsFalse(MalEqual(TrueV, fn));

            Assert.IsTrue(MalEqual(NilV, NilV));
            Assert.IsFalse(MalEqual(NilV, fn));

            Assert.IsTrue(MalEqual(new Str("a"), new Str("a")));
            Assert.IsFalse(MalEqual(new Str("a"), new Str("b")));
            Assert.IsFalse(MalEqual(new Str("a"), fn));

            Assert.IsTrue(MalEqual(new Number(1), new Number(1)));
            Assert.IsFalse(MalEqual(new Number(1), new Number(2)));
            Assert.IsFalse(MalEqual(new Number(1), fn));

            Assert.IsTrue(MalEqual(new Symbol("a"), new Symbol("a")));
            Assert.IsFalse(MalEqual(new Symbol("a"), new Symbol("b")));
            Assert.IsFalse(MalEqual(new Symbol("a"), fn));


            var mals = new MalType[] { new Str("name"), NilV };
            Assert.IsTrue(MalEqual(new List(mals, ListType.List), new List(mals.ToList().ToArray(), ListType.List)));
            Assert.IsTrue(MalEqual(new List(mals, ListType.List), new List(mals.ToList().ToArray(), ListType.Vector)));
            Assert.IsFalse(MalEqual(new List(mals, ListType.List), new List(mals.Take(1).ToArray(), ListType.List)));
            Assert.IsFalse(MalEqual(new List(mals, ListType.List), new List(mals.Reverse().ToArray(), ListType.List)));
            Assert.IsFalse(MalEqual(new List(mals, ListType.List), fn));

            var map = new Map(new() { { "name", NilV } });
            Assert.IsTrue(MalEqual(map, new Map(map.Value.ToDictionary(kv => kv.Key, kv => kv.Value))));
            Assert.IsFalse(MalEqual(map, new Map(map.Value.ToDictionary(kv => kv.Key + "!", kv => kv.Value))));
            Assert.IsFalse(MalEqual(map, new Map(new())));
            Assert.IsFalse(MalEqual(map, fn));

            Assert.IsTrue(MalEqual(
                new Map(new() { { "name", NilV }, { "age", new Number(1) } }),
                new Map(new() { { "age", new Number(1) }, { "name", NilV } })));
        }
    }
}

