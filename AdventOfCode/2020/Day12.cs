using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2020
{
    public static class Day12
    {
        public static int RunPart1()
        {
            List<(char Dir, int Dist)> instructions = File.ReadAllLines(@"2020\Input\Day12.txt").Select(x => (x[0], int.Parse(x[1..]))).ToList();
            (int x, int y) = (0, 0);
            var dir = 1;

            foreach(var inst in instructions)
            {
                switch (inst.Dir)
                {
                    case 'N':
                    case 'F' when dir == 0:
                        y -= inst.Dist;
                        break;
                    case 'S':
                    case 'F' when dir == 2:
                        y += inst.Dist;
                        break;
                    case 'E':
                    case 'F' when dir == 1:
                        x += inst.Dist;
                        break;
                    case 'W':
                    case 'F' when dir == 3:
                        x -= inst.Dist;
                        break;
                    case 'L':
                        dir = (4 + dir - (inst.Dist / 90)) % 4;
                        break;
                    case 'R':
                        dir = (dir + (inst.Dist / 90)) % 4;
                        break;
                }
            }

            return Math.Abs(x) + Math.Abs(y);
        }

        public static int RunPart2()
        {
            List<(char Dir, int Dist)> instructions = File.ReadAllLines(@"2020\Input\Day12.txt").Select(x => (x[0], int.Parse(x[1..]))).ToList();
            (int N, int E) waypoint = (1, 10);
            (int N, int E) position = (0, 0);

            foreach(var inst in instructions)
            {
                switch (inst.Dir)
                {
                    case 'N':
                        waypoint.N += inst.Dist;
                        break;
                    case 'S':
                        waypoint.N -= inst.Dist;
                        break;
                    case 'E':
                        waypoint.E += inst.Dist;
                        break;
                    case 'W':
                        waypoint.E -= inst.Dist;
                        break;
                    case 'L' when inst.Dist == 90:
                    case 'R' when inst.Dist == 270:
                        waypoint = (waypoint.E, -waypoint.N);
                        break;
                    case 'L' when inst.Dist == 180:
                    case 'R' when inst.Dist == 180:
                        waypoint = (-waypoint.N, -waypoint.E);
                        break;
                    case 'L' when inst.Dist == 270:
                    case 'R' when inst.Dist == 90:
                        waypoint = (-waypoint.E, waypoint.N);
                        break;
                    case 'F':
                        position.N += (waypoint.N * inst.Dist);
                        position.E += (waypoint.E * inst.Dist);
                        break;
                }
            }

            return Math.Abs(position.N) + Math.Abs(position.E);
        }
    }
}
