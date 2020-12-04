using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode._2015
{
    public static class Day02
    {
        public static int RunPart1()
        {
            var boxes = File.ReadAllLines(@"2015\input\Day02.txt")
                .Select(x => x.Split('x'))
                .Select(x => new Box { H = int.Parse(x[0]), L = int.Parse(x[1]), W = int.Parse(x[2]) });
            return boxes.Sum(x => x.Paper);
        }

        public static int RunPart2()
        {
            var boxes = File.ReadAllLines(@"2015\input\Day02.txt")
                .Select(x => x.Split('x'))
                .Select(x => new Box { H = int.Parse(x[0]), L = int.Parse(x[1]), W = int.Parse(x[2]) });
            return boxes.Sum(x => x.Ribbon);
        }

        private class Box
        {
            public int L { get; init; }
            public int W { get; init; }
            public int H { get; init; }
            private int LW => L * W;
            private int LH => L * H;
            private int WH => W * H;

            public int Paper => (2 * LW) + (2 * LH) + (2 * WH) + new int[] { LW, LH, WH }.Min();
            public int Ribbon {
                get
                {
                    var size = (L < H && W < H) ? L + L + W + W : H + H + (L < W ? L + L : W + W);
                    return size + (L * W * H);
                }
            }
        }
    }
}
