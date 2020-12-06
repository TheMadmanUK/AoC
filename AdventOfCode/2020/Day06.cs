using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2020
{
    public static class Day06
    {
        public static int RunPart1()
        {
            var answers = File.ReadAllLines(@"2020\Input\Day06.txt");
            var total = 0;
            var answerList = new List<char>();

            foreach (var answer in answers)
            {
                if (string.IsNullOrEmpty(answer))
                {
                    total += answerList.Count;
                    answerList = new List<char>();
                    continue;
                }

                answerList.AddRange(answer.Where(x => !answerList.Contains(x)));
            }

            total += answerList.Count;
            return total;
        }

        public static int RunPart2()
        {
            var answers = File.ReadAllLines(@"2020\Input\Day06.txt");
            var total = 0;
            var answerList = new List<char>("abcdefghijklmnopqrstuvwxyz");

            foreach (var answer in answers)
            {
                if (string.IsNullOrEmpty(answer))
                {
                    total += answerList.Count;
                    answerList = new List<char>("abcdefghijklmnopqrstuvwxyz");
                    continue;
                }

                if (answerList.Count == 0) continue;

                answerList = answer.Where(x => answerList.Contains(x)).ToList();
            }

            total += answerList.Count;
            return total;
        }
    }
}
