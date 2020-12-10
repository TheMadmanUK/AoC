using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2019
{
    public class IntCode
    {
        public Memory Code { get; private set; }
        private readonly Memory _baseCode;

        public IntCode(string codeFile) : this(File.ReadAllText(codeFile).Split(",").Select(long.Parse).ToList()) { }

        public IntCode(IReadOnlyList<long> baseCode)
        {
            _baseCode = new Memory();
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
        }

        public IEnumerable<long> RunCode()
        {
            return RunCode(new List<long>());
        }

        public IEnumerable<long> RunCode(int input)
        {
            return RunCode(new List<long> { input });
        }

        public IEnumerable<long> RunCode(List<long> input)
        {
            var ptr = 0;
            var inptr = 0;

            while (true)
            {
                var instruction = (int)Code[ptr] % 100;
                var mode1 = (int)Code[ptr] / 100 % 10;
                var mode2 = (int)Code[ptr] / 1000 % 10;
                var mode3 = (int)Code[ptr] / 10000;

                switch (instruction)
                {
                    case 1: // ADD
                        var val1 = Code.GetValue(Code[ptr + 1], mode1);
                        var val2 = Code.GetValue(Code[ptr + 2], mode2);
                        Code.WriteValue(Code[ptr + 3], val1 + val2);
                        ptr += 4;
                        break;

                    case 2: // MULT
                        val1 = Code.GetValue(Code[ptr + 1], mode1);
                        val2 = Code.GetValue(Code[ptr + 2], mode2);
                        Code.WriteValue(Code[ptr + 3], val1 * val2);
                        ptr += 4;
                        break;

                    case 3: // INPUT
                        if (!input.Any()) throw new Exception("Cannot read from empty input");
                        if (inptr >= input.Count) throw new Exception($"No input value in position {inptr}");
                        Code.WriteValue(Code[ptr + 1], input[inptr]);
                        inptr++;
                        ptr += 2;
                        break;

                    case 4: // OUTPUT
                        yield return Code.GetValue(Code[ptr + 1], mode1);
                        ptr += 2;
                        break;

                    case 5: // JUMP-IF-TRUE
                        ptr = Code.GetValue(Code[ptr + 1], mode1) != 0 ? (int)Code.GetValue(Code[ptr + 2], mode2) : ptr + 3;
                        break;

                    case 6: // JUMP-IF-FALSE
                        ptr = Code.GetValue(Code[ptr + 1], mode1) == 0 ? (int)Code.GetValue(Code[ptr + 2], mode2) : ptr + 3;
                        break;

                    case 7: // LESS-THAN
                        Code.WriteValue(Code[ptr + 3], Code.GetValue(Code[ptr + 1], mode1) < Code.GetValue(Code[ptr + 2], mode2) ? 1 : 0);
                        ptr += 4;
                        break;

                    case 8: // EQUALS
                        Code.WriteValue(Code[ptr + 3], Code.GetValue(Code[ptr + 1], mode1) == Code.GetValue(Code[ptr + 2], mode2) ? 1 : 0);
                        ptr += 4;
                        break;

                    case 99:
                        yield break;

                    default:
                        throw new Exception($"Invalid code in position {ptr}: {Code[ptr]}");
                }
            }
        }
    }

    public class Memory : Dictionary<long, long>
    {
        public Memory() { }
        public Memory(IDictionary<long, long> dictionary) : base(dictionary) { }

        public Memory Copy()
        {
            return new Memory(this.ToDictionary(kvp => kvp.Key, kvp => kvp.Value));
        }

        public long GetValue(long value, int mode)
        {
            return mode switch
            {
                1 => value,
                _ => ContainsKey(value) ? this[value] : 0
            };
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
