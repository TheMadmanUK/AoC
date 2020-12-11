using System;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode._2020
{
    public static class Day11
    {
        public static int RunPart1()
        {
            var grid = File.ReadAllLines(@"2020\Input\Day11.txt");

            var (height, width) = (grid.Length, grid[0].Length);

            while (true)
            {
                var occupiedCount = new int[height, width];
                for (int y = 0; y < height; y++)
                    for (int x = 0; x < width; x++)
                        for (int y2 = y - 1; y2 <= y + 1; y2++)
                            for (int x2 = x - 1; x2 < x + 2; x2++)
                                if (x2 >= 0 && x2 < width && y2 >= 0 && y2 < height && !(x == x2 && y == y2) && grid[y2][x2] == '#') occupiedCount[y, x]++;

                var change = false;
                for (int y = 0; y < height; y++)
                {
                    
                    var newString = new StringBuilder();
                    for (int x = 0; x < width; x++)
                    {
                        switch (grid[y][x])
                        {
                            case '#' when occupiedCount[y, x] >= 4:
                                newString.Append('L');
                                change = true;
                                break;

                            case 'L' when occupiedCount[y, x] == 0:
                                newString.Append('#');
                                change = true;
                                break;

                            default:
                                newString.Append(grid[y][x]);
                                break;
                        }
                    }
                    grid[y] = newString.ToString();
                }

                if (!change) 
                    return string.Join("", grid).Count(x => x == '#');
            }
        }

        public static int RunPart2()
        {
            var grid = File.ReadAllLines(@"2020\Input\Day11.txt");

            var (height, width) = (grid.Length, grid[0].Length);

            while (true)
            {
                var occupiedCount = new int[height, width];
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        for (int y1 = y - 1; y1 >= 0; y1--)
                        {
                            if (grid[y1][x] == '#') occupiedCount[y, x]++;
                            if (grid[y1][x] != '.') break;
                        }
                        for (int y1 = y - 1, x1 = x + 1; y1 >= 0 && x1 < width; y1--, x1++)
                        {
                            if (grid[y1][x1] == '#') occupiedCount[y, x]++;
                            if (grid[y1][x1] != '.') break;
                        }
                        for (int x1 = x + 1; x1 < width; x1++)
                        {
                            if (grid[y][x1] == '#') occupiedCount[y, x]++;
                            if (grid[y][x1] != '.') break;
                        }
                        for (int y1 = y + 1, x1 = x + 1; y1 < height && x1 < width; y1++, x1++)
                        {
                            if (grid[y1][x1] == '#') occupiedCount[y, x]++;
                            if (grid[y1][x1] != '.') break;
                        }
                        for (int y1 = y + 1; y1 < height; y1++)
                        {
                            if (grid[y1][x] == '#') occupiedCount[y, x]++;
                            if (grid[y1][x] != '.') break;
                        }
                        for (int y1 = y + 1, x1 = x - 1; y1 < height && x1 >= 0; y1++, x1--)
                        {
                            if (grid[y1][x1] == '#') occupiedCount[y, x]++;
                            if (grid[y1][x1] != '.') break;
                        }
                        for (int x1 = x - 1; x1 >= 0; x1--)
                        {
                            if (grid[y][x1] == '#') occupiedCount[y, x]++;
                            if (grid[y][x1] != '.') break;
                        }
                        for (int y1 = y - 1, x1 = x - 1; y1 >= 0 && x1 >= 0; y1--, x1--)
                        {
                            if (grid[y1][x1] == '#') occupiedCount[y, x]++;
                            if (grid[y1][x1] != '.') break;
                        }
                    }
                }

                var change = false;
                for (int y = 0; y < height; y++)
                {

                    var newString = new StringBuilder();
                    for (int x = 0; x < width; x++)
                    {
                        switch (grid[y][x])
                        {
                            case '#' when occupiedCount[y, x] >= 5:
                                newString.Append('L');
                                change = true;
                                break;

                            case 'L' when occupiedCount[y, x] == 0:
                                newString.Append('#');
                                change = true;
                                break;

                            default:
                                newString.Append(grid[y][x]);
                                break;
                        }
                    }
                    grid[y] = newString.ToString();
                }

                if (!change)
                    return string.Join("", grid).Count(x => x == '#');
            }
        }
    }
}
