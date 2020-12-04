using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015
{
    public static class Day03
    {
        public static int RunPart1()
        {
            (int X, int Y) coords = (0, 0);
            var houses = new Dictionary<(int, int), int> { { coords, 1 } };
            var route = File.ReadAllText(@"2015\input\Day03.txt");

            foreach(var arrow in route)
            {
                switch(arrow)
                {
                    case '<': coords.X--; break;
                    case '>': coords.X++; break;
                    case '^': coords.Y--; break;
                    case 'v': coords.Y++; break;
                }

                if (houses.ContainsKey(coords))
                    houses[coords]++;
                else
                    houses.Add(coords, 1);
            }

            return houses.Keys.Count;
        }

        public static int RunPart2()
        {
            (int X, int Y)[] coords = new [] { (0, 0), (0, 0) };
            var houses = new Dictionary<(int, int), int> { { coords[0], 2 } };
            var route = File.ReadAllText(@"2015\input\Day03.txt");

            for (int i = 0; i < route.Length; i++)
            {
                switch (route[i])
                {
                    case '<': coords[i % 2].X--; break;
                    case '>': coords[i % 2].X++; break;
                    case '^': coords[i % 2].Y--; break;
                    case 'v': coords[i % 2].Y++; break;
                }

                if (houses.ContainsKey(coords[i % 2]))
                    houses[coords[i % 2]]++;
                else
                    houses.Add(coords[i % 2], 1);
            }

            return houses.Keys.Count;
        }
    }
}
