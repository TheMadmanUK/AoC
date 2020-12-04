using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015
{
    public static class Day05
    {
        public static int RunPart1()
        {
            var words = File.ReadAllLines(@"2015\input\Day05.txt");
            var nice = 0;

            foreach (var word in words)
            {
                if (word.Where(x => "aeiou".Contains(x)).Count() < 3) continue;
                if (Enumerable.Range(0, word.Length - 1).All(x => word[x] != word[x + 1])) continue;
                if (new[] { "ab", "cd", "pq", "xy" }.Any(x => word.Contains(x))) continue;
                nice++;
            }

            return nice;
        }

        public static int RunPart2()
        {
            var words = File.ReadAllLines(@"2015\input\Day05.txt");
            var nice = 0;

            foreach (var word in words)
            {
                var found = false;
                for(int i = 0; i < word.Length - 3; i++)
                {
                    if (word[(i+2)..].Contains(word.Substring(i, 2))) {
                        //Console.WriteLine($"{word.Substring(i, 2)} found twice in {word}!");
                        found = true;
                        break;
                    }
                }
                if (!found) continue;

                found = false;
                for(int i = 0; i < word.Length - 2; i++)
                {
                    if (word[i] == word[i+2])
                    {
                        //Console.WriteLine($"{word[i]} found in i+2 in {word}!");
                        found = true;
                        break;
                    }
                }
                if (found) nice++;
            }

            return nice;
        }

    }
}
