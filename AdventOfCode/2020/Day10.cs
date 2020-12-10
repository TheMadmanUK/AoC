using System.IO;
using System.Linq;

namespace AdventOfCode._2020
{
    public static class Day10
    {
        public static int RunPart1()
        {
            var adapters = File.ReadAllLines(@"2020\Input\Day10.txt").Select(int.Parse).OrderBy(x => x);
            var lastAdapter = 0;
            var diff1 = 0;
            var diff3 = 0;

            foreach(var adapter in adapters)
            {
                switch (adapter - lastAdapter)
                {
                    case 1: diff1++; break;
                    case 2: break;
                    case 3: diff3++; break;
                    default: return 0;
                }

                lastAdapter = adapter;
            }

            diff3++;
            return diff1 * diff3;
        }

        public static long RunPart2()
        {
            var adapters = File.ReadAllLines(@"2020\Input\Day10.txt").ToDictionary(int.Parse, _ => 0L);
            adapters.Add(0, 1);
            var max = adapters.Keys.Max() + 3;
            adapters.Add(max, 0);

            foreach (var adapter in adapters.Keys.OrderBy(x => x))
            {
                if (adapters[adapter] > 0) continue;
                adapters[adapter] = adapters.Keys.Where(x => adapter - x >= 1 && adapter - x <= 3).Sum(x => adapters[x]);
            }

            return adapters[max];
        }
    }
}
