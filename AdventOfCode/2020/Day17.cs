using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2020
{
    public static class Day17
    {
        public static int RunPart1()
        {
            var lines = File.ReadAllLines(@"2020\Input\Day17.txt");
            var grid = new List<(int X, int Y, int Z)>();
            for (int y = 0; y < lines.Length; y++)
                for (int x = 0; x < lines[y].Length; x++)
                    if (lines[y][x] == '#') grid.Add((x, -y, 0));

            for (int i = 0; i < 6; i++)
            {
                var gridCount = new Dictionary<(int X, int Y, int Z), int>();

                foreach(var cell in grid)
                {
                    for (int z = cell.Z - 1; z <= cell.Z + 1; z++)
                        for (int y = cell.Y - 1; y <= cell.Y + 1; y++)
                            for (int x = cell.X - 1; x <= cell.X + 1; x++)
                            {
                                if (cell == (x, y, z)) continue;
                                if (!gridCount.TryAdd((x, y, z), 1)) gridCount[(x, y, z)]++;
                            }
                }

                var newGrid = new List<(int X, int Y, int Z)>();

                foreach(var (cell, value) in gridCount)
                    if (value == 3 || (value == 2 && grid.Contains(cell))) newGrid.Add(cell);

                grid = newGrid;
            }

            return grid.Count;
        }

        public static int RunPart2()
        {
            var lines = File.ReadAllLines(@"2020\Input\Day17.txt");
            var grid = new List<(int X, int Y, int Z, int W)>();
            for (int y = 0; y < lines.Length; y++)
                for (int x = 0; x < lines[y].Length; x++)
                    if (lines[y][x] == '#') grid.Add((x, -y, 0, 0));

            for (int i = 0; i < 6; i++)
            {
                var gridCount = new Dictionary<(int X, int Y, int Z, int W), int>();

                foreach (var cell in grid)
                {
                    for (int z = cell.Z - 1; z <= cell.Z + 1; z++)
                        for (int y = cell.Y - 1; y <= cell.Y + 1; y++)
                            for (int x = cell.X - 1; x <= cell.X + 1; x++)
                                for (int w = cell.W - 1; w <= cell.W + 1; w++)
                                {
                                    if (cell == (x, y, z, w)) continue;
                                    if (!gridCount.TryAdd((x, y, z, w), 1)) gridCount[(x, y, z, w)]++;
                                }
                }

                var newGrid = new List<(int X, int Y, int Z, int W)>();

                foreach (var (cell, value) in gridCount)
                    if (value == 3 || (value == 2 && grid.Contains(cell))) newGrid.Add(cell);

                grid = newGrid;
            }

            return grid.Count;
        }
    }
}
