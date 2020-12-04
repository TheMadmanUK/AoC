using System.IO;

namespace AdventOfCode._2020
{
    public static class Day03
    {
        public static long RunPart1()
        {
            var grid = File.ReadAllLines(@"2020\Input\Day03.txt");
            return CountTrees(grid, 3, 1);
        }

        public static long RunPart2()
        {
            var grid = File.ReadAllLines(@"2020\Input\Day03.txt");
            return CountTrees(grid, 1, 1) * CountTrees(grid, 3, 1) * CountTrees(grid, 5, 1) * CountTrees(grid, 7, 1) * CountTrees(grid, 1, 2);
        }

        private static long CountTrees(string[] grid, int dX, int dY)
        {
            var height = grid.Length;
            var width = grid[0].Length;
            (int x, int y) = (0, 0);
            var trees = 0;

            while (true)
            {
                y += dY;
                if (y >= height) break;
                x = (x + dX) % width;

                if (grid[y][x] == '#') trees++;
            }

            return trees;
        }
    }
}
