using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace MiniMal.Test;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestMethod1()
    {
        var items = Program.ProcessItems(new[] { "", "a", "bbbb", "cc", "x" }).ToArray();

            Console.WriteLine(string.Join(" ", items));

            Assert.AreEqual(4, items.Length);
            Assert.AreEqual("A", items[0]);
            Assert.AreEqual("X", items[1]);
            Assert.AreEqual("CC", items[2]);
            Assert.AreEqual("BBBB", items[3]);
    }
}