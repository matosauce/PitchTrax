using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Statistics;

namespace StatisticsTests
{
    [TestClass]
    public class TrendlineTests
    {
        [TestMethod]
        public void FitLine_Linear_RSquared1()
        {
            var fitter = new LineFitting();
            var line = fitter.FitLine(new double[] {0, 1, 2}, new double[] {0, 1, 2}, 1);
            Assert.AreEqual(line[0],0);
            Assert.AreEqual(line[1],1);
            Assert.AreEqual(fitter.RSquared(line, new double[] { 3, 4, 5 }, new double[] { 3, 4, 5 }), 1);
        }

        [TestMethod]
        public void FitLine_XSquared_RSquared1()
        {
            var fitter = new LineFitting();
            var line = fitter.FitLine(new double[] { 0, 1, 2 }, new double[] { 0, 1, 4 }, 2);
            Assert.AreEqual(Math.Round(line[0], 2), 0);
            Assert.AreEqual(Math.Round(line[1], 2), 0);
            Assert.AreEqual(Math.Round(line[2], 2), 1);
            Assert.AreEqual(fitter.RSquared(line, new double[] { 3, 4, 5 }, new double[] { 9, 16, 25 }), 1);
        }
    }
}