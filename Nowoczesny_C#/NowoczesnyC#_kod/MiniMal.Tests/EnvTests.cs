
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static MiniMal.EnvM;
using static MiniMal.Types;
using PowerFP;
using System;
using System.Linq;

namespace MiniMal.Tests
{
    [TestClass]
    public class EnvTests
    {
        [TestMethod]
        public void EnvTest()
        {
            var a = new Symbol("a");
            var b = new Symbol("b");
            var c = new Symbol("c");
            var d = new Symbol("d");

            var env1 = new Env(
                new Map<Symbol, MalType>(null).Add(a, new Str("a")).Add(b, new Str("b")),
                null);

            var env2 = new Env(
                new Map<Symbol, MalType>(null).Add(c, new Str("c")),
                env1);


            env2.Set(d, new Str("d"));

            Assert.AreSame(env1, env2.Find(a));
            Assert.AreSame(env1, env2.Find(b));
            Assert.AreSame(env2, env2.Find(c));
            Assert.AreSame(env2, env2.Find(d));
            Assert.IsNull(env2.Find(new Symbol("e")));

            Assert.AreEqual(new Str("a"), env2.Get(a));
            Assert.AreEqual(new Str("b"), env2.Get(b));
            Assert.AreEqual(new Str("c"), env2.Get(c));
            Assert.AreEqual(new Str("d"), env2.Get(d));
            Assert.ThrowsException<Exception>(() => env2.Get(new Symbol("e")));
        }
    }
}
