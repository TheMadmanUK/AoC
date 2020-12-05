using System;
using System.IO;
using System.Linq;

namespace AdventOfCode._2020
{
    public static class Day05
    {
        public static int RunPart1()
        {
            var seats = File.ReadAllLines(@"2020\Input\Day05.txt");
            return seats.Max(x => Convert.ToInt32(x.Replace('F', '0').Replace('B', '1').Replace('L', '0').Replace('R', '1'), 2));
        }

        public static int RunPart2()
        {
            var seats = File.ReadAllLines(@"2020\Input\Day05.txt");
            var seatCodes =  seats.Select(x => Convert.ToInt32(x.Replace('F', '0').Replace('B', '1').Replace('L', '0').Replace('R', '1'), 2))
                .OrderBy(x => x).ToList();

            return Enumerable.Range(0, 965).Where(x => !seatCodes.Contains(x) && seatCodes.Contains(x - 1) && seatCodes.Contains(x + 1)).FirstOrDefault();
        }
    }
}
