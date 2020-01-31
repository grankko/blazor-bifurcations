using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlazorBifurcations.Calculations.Tests
{
    [TestClass]
    public class BifurcatorTests
    {
        [TestMethod]
        public void CalculateTest()
        {
            var sut = new Bifurcator(0.6, 100, 4);
            var result = sut.Calculate(3.5);
        }

        [TestMethod]
        public void CalculateOverFertilityTest()
        {
            var sut = new Bifurcator(0.6, 1000, 4);
            var result = sut.CalculateOverFertility(0.77, 4,0.01);
        }
    }
}
