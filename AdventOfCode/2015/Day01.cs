using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode._2015
{
    public static class Day01
    {
        public static int RunPart1()
        {
            var input = File.ReadAllText(@"2015\input\Day01.txt");
            return input.Count(x => x == '(') - input.Count(x => x == ')');
        }

        public static int RunPart2()
        {
            var input = File.ReadAllText(@"2015\input\Day01.txt");
            var count = 0;
            for(var i = 1; i <= input.Length; i++)
            {
                count += input[i-1] == '(' ? 1 : -1;
                if (count < 0) return i;
            }

            return 0;
        }
    }
}
