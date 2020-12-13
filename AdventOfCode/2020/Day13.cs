using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2020
{
    public static class Day13
    {
        public static int RunPart1()
        {
            var lines = File.ReadAllLines(@"2020\Input\Day13.txt");
            var thisTime = int.Parse(lines[0]);
            var busIds = lines[1].Split(',').Where(x => int.TryParse(x, out _)).Select(int.Parse);

            var nextBus = 0;
            var nextBusMins = 0;
            foreach (var busId in busIds)
            {
                var nextBusTime = 0;
                while (nextBusTime < thisTime)
                    nextBusTime += busId;

                if (nextBusMins == 0 || nextBusTime - thisTime < nextBusMins)
                {
                    nextBus = busId;
                    nextBusMins = nextBusTime - thisTime;
                }
            }

            return nextBus * nextBusMins;
        }

        public static long RunPart2()
        {
            var lines = File.ReadAllLines(@"2020\Input\Day13.txt");
            var busIdList = lines[1].Split(',');
            var busIds = busIdList.Where(x => int.TryParse(x, out _)).ToDictionary(x => Array.FindIndex(busIdList, y => x == y), int.Parse);

            var nextBus = 0L;
            var found = new List<long> { 0 };
            long busAdd = busIds[0];
            while(true)
            {
                nextBus += busAdd;
                if (nextBus < 0) return 0;
                foreach(var (k, v) in busIds)
                {
                    if (found.Contains(k)) continue;
                    if ((nextBus + k) % v == 0)
                    {
                        found.Add(k);
                        busAdd *= v;
                    }
                }

                if (found.Count == busIds.Count) return nextBus;
            }
        }
    }
}
