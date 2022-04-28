
using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static MiniMal.Types;
using static MiniMal.Reader;
using static MiniMal.EnvM;
using static PowerFP.LListM;
using static MiniMal.Tests.TestUtils;

namespace MiniMal.Tests
{

    [TestClass]
    public class EvalTests
    {
        [TestMethod]
        public void ApplyDefTest()
        {
            var env1 = EmptyEnv();

            Assert.ThrowsException<Exception>(() => EvalM.ApplyDef(MalLListFrom(), env1));
            Assert.ThrowsException<Exception>(() => EvalM.ApplyDef(MalLListFrom(new Symbol("a")), env1));
            Assert.ThrowsException<Exception>(() => EvalM.ApplyDef(MalLListFrom(
                new Symbol("a"), new Number(1), new Number(1)), env1));

            var mal1 = EvalM.ApplyDef(MalLListFrom(new Symbol("x"), new Number(1)), env1);

            Assert.AreEqual(new Number(1), mal1);
            Assert.AreEqual(new Number(1), env1.Get(new Symbol("x")));


            var env2 = EmptyEnv(DefaultEnv());
            var mal2 = EvalM.ApplyDef(MalLListFrom(
                new Symbol("y"),
                MalListFrom(new Symbol("+"), new Number(1), new Number(2))
                ), env2);

            Assert.AreEqual(new Number(3), mal2);
            Assert.AreEqual(new Number(3), env2.Get(new Symbol("y")));
        }

        [TestMethod]
        public void ApplyBindingTest()
        {
            Assert.ThrowsException<Exception>(() => EvalM.ApplyBindings(MalLListFrom(new Symbol("a")), EmptyEnv()));

            Assert.ThrowsException<Exception>(() => EvalM.ApplyBindings(
                MalLListFrom(new Symbol("a"), new Number(1), new Symbol("b")), EmptyEnv()));

            var env1 = EmptyEnv();
            var envResult1 = EvalM.ApplyBindings(null, env1);
            Assert.AreSame(env1, envResult1);

            var env2 = EmptyEnv();
            var envResult2 = EvalM.ApplyBindings(MalLListFrom(
                new Symbol("a"),
                new Number(1),
                new Symbol("b"),
                new Number(2)
                ), env2);

            Assert.AreSame(env2, envResult2);
            Assert.AreEqual(new Number(1), env2.Get(new Symbol("a")));
            Assert.AreEqual(new Number(2), env2.Get(new Symbol("b")));
        }



        [TestMethod]
        public void ApplyLetTest()
        {
            Assert.ThrowsException<Exception>(() => EvalM.ApplyLet(MalLListFrom(new Symbol("a")), EmptyEnv()));
            Assert.ThrowsException<Exception>(() => EvalM.ApplyLet(MalLListFrom(MalListFrom(new Symbol("a"), new Number(1))), EmptyEnv()));

            var malResult = EvalM.ApplyLet(MalLListFrom(
                MalListFrom(new Symbol("a"), new Number(1), new Symbol("b"), new Number(3)),
                MalListFrom(new Symbol("+"), new Symbol("a"), new Symbol("b"))
                ), DefaultEnv());

            Assert.AreEqual(new Number(4), malResult);
        }


        [TestMethod]
        public void ApplyDoTest()
        {
            Assert.ThrowsException<Exception>(() => EvalM.ApplyDo(MalLListFrom(), EmptyEnv()));
            var malResult = EvalM.ApplyDo(MalLListFrom(NilV, MalListFrom(new Symbol("+"), new Number(1), new Number(3))), DefaultEnv());
            Assert.AreEqual(new Number(4), malResult);
        }

