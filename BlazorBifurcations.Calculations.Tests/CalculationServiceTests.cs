using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace BlazorBifurcations.Calculations.Tests
{
    [TestClass]
    public class CalculationServiceTests
    {
        [TestMethod]
        public void CalculateFullSetTest()
        {
            var sut = new CalculationService();
            var isDone = false;

            while (!isDone)
                isDone = sut.CalculateStep();

            Assert.IsNotNull(sut.Results);
        }

    }
}
