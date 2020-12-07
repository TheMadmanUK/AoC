using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2020
{
    public static class Day07
    {
        public static int RunPart1()
        {
            return BuildBags(File.ReadAllLines(@"2020\Input\Day07.txt")).Values.Count(bag => bag.ContainsShinyGold) - 1;
        }

        public static int RunPart2()
        {
            return BuildBags(File.ReadAllLines(@"2020\Input\Day07.txt"))["shiny gold"].CountBags() - 1;
        }

        private static Dictionary<string, Bag> BuildBags(string[] bagLines)
        {
            var bagList = new Dictionary<string, Bag>();

            foreach(var line in bagLines)
            {
                var details = line[..^1].Replace(" bags", "").Replace(" bag", "").Split(" contain ");
                var bagName = details[0].Trim();

                if (!bagList.ContainsKey(bagName)) bagList.Add(bagName, new Bag(bagName));

                if (details[1] == "no other") continue;
                var subbags = details[1].Split(", ");

                foreach (var subbag in subbags)
                {
                    var bagCount = int.Parse(subbag[..subbag.IndexOf(' ')].Trim());
                    var subBagName = subbag[subbag.IndexOf(' ')..].Trim();
                    if (!bagList.ContainsKey(subBagName))
                    {
                        bagList.Add(subBagName, new Bag(subBagName));
                    }

                    bagList[bagName].AddContained(bagList[subBagName], bagCount);
                }
            }

            return bagList;
        }

        private class Bag
        {
            public string Name { get; init; }
            public Dictionary<Bag, int> Contained { get; private set; }

            private bool? _containsShinyGold = null;
            private int? _totalBags;

            public Bag(string name)
            {
                Name = name;
                if (name == "shiny gold") _containsShinyGold = true;
                Contained = new Dictionary<Bag, int>();
            }

            public void AddContained(Bag bag, int value)
            {
                Contained.Add(bag, value);
            }

            public bool ContainsShinyGold {
                get
                {
                    if (!_containsShinyGold.HasValue)
                    {
                        _containsShinyGold = false;
                        foreach (var bag in Contained.Keys)
                        {
                            _containsShinyGold = bag.ContainsShinyGold;
                            if (_containsShinyGold.Value)
                            {
                                break;
                            }
                        }
                    }

                    return _containsShinyGold.Value;
                }
            }

            public int CountBags()
            {
                if (Contained.Count == 0) return 1;
                if (!_totalBags.HasValue)
                    _totalBags = Contained.Sum(bag => bag.Value * bag.Key.CountBags()) + 1;
                return _totalBags.Value;
            }
        }
    }
}
