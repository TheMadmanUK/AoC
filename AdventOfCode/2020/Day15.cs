using System.Linq;

namespace AdventOfCode._2020
{
    public static class Day15
    {
        private static readonly string input = "1,20,11,6,12,0";

        public static int RunPart1()
        {
            var inputList = input.Split(',').Select(int.Parse).ToList();

            var lastPosition = Enumerable.Range(0, inputList.Count - 1).ToDictionary(x => inputList[x], x => x + 1);
            var lastNumber = inputList.Last();

            for (int i = inputList.Count; i < 2020; i++)
            {
                int thisNumber = 0;

                if (lastPosition.ContainsKey(lastNumber))
                {
                    thisNumber = i - lastPosition[lastNumber];
                    lastPosition[lastNumber] = i;
                }
                else
                {
                    lastPosition.Add(lastNumber, i);
                }

                lastNumber = thisNumber;
            }

            return lastNumber;
        }

        public static int RunPart2()
        {
            var inputList = input.Split(',').Select(int.Parse).ToList();

            var lastPosition = Enumerable.Range(0, inputList.Count - 1).ToDictionary(x => inputList[x], x => x + 1);
            var lastNumber = inputList.Last();

            for (int i = inputList.Count; i < 30000000; i++)
            {
                int thisNumber = 0;

                if (lastPosition.ContainsKey(lastNumber))
                {
                    thisNumber = i - lastPosition[lastNumber];
                    lastPosition[lastNumber] = i;
                }
                else
                {
                    lastPosition.Add(lastNumber, i);
                }

                lastNumber = thisNumber;
            }

            return lastNumber;
        }
    }
}
