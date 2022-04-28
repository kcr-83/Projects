using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static MiniMal.Types1;
using static MiniMal.Printer1;

namespace MiniMal.Tests
{
    [TestClass]
    public class Printer1Tests
    {
        [TestMethod]
        public void PrintStrTest()
        {
            Assert.AreEqual("nil", PrintStr(NilV));
            Assert.AreEqual("true", PrintStr(TrueV));
            Assert.AreEqual("false", PrintStr(FalseV));

            Assert.AreEqual("\"hej\"", PrintStr(new Str("hej")));
            Assert.AreEqual("bla", PrintStr(new Symbol("bla")));
            Assert.AreEqual("123", PrintStr(new Number(123)));

            var mals = new MalType[] { new Str("name"), NilV };
            Assert.AreEqual("(\"name\" nil)", PrintStr(new List(mals, ListType.List)));
            Assert.AreEqual("((\"name\" nil) nil)", PrintStr(new
                List(new MalType[] { new List(mals, ListType.List), NilV }, ListType.List)));

            var map = new Map(new() { { "name", NilV } });
            Assert.AreEqual("{\"name\" nil}", PrintStr(map));

            Assert.AreEqual("#<function>", PrintStr(new Fn(mals => mals[0])));

            Assert.AreEqual("", PrintStr(null));
        }
    }
}
