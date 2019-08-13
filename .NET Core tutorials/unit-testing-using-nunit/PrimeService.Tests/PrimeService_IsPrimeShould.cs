using NUnit.Framework;
using Prime.Service;

namespace Prime.UnitTests.Services {
    [TestFixture]
    public class PrimeService_IsPrimeShould {
        private PrimeService CreatePrimeService () {
            return new PrimeService ();
        }

        [TestCase (-1)]
        [TestCase (0)]
        [TestCase (1)]
        public void IsPrime_InputIs1_ReturnFalse (int value) {
            PrimeService primeService = CreatePrimeService ();
            var result = primeService.IsPrime (value);

            Assert.IsFalse (result, $"{value} should not be prime");
        }
    }
}