
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static MiniMal.EnvM;
using static MiniMal.Types;
using PowerFP;
using static PowerFP.LListM;
using static MiniMal.Core;
using System;
using System.Linq;
using static MiniMal.Tests.TestUtils;

namespace MiniMal.Tests
{
    [TestClass]
    public class CoreTests
    {
        [TestMethod]
        public void ExecuteArithmeticFnTest()
        {
            Func<double, double, double> add = (a, b) => a + b;

            Assert.ThrowsException<Exception>(() => Core.ExecuteArithmeticFn(null, add));
            Assert.ThrowsException<Exception>(() => Core.ExecuteArithmeticFn(new(new Number(1), null), add));
            Assert.ThrowsException<Exception>(() => Core.ExecuteArithmeticFn(new(new Number(1), new(new Str("2"), null)), add));

            Assert.AreEqual(new Number(1 + 2 + 3), Core.ExecuteArithmeticFn(new Number[] { new(1), new(2), new(3) }.Cast<MalType>().ToLList(), add));
        }

        [TestMethod]
        public void ExecuteComparisonFnTest()
        {
            Func<double, double, bool> lessThen = (a, b) => a < b;

            Assert.ThrowsException<Exception>(() => Core.ExecuteComparisonFn(null, lessThen));
            Assert.ThrowsException<Exception>(() => Core.ExecuteComparisonFn(MalLListFrom(new Number(1)), lessThen));
            Assert.ThrowsException<Exception>(() => Core.ExecuteComparisonFn(MalLListFrom(new Number(1), new Number(2), new Number(3)), lessThen));
            Assert.ThrowsException<Exception>(() => Core.ExecuteComparisonFn(MalLListFrom(new Str("1"), new Str("2")), lessThen));

            Assert.AreEqual(TrueV, Core.ExecuteComparisonFn(MalLListFrom(new Number(123), new Number(124)), lessThen));
            Assert.AreEqual(FalseV, Core.ExecuteComparisonFn(MalLListFrom(new Number(123), new Number(123)), lessThen));
        }

        [TestMethod]
        public void ListFnTest()
        {
            Assert.AreEqual(new List(null, ListType.List), Core.ListFn(MalLListFrom()));
            Assert.AreEqual(new List(new(new Number(1), null), ListType.List), Core.ListFn(MalLListFrom(new Number(1))));
            Assert.AreEqual(new List(new(new Number(1), new(new Number(2), null)), ListType.List),
                Core.ListFn(MalLListFrom(new Number(1), new Number(2))));
        }


        [TestMethod]
        public void IsListFnTest()
        {
            Assert.ThrowsException<Exception>(() => Core.IsListFn(null));
            Assert.ThrowsException<Exception>(() => Core.IsListFn(MalLListFrom(new Number(1), new Number(2))));

            Assert.AreEqual(TrueV, Core.IsListFn(MalLListFrom(new List(new(new Number(1), null), ListType.List))));
            Assert.AreEqual(FalseV, Core.IsListFn(MalLListFrom(new List(new(new Number(1), null), ListType.Vector))));
            Assert.AreEqual(FalseV, Core.IsListFn(MalLListFrom(new Number(1))));
        }


        [TestMethod]
        public void IsEmptyFnTest()
        {
            Assert.ThrowsException<Exception>(() => Core.IsEmptyFn(null));
            Assert.ThrowsException<Exception>(() => Core.IsEmptyFn(MalLListFrom(new Number(1))));
            Assert.ThrowsException<Exception>(() => Core.IsEmptyFn(MalLListFrom(new Number(1), new Number(2))));

            Assert.AreEqual(TrueV, Core.IsEmptyFn(MalLListFrom(new List(null, ListType.List))));
            Assert.AreEqual(TrueV, Core.IsEmptyFn(MalLListFrom(new List(null, ListType.Vector))));
            Assert.AreEqual(FalseV, Core.IsEmptyFn(MalLListFrom(new List(new(new Number(1), null), ListType.List))));
        }



        [TestMethod]
        public void CountFnTest()
        {
            Assert.ThrowsException<Exception>(() => Core.CountFn(null));
            Assert.ThrowsException<Exception>(() => Core.CountFn(MalLListFrom(new Number(1))));
            Assert.ThrowsException<Exception>(() => Core.CountFn(MalLListFrom(new Number(1), new Number(2))));

            Assert.AreEqual(new Number(0), Core.CountFn(MalLListFrom(new List(null, ListType.List))));
            Assert.AreEqual(new Number(0), Core.CountFn(MalLListFrom(new List(null, ListType.Vector))));
            Assert.AreEqual(new Number(1), Core.CountFn(MalLListFrom(new List(new(new Number(100), null), ListType.List))));
            Assert.AreEqual(new Number(0), Core.CountFn(MalLListFrom(NilV)));
        }

        [TestMethod]
        public void EqualsFnTest()
        {
            Assert.ThrowsException<Exception>(() => Core.EqualsFn(null));
            Assert.ThrowsException<Exception>(() => Core.EqualsFn(MalLListFrom(new Number(1))));
            Assert.ThrowsException<Exception>(() => Core.EqualsFn(MalLListFrom(new Number(1), new Number(2), new Number(3))));

            Assert.AreEqual(TrueV, Core.EqualsFn(MalLListFrom(new Number(123), new Number(123))));
            Assert.AreEqual(FalseV, Core.EqualsFn(MalLListFrom(new Number(123), new Number(1230))));
            Assert.AreEqual(FalseV, Core.EqualsFn(MalLListFrom(new Number(123), new Str("123"))));

            Assert.AreEqual(TrueV, Core.EqualsFn(MalLListFrom(
                new List(new(new Number(4), null), ListType.Vector),
                new List(new(new Number(4), null), ListType.Vector)
            )));
        }




