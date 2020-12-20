using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2020
{
    public static class Day18
    {

        public static long RunPart1()
        {
            return File.ReadAllLines(@"2020\Input\Day18.txt").Sum(Evaluate);
        }

        public static long RunPart2()
        {
           return File.ReadAllLines(@"2020\Input\Day18.txt").Sum(EvaluateMultFirst);
        }

        private static long Evaluate (string expression)
        {
            return Evaluate(expression.Split(" "));
        }

        private static long Evaluate (string[] expression)
        {
            string lastCommand = "S";
            long total = 0;
            HashSet<string> commands = new HashSet<string> { "+", "*" };

            for (var i = 0; i < expression.Length; i++)
            {
                if (commands.Contains(expression[i]))
                {
                    lastCommand = expression[i];
                    continue;
                }

                long subtotal;

                if (expression[i].StartsWith("("))
                {
                    var start = i;
                    var end = 0;
                    var count = expression[i].Count(x => x == '(');
                    for (int j = i + 1; j < expression.Length; j++)
                    {
                        if (expression[j].StartsWith("("))
                        {
                            count += expression[j].Count(x => x == '(');
                        }
                        else if (expression[j].EndsWith(")"))
                        {
                            count -= expression[j].Count(x => x == ')');
                            if (count == 0)
                            {
                                end = j;
                                break;
                            }
                        }
                    }

                    if (end == 0) throw new Exception("End not found!");
                    i = end;
                    var subsum = expression[start..(end + 1)];
                    subsum[0] = subsum[0][1..];
                    subsum[^1] = subsum[^1][..^1];

                    subtotal = Evaluate(subsum);
                }
                else
                {
                    subtotal = long.Parse(expression[i]);
                }

                total = lastCommand switch
                {
                    "S" => subtotal,
                    "+" => total + subtotal,
                    "*" => total * subtotal,
                    _ => throw new Exception($"Unknown command: {lastCommand}")
                };
            }

            return total;
        }

        private static long EvaluateMultFirst(string expression)
        {
            return EvaluateMultFirst(expression.Split(" "));
        }

        private static long EvaluateMultFirst(string[] expression)
        {
            var pass1 = new List<string>();

            for (var i = 0; i < expression.Length; i++)
            {
                if (expression[i].StartsWith("("))
                {
                    var start = i;
                    var end = 0;
                    var count = expression[i].Count(x => x == '(');
                    for (int j = i + 1; j < expression.Length; j++)
                    {
                        if (expression[j].StartsWith("("))
                        {
                            count += expression[j].Count(x => x == '(');
                        }
                        else if (expression[j].EndsWith(")"))
                        {
                            count -= expression[j].Count(x => x == ')');
                            if (count == 0)
                            {
                                end = j;
                                break;
                            }
                        }
                    }

                    if (end == 0) throw new Exception("End not found!");
                    i = end;
                    var subsum = expression[start..(end + 1)];
                    subsum[0] = subsum[0][1..];
                    subsum[^1] = subsum[^1][..^1];

                    pass1.Add(EvaluateMultFirst(subsum).ToString());
                }
                else
                {
                    pass1.Add(expression[i]);
                }
            }

            long total = 1;
            long subtotal = long.Parse(pass1[0]);

            for (int i = 2; i < pass1.Count; i += 2)
            {
                long value = long.Parse(pass1[i]);
                if (pass1[i - 1] == "*")
                {
                    total *= subtotal;
                    subtotal = value;
                }
                else
                {
                    subtotal += value;
                }
            }
            total *= subtotal;
            return total;
        }
    }
}
