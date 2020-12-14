using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2020
{
    public static class Day14
    {
        public static long RunPart1()
        {
            var lines = File.ReadAllLines(@"2020\Input\Day14.txt");
            string bitmask = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
            long bitsOn = 0L;
            long bitsOff = 0L;
            var memory = new Dictionary<int, long>();
            var regex = new Regex(@"^mem\[(\d+)\] = (\d+)$");

            foreach(var line in lines)
            {
                if (line.StartsWith("mask"))
                {
                    bitmask = line.Remove(0, 7).Trim();
                    bitsOn = Convert.ToInt64(bitmask.Replace('X', '0'), 2);
                    bitsOff = Convert.ToInt64(bitmask.Replace('1', 'X').Replace('0', '1').Replace('X', '0'), 2);
                    continue;
                }

                var matches = regex.Match(line);
                var pos = int.Parse(matches.Groups[1].ToString());
                var value = long.Parse(matches.Groups[2].ToString());

                value = (value | bitsOn) & ~bitsOff;
                if (!memory.TryAdd(pos, value))
                    memory[pos] = value;
            }

            return memory.Values.Sum();
        }

        public static long RunPart2()
        {
            var lines = File.ReadAllLines(@"2020\Input\Day14.txt");
            string bitmask = "000000000000000000000000000000000000";
            long bitsOn = 0L;
            var memory = new Dictionary<long, long>();
            var regex = new Regex(@"^mem\[(\d+)\] = (\d+)$");

            foreach (var line in lines)
            {
                if (line.StartsWith("mask"))
                {
                    bitmask = line.Remove(0, 7).Trim();
                    bitsOn = Convert.ToInt64(bitmask.Replace('X', '0'), 2);
                    continue;
                }

                var matches = regex.Match(line);
                var positions = new List<long> { long.Parse(matches.Groups[1].ToString()) | bitsOn };
                var value = long.Parse(matches.Groups[2].ToString());

                for (int i = 0; i < bitmask.Length; i++)
                {
                    if (bitmask[i] == 'X')
                    {
                        var newpos = new List<long>();
                        foreach(var pos in positions)
                        {
                            if ((pos & 1L << (35 - i)) != 0)
                                newpos.Add(pos & ~(1L << (35 - i)));
                            else
                                newpos.Add(pos | (1L << (35 - i)));
                        }
                        positions.AddRange(newpos);
                    }
                }

                foreach (var pos in positions)
                    if (!memory.TryAdd(pos, value))
                        memory[pos] = value;
            }

            return memory.Values.Sum();
        }
    }
}
