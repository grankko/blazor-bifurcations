using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlazorBifurcations.Calculations.Tests
{
    [TestClass]
    public class BifurcatorTests
    {
        [TestMethod]
        public void CalculateStableValuesForZeroShouldGiveZero()
        {
            var sut = new Bifurcator(0.5, 100, 3);
            var result = sut.Calculate(0.5);
            Assert.IsTrue(result.StableValues.Count == 1 && result.StableValues[0] == 0);
        }

        [TestMethod]
        public void CalculateStableValuesForOnePointFiveShouldGiveOneStableNonZeroValue()
        {
            var sut = new Bifurcator(0.5, 100, 3);
            var result = sut.Calculate(1.5);
            Assert.IsTrue(result.StableValues.Count == 1 && result.StableValues[0] > 0);
        }

        [TestMethod]
        public void CalculateStableValuesForThreePointTwoShouldGiveTwoStableValues()
        {
            var sut = new Bifurcator(0.5, 100, 3);
            var result = sut.Calculate(3.2);
            Assert.IsTrue(result.StableValues.Count == 2);
        }

        [TestMethod]
        public void CalculateStableValuesForThreePointFiveShouldGiveFourStableValues()
        {
            var sut = new Bifurcator(0.5, 100, 3);
            var result = sut.Calculate(3.5);
            Assert.IsTrue(result.StableValues.Count == 4);
        }
    }
}
