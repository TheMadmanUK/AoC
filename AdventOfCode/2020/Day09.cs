using System;
using System.IO;
using System.Linq;

namespace AdventOfCode._2020
{
    public static class Day09
    {
        public static long RunPart1()
        {
            var numbers = File.ReadAllLines(@"2020\Input\Day09.txt").Select(long.Parse).ToList();
            for(int i = 25; i < numbers.Count; i++)
            {
                var found = false;
                for (int j = i - 25; !found && j < i; j++)
                {
                    if (Enumerable.Range(i - 25, 25).Any(x => x != j && numbers[x] == numbers[i] - numbers[j])) 
                    {
                        found = true;
                        continue;
                    }
                }

                if (!found) return numbers[i];
            }

            return 0;
        }

        public static long RunPart2()
        {
            var numbers = File.ReadAllLines(@"2020\Input\Day09.txt").Select(long.Parse).ToList();
            var check = RunPart1();

            for (int i = 0; i < numbers.Count; i++)
            {
                var sum = numbers[i];
                var min = sum;
                var max = sum;
                for (int j = i + 1; j < numbers.Count && sum < check; j++)
                {
                    sum += numbers[j];
                    if (numbers[j] < min) min = numbers[j];
                    if (numbers[j] > max) max = numbers[j];
                    if (sum == check)
                        return min + max;
                }
            }

            return 0;
        }
    }
}