        [TestMethod]
        public void ApplyIfTest()
        {
            Assert.ThrowsException<Exception>(() => EvalM.ApplyIf(MalLListFrom(
                ), EmptyEnv()));

            Assert.ThrowsException<Exception>(() => EvalM.ApplyIf(MalLListFrom(
                new Symbol("a")
                ), EmptyEnv()));

            Assert.ThrowsException<Exception>(() => EvalM.ApplyIf(MalLListFrom(
                new Symbol("a"),
                new Symbol("a"),
                new Symbol("a"),
                new Symbol("a")
                ), EmptyEnv()));

            // falsy values
            Assert.AreEqual(new Number(2),
                EvalM.ApplyIf(MalLListFrom(FalseV, new Number(1), new Number(2)), EmptyEnv()));
            Assert.AreEqual(new Number(2),
                EvalM.ApplyIf(MalLListFrom(NilV, new Number(1), new Number(2)), EmptyEnv()));
            Assert.AreEqual(NilV,
                EvalM.ApplyIf(MalLListFrom(NilV, new Number(1)), EmptyEnv()));

            //truthy values
            Assert.AreEqual(new Number(1),
                EvalM.ApplyIf(MalLListFrom(TrueV, new Number(1), new Number(2)), EmptyEnv()));
            Assert.AreEqual(new Number(1),
                EvalM.ApplyIf(MalLListFrom(new Str(""), new Number(1), new Number(2)), EmptyEnv()));
            Assert.AreEqual(new Number(1),
                EvalM.ApplyIf(MalLListFrom(new List(null, ListType.Vector), new Number(1), new Number(2)), EmptyEnv()));
        }

        [TestMethod]
        public void BindFunctionArgumentsTest()
        {
            Symbol a = new Symbol("a"), b = new Symbol("b"), amp = new Symbol("&");
            MalType one = new Number(1), two = new Number(2), three = new Number(3);

            Assert.AreEqual(LListFrom((a, one)), EvalM.BindFunctionArguments(MalLListFrom(a), LListFrom(one)));
            Assert.AreEqual(null, EvalM.BindFunctionArguments(MalLListFrom(), LListFrom(one)));
            Assert.AreEqual(LListFrom((a, one)), EvalM.BindFunctionArguments(MalLListFrom(a), LListFrom(one, two)));
            Assert.ThrowsException<Exception>(() => EvalM.BindFunctionArguments(MalLListFrom(a), MalLListFrom()));

            Assert.AreEqual(
                LListFrom((a, one), (b, new List(LListFrom(two, three), ListType.List))),
                EvalM.BindFunctionArguments(MalLListFrom(a, amp, b), LListFrom(one, two, three)));
            Assert.AreEqual(
                LListFrom((a, one), (b, new List(null, ListType.List))),
                EvalM.BindFunctionArguments(MalLListFrom(a, amp, b), LListFrom(one)));

            Assert.ThrowsException<Exception>(() => Assert.AreEqual(
                LListFrom((a, one), (b, new List(null, ListType.List))),
                EvalM.BindFunctionArguments(MalLListFrom(a, amp), LListFrom(one))));
        }

        [TestMethod]
        public void ApplyFnTest()
        {
            Assert.ThrowsException<Exception>(() => EvalM.ApplyFn(MalLListFrom(), EmptyEnv()));
            Assert.ThrowsException<Exception>(() => EvalM.ApplyFn(MalLListFrom(new Number(1), new Number(1)), EmptyEnv()));
            Assert.ThrowsException<Exception>(() => EvalM.ApplyFn(MalLListFrom(MalListFrom(), new Number(1), new Number(1)), EmptyEnv()));


            Symbol a = new Symbol("a"), b = new Symbol("b"), plus = new Symbol("+");
            MalType one = new Number(1), two = new Number(2);

            var fn = (Fn)EvalM.ApplyFn(MalLListFrom(MalListFrom(a, b), MalListFrom(plus, a, b)), DefaultEnv());

            Assert.AreEqual(new Number(3), fn.Value(LListFrom(one, two)));
        }

        // [TestMethod]
        // public void ApplyQuoteTest()
        // {
        //     Assert.ThrowsException<Exception>(() => EvalM.ApplyQuote(MalLListFrom(), EmptyEnv()));
        //     Assert.ThrowsException<Exception>(() => EvalM.ApplyFn(MalLListFrom(new Number(1), new Number(1)), EmptyEnv()));

        //     Assert.AreEqual(new Number(1), EvalM.ApplyQuote(MalLListFrom(new Number(1)), EmptyEnv()));
        // }

