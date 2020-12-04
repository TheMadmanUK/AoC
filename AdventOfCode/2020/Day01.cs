using System.IO;
using System.Linq;

namespace AdventOfCode._2020
{
    public static class Day01
    {
        public static int RunPart1()
        {
            var values = File.ReadAllLines(@"2020\Input\Day01.txt").Select(int.Parse).ToList();
            for (int i = 0; i < values.Count - 2; i++) {
                for (int j = i + 1; j < values.Count - 1; j++)
                {
                    if (values[i] + values[j] == 2020) return values[i] * values[j];
                }
            }
            return 0;
        }

        public static long RunPart2()
        {
            var values = File.ReadAllLines(@"2020\Input\Day01.txt").Select(int.Parse).ToList();
            for (int i = 0; i < values.Count - 3; i++)
            {
                for (int j = i + 1; j < values.Count - 2; j++)
                {
                    if (values[i] + values[j] >= 2020) continue;

                    for (int k = j + 1; k < values.Count - 1; k++)
                        if (values[i] + values[j] + values[k] == 2020) return values[i] * values[j] * values[k];
                }
            }
            return 0;
        }
    }
}
