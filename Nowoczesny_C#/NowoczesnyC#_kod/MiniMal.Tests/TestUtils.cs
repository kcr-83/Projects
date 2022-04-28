using static MiniMal.Types;
using static MiniMal.EnvM;
using static MiniMal.Core;
using PowerFP;

namespace MiniMal.Tests
{
    public static class TestUtils
    {
        internal static Env EmptyEnv(Env? outer = null) => new Env(new Map<Symbol, MalType>(null), outer);
        internal static Env DefaultEnv(Env? outer = null) => new Env(Ns, outer);

        internal static LList<MalType>? MalLListFrom(params MalType[] mals) => LListM.LListFrom(mals);
        internal static List MalListFrom(params MalType[] mals) => new List(MalLListFrom(mals), ListType.List);
    }
}