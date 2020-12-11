using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2019
{
    internal static class Day07
    {
        public static long RunPart1()
        {
            var code = new IntCode(@"2019\Input\Day07.txt");
            var permutations = GetPermutations(new List<long> { 0, 1, 2, 3, 4 }, 5);

            return permutations.Select(permutation =>
                    permutation.Aggregate(0L, (current, setting) => code.RunCode(new List<long> { setting, current }).First()))
                .Max();
        }

        public static long RunPart2()
        {
            var code = new IntCode[5];
            for (var i = 0; i < 5; i++)
                code[i] = new IntCode(@"2019\Input\Day07.txt");
            var permutations = GetPermutations(new List<long> { 5, 6, 7, 8, 9 }, 5);
            var maxOutput = 0L;

            foreach (var permutation in permutations)
            {
                for (var i = 0; i < 5; i++) code[i].Reset();
                var output = 0L;
                var lastOutput = 0L;

                while (true)
                {
                    var stopped = false;
                    for (var i = 0; i < 5; i++)
                    {
                        var thisOutput = code[i].RunCode(new List<long> { permutation[i], output});
                        if (thisOutput.Any())
                        {
                            output = thisOutput.First();
                            continue;
                        }
                        //if (code[i].RunCode(new List<long> { output }, out output)) continue;

                        stopped = true;
                        break;
                    }

                    if (stopped) break;
                    lastOutput = output;
                }

                if (maxOutput < lastOutput) maxOutput = lastOutput;
            }

            return maxOutput;
        }

        private static List<List<T>> GetPermutations<T>(IReadOnlyCollection<T> list, int length)
        {
            if (length == 1) return list.Select(t => new List<T> { t }).ToList();

            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new[] { t2 }).ToList()).ToList();
        }
    }
}
