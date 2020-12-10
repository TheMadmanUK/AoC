using System.Linq;

namespace AdventOfCode._2019
{
    public static class Day05
    {
        public static long RunPart1()
        {
            var code = new IntCode(@"2019\Input\Day05.txt");
            return code.RunCode(1).ToList()[^1];
        }

        public static long RunPart2()
        {
            var code = new IntCode(@"2019\Input\Day05.txt");
            return code.RunCode(5).ToList()[^1];
        }
    }
}
