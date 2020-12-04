using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2015
{
    public static class Day06
    {
        public static int RunPart1()
        {
            var instructions = File.ReadAllLines(@"2015\Input\Day06.txt");
            var regex = new Regex(@"^(turn on|turn off|toggle) (\d{1,3}),(\d{1,3}) through (\d{1,3}),(\d{1,3})$");
            var grid = new bool[1000, 1000];

            foreach (var instruction in instructions)
            {
                var matches = regex.Match(instruction);
                var command = matches.Groups[1].Value;
                var startX = int.Parse(matches.Groups[2].Value);
                var startY = int.Parse(matches.Groups[3].Value);
                var endX = int.Parse(matches.Groups[4].Value);
                var endY = int.Parse(matches.Groups[5].Value);

                for (int y = startY; y <= endY; y++)
                {
                    for (int x = startX; x <= endX; x++)
                    {
                        switch(command)
                        {
                            case "turn on":
                                grid[x, y] = true;
                                break;
                            case "turn off":
                                grid[x, y] = false;
                                break;
                            case "toggle":
                                grid[x, y] = !grid[x, y];
                                break;
                        }
                    }
                }
            }
            
            return grid.Cast<bool>().Count(b => b);
        }

        public static int RunPart2()
        {
            var instructions = File.ReadAllLines(@"2015\Input\Day06.txt");
            var regex = new Regex(@"^(turn on|turn off|toggle) (\d{1,3}),(\d{1,3}) through (\d{1,3}),(\d{1,3})$");
            var grid = new int[1000, 1000];

            foreach (var instruction in instructions)
            {
                var matches = regex.Match(instruction);
                var command = matches.Groups[1].Value;
                var startX = int.Parse(matches.Groups[2].Value);
                var startY = int.Parse(matches.Groups[3].Value);
                var endX = int.Parse(matches.Groups[4].Value);
                var endY = int.Parse(matches.Groups[5].Value);

                for (int y = startY; y <= endY; y++)
                {
                    for (int x = startX; x <= endX; x++)
                    {
                        switch (command)
                        {
                            case "turn on":
                                grid[x, y] += 1;
                                break;
                            case "turn off":
                                if (grid[x, y] > 0) grid[x, y] -= 1;
                                break;
                            case "toggle":
                                grid[x, y] += 2;
                                break;
                        }
                    }
                }
            }

            return grid.Cast<int>().Sum();
        }

    }
}
