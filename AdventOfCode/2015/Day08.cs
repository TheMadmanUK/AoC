using System;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode._2015
{
    public static class Day08
    {
        public static int RunPart1()
        {
            var lines = File.ReadAllLines(@"2015\Input\Day08.txt");
            var originalLength = 0;
            var formattedLength = 0;

            foreach (var line in lines)
            {
                originalLength += line.Length;
                var formattedLine = line[1..^1];
                formattedLine = Regex.Unescape(formattedLine);

                formattedLength += formattedLine.Length;
            }

            return originalLength - formattedLength;
        }

        public static int RunPart2()
        {
            var lines = File.ReadAllLines(@"2015\Input\Day08.txt");
            var originalLength = 0;
            var formattedLength = 0;

            foreach (var line in lines)
            {
                originalLength += line.Length;
                var formattedLine = Regex.Escape(line);
                formattedLine = formattedLine.Replace("\"", "\\\"");
                formattedLength += formattedLine.Length + 2;
            }

            return formattedLength - originalLength;
        }
    }
}
