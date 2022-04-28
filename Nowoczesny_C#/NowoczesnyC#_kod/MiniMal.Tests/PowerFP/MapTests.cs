
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PowerFP;
using static PowerFP.LListM;

namespace Mal.Tests
{
    [TestClass]
    public class MapTests
    {
        [TestMethod]
        public void AddTests()
        {
            Map<int, string> map = new Map<int, string>(null);

            Assert.AreEqual(LListFrom((5, "5!")), map.Add(5, "5!").Items);
            Assert.AreEqual(LListFrom((5, "5!"), (10, "10!")), map.Add(5, "5!").Add(10, "10!").Items);
            Assert.AreEqual(LListFrom((5, "55!")), map.Add(5, "5!").Add(5, "55!").Items);
        }

        [TestMethod]
        public void MapFromTests()
        {
            Assert.AreEqual(LListFrom((5, "5!"), (10, "10!")), MapM.MapFrom(LListFrom((5, "5!"), (10, "10!"))).Items);
            Assert.AreEqual(LListFrom((5, "5!"), (10, "10!")), MapM.MapFrom(LListFrom((5, "5!"), (10, "10!"))).Items);
        }

        [TestMethod]
        public void TryFindTests()
        {
            Map<int, string> map = new Map<int, string>(null).Add(5, "5!").Add(10, "10!");

            Assert.AreEqual((false, default(string)), map.TryFind(1));
            Assert.AreEqual((true, "5!"), map.TryFind(5));
        }
    }
}