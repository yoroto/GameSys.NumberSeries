using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace GameSys.NumberSeries.Tests
{
    [TestFixture]
    public class NumberSeriesTests
    {
        [Test]
        [TestCase(1.62, Result = 1.5)]
        [TestCase(-1.62, Result = -1.5)]
        [TestCase(1.63, Result = 1.75)]
        [TestCase(-1.63, Result = -1.75)]
        [TestCase(6.561, Result = 6.5)]
        [TestCase(10.625, Result = 10.75)]
        [TestCase(0, Result = 0)]
        public decimal RoundTest(decimal input)
            => NumberSeries.Round(input);

        [Test]
        [TestCase(1, Result = 1.62)]
        [TestCase(-1, Result = -0.78)]
        [TestCase(10.3, Result = 14.8818)]
        [TestCase(0, Result = 0.4)]
        [TestCase(0.4, Result = 0.8832)]
        public decimal GetFirstNumberTest(decimal x)
            => NumberSeries.GetFirstNumber(x);

        [Test]
        [TestCase(5062.5, Result = 4.05)]
        public decimal GetGrowthRateTest(decimal y)
            => NumberSeries.GetGrowthRate(y);

        [Test]
        [TestCase(1.62, 4.05, 1, Result = 4.05)]
        [TestCase(1.62, 4.05, 2, Result = 6.561)]
        [TestCase(1.62, 4.05, 3, Result = 10.62882)]
        [TestCase(1.62, 4.05, 4, Result = 17.2186884)]
        [TestCase(-0.78, 4.05, 1, Result = 4.05)]
        [TestCase(-0.78, 4.05, 2, Result = -3.159)]
        [TestCase(1.62, 4.05, 0, ExpectedException = typeof(ArgumentOutOfRangeException))]
        [TestCase(1.62, 4.05, -1, ExpectedException = typeof(ArgumentOutOfRangeException))]
        public decimal GetNextTest(decimal first, decimal growthRate, int index)
            => NumberSeries.Next(first, growthRate, index);

        [Test]
        [TestCaseSource(typeof(NumberSeriesTestCaseFactory), nameof(NumberSeriesTestCaseFactory.TestCases))]
        public IEnumerable<decimal> GenerateSeriesTest(decimal x, decimal y, int length)
            => new NumberSeries(x, y, length).Result;

        [Test]
        [TestCase(1, 5062.5, 5, Result = 6.5)]
        [TestCase(1, 5062.5, 4, Result = 4)]
        [TestCase(1, 5062.5, 2, ExpectedException = typeof(ArgumentOutOfRangeException))]
        [TestCase(-1, 5062.5, 6, Result = 1.5)]
        public decimal ThirdLargestTest(decimal x, decimal y, int length)
            => new NumberSeries(x, y, length).ThirdLargestNumber;

        [Test]
        [TestCase(1, 5062.5, 5, 3158, Result = 1.5)]
        [TestCase(1, 5062.5, 5, 266, Result = 4)]
        [TestCase(1, 5062.5, 5, -566, Result = 1.5,
            Description = "The approximate number less than the smallest number in the series.")]
        [TestCase(2, 6250, 4, 250, Result = 5,
            Description = "If two numbers are evenly apart from the approximateNumber the highest number is chosen.")]
        [TestCase(-2, 6250, 5, 50, Result = 18.5)]
        [TestCase(-2, 6250, 5, -240, Result = -2)]
        public decimal ClosestNumberTest(decimal x, decimal y, int length, decimal z)
            => new NumberSeries(x, y, length).ClosestNumber(z);
    }

    public class NumberSeriesTestCaseFactory
    {
        public static IEnumerable TestCases
        {
            get
            {
                yield return new TestCaseData(1m, 5062.5m, 5).Returns(new[] { 1.5m, 4m, 6.5m, 10.75m, 17.25m });
                yield return new TestCaseData(1m, 5062.5m, 1).Returns(new[] { 1.5m });
                yield return new TestCaseData(2m, 6250m, 7).Returns(new[] { 3m, 5m, 14.5m, 41.5m, 119.5m, 344m, 990.75m});
                yield return new TestCaseData(1m, 5062.5m, 0).Returns(new decimal[0]).Throws(typeof(ArgumentOutOfRangeException));
                yield return new TestCaseData(1m, 5062.5m, -1).Returns(new decimal[0]).Throws(typeof(ArgumentOutOfRangeException));
                yield return new TestCaseData(-1m, 5062.5m, 6).Returns(new[] { -0.75m, 4m, -3.25m, 2.5m, -2m, 1.5m });
            }
        }
    }
}