        // [TestMethod]
        // public void TransformQuasiquoteTest()
        // {
        //     Assert.AreEqual(new Number(1), EvalM.TransformQuasiquote(new Number(1)));
        //     Assert.AreEqual(
        //         MalListFrom(new Symbol("quote"), new Symbol("abc")),
        //         EvalM.TransformQuasiquote(new Symbol("abc")));

        //     Assert.AreEqual("(cons 1 (cons 2 ()))", Printer.PrintStr(EvalM.TransformQuasiquote(
        //         MalListFrom(new Number(1), new Number(2)))));


        //     var oneTwoList = MalListFrom(new Number(1), new Number(2));

        //     var mal = EvalM.TransformQuasiquote(
        //         MalListFrom(
        //             new Number(1),
        //             MalListFrom(new Symbol("unquote"), oneTwoList),
        //             new Number(4),
        //             MalListFrom(new Symbol("splice-unquote"), oneTwoList)
        //             ));

        //     Assert.AreEqual("(cons 1 (cons (1 2) (cons 4 (concat (1 2) ()))))", Printer.PrintStr(mal));
        // }

        // [TestMethod]
        // public void ApplyDefMacroTest()
        // {
        //     var env1 = EmptyEnv();

        //     Assert.ThrowsException<Exception>(() => EvalM.ApplyDefMacro(MalLListFrom(), env1));
        //     Assert.ThrowsException<Exception>(() => EvalM.ApplyDefMacro(MalLListFrom(new Symbol("a")), env1));
        //     Assert.ThrowsException<Exception>(() => EvalM.ApplyDefMacro(MalLListFrom(new Symbol("a"), new Number(1)), env1));

        //     var mal1 = EvalM.ApplyDefMacro(MalLListFrom(
        //         new Symbol("x"),
        //         MalListFrom(new Symbol("fn*"), MalListFrom())
        //         ), env1);

        //     Assert.IsTrue(mal1 is Fn { IsMacro: true });
        // }


        // [TestMethod]
        // public void IsMacroCallTest()
        // {

        //     var env1 = EmptyEnv();
        //     env1.Set(new Symbol("number"), new Number(1));
        //     env1.Set(new Symbol("function"), new Fn(args => NilV, false));
        //     env1.Set(new Symbol("macro"), new Fn(args => NilV, true));

        //     Assert.ThrowsException<Exception>(() => EvalM.IsMacroCall(MalListFrom(new Symbol("number__")), env1));

        //     Assert.IsFalse(EvalM.IsMacroCall(MalListFrom(new Symbol("number")), env1));
        //     Assert.IsFalse(EvalM.IsMacroCall(MalListFrom(new Symbol("function")), env1));
        //     Assert.IsTrue(EvalM.IsMacroCall(MalListFrom(new Symbol("macro")), env1));

        // }


        // [TestMethod]
        // public void TryCatchTest()
        //         {
        //             var emptyEnv = EmptyEnv();

        //         Assert.ThrowsException<Exception>(() => EvalM.ApplyTryCatch(MalLListFrom(TrueV), emptyEnv));
        //             Assert.ThrowsException<Exception>(() => EvalM.ApplyTryCatch(MalLListFrom(TrueV, MalListFrom(new Symbol("catch*"), new Symbol("err"), TrueV, FalseV)), emptyEnv));

        //             Assert.AreEqual(TrueV, EvalM.ApplyTryCatch(
        //                 MalLListFrom(TrueV, MalListFrom(new Symbol("catch*"), new Symbol("err"), FalseV)), emptyEnv));

        //             Assert.AreEqual(FalseV, EvalM.ApplyTryCatch(
        //                 MalLListFrom(new Symbol("xx"), MalListFrom(new Symbol("catch*"), new Symbol("err"), FalseV)), emptyEnv));

        //             var defaultEnv = DefaultEnv();

        //         Assert.AreEqual(new Number(666), EvalM.ApplyTryCatch(
        //             MalLListFrom(MalListFrom(new Symbol("throw"), new Number(666)), MalListFrom(new Symbol("catch*"), new Symbol("err"), new Symbol("err"))), defaultEnv));

        //         }
    }
}