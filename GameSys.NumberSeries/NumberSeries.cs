using System;
using System.Collections.Generic;
using System.Linq;

namespace GameSys.NumberSeries
{
    public class NumberSeries
    {
        private const decimal ConstantY = 1000m;

        public IEnumerable<decimal> Result { get; }

        public NumberSeries(decimal x, decimal y, int length)
        {
            Result = GetSeries(GetFirstNumber(x), GetGrowthRate(y), length);
        }

        public decimal ThirdLargestNumber
            => new List<decimal>(Result).OrderByDescending(d => d).ElementAt(2);

        public decimal ClosestNumber(decimal z)
        {
            var approximateNumber = ConstantY/z;
            var previous = approximateNumber;
            foreach (var n in new List<decimal>(Result).OrderBy(d => d))
            {
                if (approximateNumber < n)
                    if(previous < approximateNumber)
                        return (approximateNumber - previous) < (n - approximateNumber) ? previous : n;
                    else
                        return n;
                previous = n;
            }
            return previous;
        }

        #region Internal
        internal static decimal Round(decimal number)
            => Math.Round(number * 4, MidpointRounding.AwayFromZero) / 4;

        internal static decimal GetFirstNumber(decimal x)
            => (0.5m * x * x + 30 * x + 10) / 25;

        internal static decimal GetGrowthRate(decimal y)
            => y * 0.0008m;

        internal static decimal Next(decimal first, decimal growthRate, int index)
        {
            if (index <= 0)
                throw new ArgumentOutOfRangeException(nameof(index), "The index must be greater than 0.");

            var result = growthRate;
            for (var i = 1; i < index; i++)
            {
                result *= first;
            }

            return result;
        }

        internal static IEnumerable<decimal> GetSeries(decimal first, decimal growthRate, int length)
        {
            if (length < 1)
                throw new ArgumentOutOfRangeException(nameof(length), "The length must be greater than 0.");

            yield return Round(first);
            for (var i = 1; i < length; i++)
                yield return Round(Next(first, growthRate, i));
        }
        #endregion
    }
}
