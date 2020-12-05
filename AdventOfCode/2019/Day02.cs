using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2019
{
    public static class Day02
    {
        public static long RunPart1()
        {
            var code = new IntCode(@"2019\Input\Day02.txt");
            code.SetCodeValue(1, 12);
            code.SetCodeValue(2, 2);
            code.RunCode();
            return code.Code[0];
        }

        public static long RunPart2()
        {
            var code = new IntCode(@"2019\Input\Day02.txt");
            for (int noun = 99; noun >= 0; noun--)
            {
                for (int verb = 99; verb >= 0; verb--)
                {
                    code.SetCodeValue(1, noun);
                    code.SetCodeValue(2, verb);
                    code.RunCode();
                    if (code.Code[0] == 19690720) return noun * 100 + verb;
                }
            }

            return 0;
        }
    }
}