        [TestMethod]
        public void ReadStringFnTest()
        {
            Assert.ThrowsException<Exception>(() => Core.ReadStringFn(null));
            Assert.ThrowsException<Exception>(() => Core.ReadStringFn(MalLListFrom(new Number(1))));
            Assert.ThrowsException<Exception>(() => Core.ReadStringFn(MalLListFrom(new Str(""), new Str(""))));

            Assert.AreEqual(new Number(123), Core.ReadStringFn(MalLListFrom(new Str("123"))));
            Assert.AreEqual(NilV, Core.ReadStringFn(MalLListFrom(new Str(""))));
        }




        [TestMethod]
        public void ConstTest()
        {
            Assert.ThrowsException<Exception>(() => Core.ConsFn(null));
            Assert.ThrowsException<Exception>(() => Core.ConsFn(MalLListFrom(new Number(1))));
            Assert.ThrowsException<Exception>(() => Core.ConsFn(MalLListFrom(new Number(1), new Number(2))));

            Assert.AreEqual(
                new List(MalLListFrom(new Number(1), new Number(2)), ListType.List),
                Core.ConsFn(MalLListFrom(new Number(1), new List(new(new Number(2), null), ListType.List))));
        }

        [TestMethod]
        public void ConcatTest()
        {
            Assert.ThrowsException<Exception>(() => Core.ConcatFn(MalLListFrom(new Number(1))));

            Assert.AreEqual(new List(null, ListType.List), Core.ConcatFn(null));
            Assert.AreEqual(
                new List(MalLListFrom(new Number(1), new Number(2), new Number(3)), ListType.List),
                Core.ConcatFn(MalLListFrom(
                    new List(null, ListType.List),
                    new List(MalLListFrom(new Number(1), new Number(2)), ListType.List),
                    new List(null, ListType.List),
                    new List(MalLListFrom(new Number(3)), ListType.List),
                    new List(null, ListType.List)
                )));
        }

        [TestMethod]
        public void VecTest()
        {
            Assert.ThrowsException<Exception>(() => Core.VecFn(MalLListFrom(new Number(1))));
            Assert.ThrowsException<Exception>(() => Core.VecFn(MalLListFrom()));

            Assert.AreEqual(
                new List(MalLListFrom(new Number(1)), ListType.Vector),
                Core.VecFn(new(new List(MalLListFrom(new Number(1)), ListType.Vector), null)));
            Assert.AreEqual(
                new List(MalLListFrom(new Number(1)), ListType.Vector),
                Core.VecFn(new(new List(MalLListFrom(new Number(1)), ListType.List), null)));

        }

        [TestMethod]
        public void NthTest()
        {
            Assert.ThrowsException<Exception>(() => Core.NthFn(MalLListFrom(MalListFrom(), new Number(0))));

            Assert.AreEqual(new Number(123),
                Core.NthFn(MalLListFrom(MalListFrom(new Number(123), new Number(456)), new Number(0))));
        }

        [TestMethod]
        public void FirstTest()
        {
            Assert.ThrowsException<Exception>(() => Core.FirstFn(MalLListFrom()));
            Assert.ThrowsException<Exception>(() => Core.FirstFn(MalLListFrom(MalListFrom(), new Number(0))));

            Assert.AreEqual(new Number(123), Core.FirstFn(MalLListFrom(MalListFrom(new Number(123), new Number(456)))));
            Assert.AreEqual(NilV, Core.FirstFn(MalLListFrom(MalListFrom())));
            Assert.AreEqual(NilV, Core.FirstFn(MalLListFrom(NilV)));
        }


        [TestMethod]
        public void RestTest()
        {
            Assert.ThrowsException<Exception>(() => Core.RestFn(MalLListFrom()));
            Assert.ThrowsException<Exception>(() => Core.RestFn(MalLListFrom(MalListFrom(), new Number(0))));

            Assert.AreEqual(MalListFrom(new Number(456)), Core.RestFn(MalLListFrom(MalListFrom(new Number(123), new Number(456)))));
            Assert.AreEqual(MalListFrom(), Core.RestFn(MalLListFrom(MalListFrom())));
            Assert.AreEqual(MalListFrom(), Core.RestFn(MalLListFrom(NilV)));
        }





        [TestMethod]
        public void AssocTest()
        {
            Assert.ThrowsException<Exception>(() => Core.AssocFn(MalLListFrom()));
            Assert.ThrowsException<Exception>(() => Core.AssocFn(MalLListFrom(new Number(0))));

            var malMap = new Map(MapM.MapFrom(LListM.LListFrom(
                ("a", new Number(1) as MalType),
                ("b", new Number(2))
                )));

            Assert.ThrowsException<Exception>(() => Core.AssocFn(MalLListFrom(malMap, new Str("c"))));

            Assert.AreEqual(
                malMap with { Value = malMap.Value.Add("c", new Number(3)).Add("d", new Number(4)) },
                Core.AssocFn(MalLListFrom(malMap, new Str("c"), new Number(3), new Str("d"), new Number(4)))
                );
        }

        [TestMethod]
        public void DissocTest()
        {
            Assert.ThrowsException<Exception>(() => Core.DissocFn(MalLListFrom()));
            Assert.ThrowsException<Exception>(() => Core.DissocFn(MalLListFrom(new Number(0))));

            var malMap = new Map(MapM.MapFrom(LListM.LListFrom(
                ("a", new Number(1) as MalType),
                ("b", new Number(2))
                )));

            Assert.AreEqual(
                new Map(MapM.MapFrom(malMap.Value.Items.Where(kv => kv.Key != "b"))),
                Core.DissocFn(MalLListFrom(malMap, new Str("b")))
                );
        }
    }
}