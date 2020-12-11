using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2019
{
    public static class Day06
    {
        public static int RunPart1()
        {
            var orbits = BuildOrbits();

            var orbitStart = orbits.Single(o => o.Name == "COM");
            return orbitStart.CountOrbits(0);
        }

        public static int RunPart2()
        {
            var orbits = BuildOrbits();

            var santaOrbits = new List<string>();
            var youOrbits = new List<string>();
            orbits.Single(o => o.Name == "SAN").GetTransitions(ref santaOrbits);
            orbits.Single(o => o.Name == "YOU").GetTransitions(ref youOrbits);

            var join = youOrbits.First(x => santaOrbits.Contains(x));
            return santaOrbits.IndexOf(join) + youOrbits.IndexOf(join) - 2;
        }

        private static List<Orbit> BuildOrbits()
        {
            var orbitList = File.ReadAllLines(@"2019\Input\Day06.txt");
            var orbits = new List<Orbit>();
            foreach (var line in orbitList)
            {
                var (from, to) = (line[..3], line[4..]);
                var orbitTo = orbits.FirstOrDefault(o => o.Name == to);
                if (orbitTo == null)
                {
                    orbitTo = new Orbit(to);
                    orbits.Add(orbitTo);
                }
                var orbitFrom = orbits.FirstOrDefault(o => o.Name == from);
                if (orbitFrom == null)
                {
                    orbitFrom = new Orbit(from);
                    orbits.Add(orbitFrom);
                }

                orbitFrom.Orbits.Add(orbitTo);
                orbitTo.Parent = orbitFrom;
            }

            return orbits;
        }

        private class Orbit
        {
            public string Name { get; }
            public Orbit Parent { get; set; }
            public List<Orbit> Orbits { get; }

            public Orbit(string name)
            {
                Name = name;
                Orbits = new List<Orbit>();
            }

            public int CountOrbits(int level)
            {
                return level + Orbits.Sum(o => o.CountOrbits(level + 1));
            }

            public void GetTransitions(ref List<string> transitions)
            {
                transitions.Add(Name);
                Parent?.GetTransitions(ref transitions);
            }
        }
    }
}
