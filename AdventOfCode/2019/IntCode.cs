using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2019
{
    public class IntCode
    {
        public IntcodeCode Code { get; private set; }
        private readonly IntcodeCode _baseCode;
        private IEnumerator<long> _isRunning;

        public IntCode(string codeFile) : this(File.ReadAllText(codeFile).Split(",").Select(long.Parse).ToList()) { }

        public IntCode(IReadOnlyList<long> baseCode)
        {
            _baseCode = new IntcodeCode();
            for (var i = 0; i < baseCode.Count; i++)
            {
                _baseCode.Add(i, baseCode[i]);
            }

            Code = _baseCode.Copy();
        }

        public void SetCodeValue(int index, long value)
        {
            _baseCode[index] = value;
            Code = _baseCode.Copy();
        }

        public void Reset()
        {
            Code = _baseCode.Copy();
            _isRunning?.Dispose();
            _isRunning = null;
        }

        public void RunCode()
        {
            var sp = 0;

            while (true)
            {
                switch (Code[sp])
                {
                    case 1:
                        var val1 = Code.GetValue(Code[sp + 1]);
                        var val2 = Code.GetValue(Code[sp + 2]);
                        Code.WriteValue(Code[sp + 3], val1 + val2);
                        sp += 4;
                        break;

                    case 2:
                        val1 = Code.GetValue(Code[sp + 1]);
                        val2 = Code.GetValue(Code[sp + 2]);
                        Code.WriteValue(Code[sp + 3], val1 * val2);
                        sp += 4;
                        break;

                    case 99:
                        return;

                    default:
                        throw new Exception($"Invalid code in position {sp}: {Code[sp]}");
                }
            }
        }
    }

    public class IntcodeCode : Dictionary<long, long>
    {
        public IntcodeCode() { }
        public IntcodeCode(IDictionary<long, long> dictionary) : base(dictionary) { }

        public IntcodeCode Copy()
        {
            return new IntcodeCode(this.ToDictionary(kvp => kvp.Key, kvp => kvp.Value));
        }

        public long GetValue(long pos)
        {
            return ContainsKey(pos) ? this[pos] : 0;
        }

        public void WriteValue(long pos, long value)
        {
            if (ContainsKey(pos))
                this[pos] = value;
            else
                Add(pos, value);
        }
    }
}
