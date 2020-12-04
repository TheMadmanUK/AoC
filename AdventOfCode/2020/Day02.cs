using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2020
{
    public static class Day02
    {
        public static int RunPart1()
        {
            var lines = File.ReadAllLines(@"2020\Input\Day02.txt");
            var regex = new Regex(@"^(?<From>\d*)-(?<To>\d*) (?<Letter>\w): (?<Password>.*)$");
            var valid = 0;


            foreach(var line in lines)
            {
                var details = regex.Match(line);
                var from = int.Parse(details.Groups["From"].Value);
                var to = int.Parse(details.Groups["To"].Value);
                var letter = details.Groups["Letter"].Value[0];
                var password = details.Groups["Password"].Value;

                var letterCount = password.Count(x => x == letter);
                if (letterCount >= from && letterCount <= to) valid++;
            }

            return valid;
        }

        public static int RunPart2()
        {
            var lines = File.ReadAllLines(@"2020\Input\Day02.txt");
            var regex = new Regex(@"^(?<From>\d*)-(?<To>\d*) (?<Letter>\w): (?<Password>.*)$");
            var valid = 0;


            foreach (var line in lines)
            {
                var details = regex.Match(line);
                var from = int.Parse(details.Groups["From"].Value);
                var to = int.Parse(details.Groups["To"].Value);
                var letter = details.Groups["Letter"].Value[0];
                var password = details.Groups["Password"].Value;

                if ((password[from - 1] == letter && password[to - 1] != letter) || (password[from - 1] != letter && password[to - 1] == letter)) valid++;
            }

            return valid;
        }
    }
}
