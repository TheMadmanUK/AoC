using System.IO;
using System.Linq;

namespace AdventOfCode._2019
{
    public static class Day01
    {
        public static long RunPart1()
        {
            return File.ReadAllLines(@"2019\Input\Day01.txt").Select(long.Parse).Sum(x => x / 3 - 2);
        }

        public static long RunPart2()
        {
            var fuelValues = File.ReadAllLines(@"2019\Input\Day01.txt").Select(long.Parse);
            var totalFuel = 0L;

            foreach(var fuel in fuelValues)
            {
                var thisFuel = fuel;
                while(true)
                {
                    thisFuel = thisFuel / 3 - 2;
                    if (thisFuel <= 0) break;
                    totalFuel += thisFuel;
                }
            }

            return totalFuel;
        }
    }
}
