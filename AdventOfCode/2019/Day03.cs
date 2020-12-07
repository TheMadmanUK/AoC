using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2019
{
    public static class Day03
    {
        public static int RunPart1()
        {
            var lines = File.ReadAllLines(@"2019\Input\Day03.txt");
            var grid1 = BuildList(lines[0]);
            var grid2 = BuildList(lines[1]);

            return grid1.Keys.Where(x => x != (0, 0)).Intersect(grid2.Keys).Min(x => Math.Abs(x.X) + Math.Abs(x.Y));
        }

        public static int RunPart2()
        {
            var lines = File.ReadAllLines(@"2019\Input\Day03.txt");
            var grid1 = BuildList(lines[0]);
            var grid2 = BuildList(lines[1]);

            var intersections = grid1.Keys.Where(x => x != (0, 0)).Intersect(grid2.Keys);

            return intersections.Min(x => grid1[x] + grid2[x]);
        }

        private static Dictionary<(int X, int Y), int> BuildList(string lineList)
        {
            var grid = new Dictionary<(int X, int Y), int> { { (0, 0), 0 } };
            (int X, int Y) point = (0, 0);
            var count = 0;
            foreach (var line in lineList.Split(","))
            {
                var dir = line[0];
                var dist = int.Parse(line[1..]);

                switch (dir)
                {
                    case 'L':
                        for (int i = 1; i <= dist; i++) 
                            if (!grid.ContainsKey((point.X - i, point.Y))) grid.Add((point.X - i, point.Y), count + i);
                        point = (point.X - dist, point.Y);
                        break;
                    case 'R':
                        for (int i = 1; i <= dist; i++)
                            if (!grid.ContainsKey((point.X + i, point.Y))) grid.Add((point.X + i, point.Y), count + i);
                        point = (point.X + dist, point.Y);
                        break;
                    case 'U':
                        for (int i = 1; i <= dist; i++)
                            if (!grid.ContainsKey((point.X, point.Y - i))) grid.Add((point.X, point.Y - i), count + i);
                        point = (point.X, point.Y - dist);
                        break;
                    case 'D':
                        for (int i = 1; i <= dist; i++)
                            if (!grid.ContainsKey((point.X, point.Y + i))) grid.Add((point.X, point.Y + i), count + i);
                        point = (point.X, point.Y + dist);
                        break;
                }

                count += dist;
            }

            return grid;
        }
    }


}
