using System.Linq;

namespace AdventOfCode._2019
{
    public static class Day04
    {
        private static readonly int MinValue = 109165;
        private static readonly int MaxValue = 576723;

        public static int RunPart1()
        {
            return Enumerable.Range(MinValue, MaxValue - MinValue + 1).Select(x => x.ToString())
                .Where(x => Enumerable.Range(0, 5).Any(n => x[n] == x[n + 1]))
                .Count(x => Enumerable.Range(0, 5).All(n => x[n] <= x[n + 1]));
        }

        public static int RunPart2()
        {
            return Enumerable.Range(MinValue, MaxValue - MinValue + 1).Select(x => x.ToString())
                .Where(x => Enumerable.Range(0, 5).Any(n => x[n] == x[n + 1] && (n == 0 || x[n] != x[n - 1]) && (n == 4 || x[n] != x[n + 2])))
                .Count(x => Enumerable.Range(0, 5).All(n => x[n] <= x[n + 1]));
        }
    }
}
