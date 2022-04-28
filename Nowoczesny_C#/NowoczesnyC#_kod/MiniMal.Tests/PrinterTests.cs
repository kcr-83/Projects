using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static MiniMal.Types;
using PowerFP;

namespace MiniMal.Tests
{
    [TestClass]
    public class PrinterTests
    {
        [TestMethod]
        public void PrintStrTest()
        {
            Assert.AreEqual("nil", Printer.PrintStr(NilV));
            Assert.AreEqual("true", Printer.PrintStr(TrueV));
            Assert.AreEqual("false", Printer.PrintStr(FalseV));

            Assert.AreEqual("\"hej\"", Printer.PrintStr(new Str("hej")));
            Assert.AreEqual("bla", Printer.PrintStr(new Symbol("bla")));
            Assert.AreEqual("123", Printer.PrintStr(new Number(123)));

            var mals = new MalType[] { new Str("name"), NilV }.ToLList();
            Assert.AreEqual("(\"name\" nil)", Printer.PrintStr(new List(mals, ListType.List)));
            Assert.AreEqual("((\"name\" nil) nil)", Printer.PrintStr(new
                List(new MalType[] { new List(mals, ListType.List), NilV }.ToLList(), ListType.List)));

            var map = new Map(new(new(("name", NilV), null)));
            Assert.AreEqual("{\"name\" nil}", Printer.PrintStr(map));

            Assert.AreEqual("#<function>", Printer.PrintStr(new Fn(mals => mals!.Head)));

            Assert.AreEqual("", Printer.PrintStr(null));
        }
    }
}
