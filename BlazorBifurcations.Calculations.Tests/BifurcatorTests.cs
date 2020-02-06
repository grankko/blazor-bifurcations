using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BlazorBifurcations.Calculations.Tests
{
    [TestClass]
    public class BifurcatorTests
    {
        [TestMethod]
        public void CalculateStableValuesForZeroShouldGiveZeroTest()
        {
            var sut = new Bifurcator(0.5, 100, 3);
            var result = sut.Calculate(0.5);
            Assert.IsTrue(result.StableValues.Count == 1 && result.StableValues[0] == 0);
        }

        [TestMethod]
        public void CalculateStableValuesForOnePointFiveShouldGiveOneStableNonZeroValueTest()
        {
            var sut = new Bifurcator(0.5, 100, 3);
            var result = sut.Calculate(1.5);
            Assert.IsTrue(result.StableValues.Count == 1 && result.StableValues[0] > 0);
        }

        [TestMethod]
        public void CalculateStableValuesForThreePointTwoShouldGiveTwoStableValuesTest()
        {
            var sut = new Bifurcator(0.5, 100, 3);
            var result = sut.Calculate(3.2);
            Assert.IsTrue(result.StableValues.Count == 2);
        }

        [TestMethod]
        public void CalculateStableValuesForThreePointFiveShouldGiveFourStableValuesTest()
        {
            var sut = new Bifurcator(0.5, 100, 3);
            var result = sut.Calculate(3.5);
            Assert.IsTrue(result.StableValues.Count == 4);
        }

        [TestMethod]
        public void ExtractStableValuesFromRepeatingPatternTest()
        {
            var input = new Stack<double>();
            input.Push(0.01);
            input.Push(0.02);
            input.Push(0.03);
            input.Push(0.04);
            input.Push(0.05);
            input.Push(0.03);
            input.Push(0.04);

            var result = new List<double>(Bifurcator.ExtractStableValuesFromSetWithRepeatingPattern(input, 0.05));

            Assert.IsTrue(result.Count == 3);
            Assert.IsTrue(result.Contains(0.05));
            Assert.IsTrue(result.Contains(0.04));
            Assert.IsTrue(result.Contains(0.03));
        }

        [TestMethod]
        public void ExtractStableValuesFromRepeatingPatternWithOneValueTest()
        {
            var input = new Stack<double>();
            input.Push(0.01);

            var result = new List<double>(Bifurcator.ExtractStableValuesFromSetWithRepeatingPattern(input, 0.01));

            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result.Contains(0.01));
        }
    }
}
