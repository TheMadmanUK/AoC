using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015
{
    public static class Day04
    {
        private static readonly string Key = "yzbqklnj";
        public static int RunPart1()
        {
            MD5 md5 = MD5.Create();

            for (int i = 1; ; i++)
            {
                var hash = BitConverter.ToString(md5.ComputeHash(Encoding.ASCII.GetBytes($"{Key}{i}"))).Replace("-", "");
                if (hash[0..5] == "00000") return i;
            }
        }

        public static int RunPart2()
        {
            MD5 md5 = MD5.Create();

            for (int i = 1; ; i++)
            {
                var hash = BitConverter.ToString(md5.ComputeHash(Encoding.ASCII.GetBytes($"{Key}{i}"))).Replace("-", "");
                if (hash[0..6] == "000000") return i;
            }
        }
    }
}
