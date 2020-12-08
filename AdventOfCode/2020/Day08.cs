using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2020
{
    public static class Day08
    {
        public static long RunPart1()
        {
            var code = ReadCode();
            var hasRun = new BitArray(code.Count);
            var ptr = 0;
            var acc = 0L;

            while(!hasRun[ptr])
            {
                hasRun[ptr] = true;
                switch (code[ptr].Command)
                {
                    case "acc":
                        acc += code[ptr].Value;
                        ptr++;
                        break;
                    case "jmp":
                        ptr += code[ptr].Value;
                        break;
                    case "nop":
                        //Do nothing, just increase the pointer
                        ptr++;
                        break;
                }
            }

            return acc;
        }

        public static long RunPart2()
        {
            var code = ReadCode();
            BitArray hasRun = new BitArray(code.Count);
            for(var i = 0; i < code.Count; i++)
            {
                if (code[i].Command == "acc") continue;
                code[i] = (code[i].Command == "jmp" ? "nop" : "jmp", code[i].Value);

                var ptr = 0;
                long acc = 0;
                var found = false;
                hasRun.SetAll(false);
                while (ptr < code.Count && !hasRun[ptr])
                {
                    hasRun[ptr] = true;
                    switch (code[ptr].Command)
                    {
                        case "acc":
                            acc += code[ptr].Value;
                            ptr++;
                            break;
                        case "jmp":
                            ptr += code[ptr].Value;
                            break;
                        case "nop":
                            //Do nothing, just increase the pointer
                            ptr++;
                            break;
                    }

                    if (ptr >= code.Count) found = true;
                }

                if (found) return acc;
                code[i] = (code[i].Command == "jmp" ? "nop" : "jmp", code[i].Value);
            }

            return 0;
        }

        private static List<(string Command, int Value)> ReadCode()
        {
            var lines = File.ReadAllLines(@"2020\Input\Day08.txt");
            var code = new List<(string Command, int Value)>();
            foreach(var line in lines)
            {
                var lineValues = line.Split(" ");
                code.Add((lineValues[0], int.Parse(lineValues[1])));
            }

            return code;
        }

        private class Code
        {
            public string Command { get; private set; }
            public int Value { get; init; }

            private string _originalCommand;
            public bool HasRun { get; set; }

            public Code(string line)
            {
                var lineValues = line.Split(" ");
                Command = lineValues[0];
                Value = int.Parse(lineValues[1]);
            }

            public bool SwitchCommand()
            {
                if (Command == "acc") return false;
                
                _originalCommand = Command;
                if (Command == "nop") Command = "jmp";
                else if (Command == "jmp") Command = "nop";
                return true;
            }

            public void ResetCommand()
            {
                if (string.IsNullOrEmpty(_originalCommand)) return;
                Command = _originalCommand;
            }
        }
    }




}
