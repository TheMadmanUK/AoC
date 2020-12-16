using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2020
{
    public static class Day16
    {
        private enum ReadMode
        {
            None,
            Field,
            YourTicket,
            NearbyTickets
        }

        public static int RunPart1()
        {
            var lines = File.ReadAllLines(@"2020\Input\Day16.txt");
            var fieldCheck = new BitArray(1000);
            var readMode = ReadMode.Field;
            var tickets = new List<List<int>>();

            foreach (var line in lines)
            {
                switch (readMode)
                {
                    case ReadMode.None when line == "your ticket:":
                        readMode = ReadMode.YourTicket;
                        break;
                    case ReadMode.None when line == "nearby tickets:":
                        readMode = ReadMode.NearbyTickets;
                        break;
                    case ReadMode.None:
                        break;
                    case ReadMode.Field when string.IsNullOrEmpty(line):
                        readMode = ReadMode.None;
                        break;
                    case ReadMode.Field:
                        var fieldDetails = line.Split(": ")[1];
                        var fieldValues = fieldDetails.Split(" or ");
                        foreach (var values in fieldValues)
                        {
                            var numValues = values.Split('-').Select(int.Parse).ToList();
                            for (int i = numValues[0]; i <= numValues[1]; i++) fieldCheck[i] = true;
                        }
                        break;
                    case ReadMode.YourTicket:
                        readMode = ReadMode.None;
                        break;
                    case ReadMode.NearbyTickets:
                        tickets.Add(line.Split(',').Select(int.Parse).ToList());
                        break;
                }
            }

            var total = 0;
            foreach (var ticket in tickets)
                total += ticket.Where(x => !fieldCheck[x]).Sum();

            return total;
        }

        public static long RunPart2()
        {
            var lines = File.ReadAllLines(@"2020\Input\Day16.txt");
            var fields = new List<Field>();
            var fieldCheck = new BitArray(1000);
            var readMode = ReadMode.Field;
            var yourTicket = new List<int>();
            var tickets = new List<List<int>>();

            foreach (var line in lines)
            {
                switch (readMode)
                {
                    case ReadMode.None when line == "your ticket:":
                        readMode = ReadMode.YourTicket;
                        break;
                    case ReadMode.None when line == "nearby tickets:":
                        readMode = ReadMode.NearbyTickets;
                        break;
                    case ReadMode.None:
                        break;
                    case ReadMode.Field when string.IsNullOrEmpty(line):
                        readMode = ReadMode.None;
                        break;
                    case ReadMode.Field:
                        var fieldDetails = line.Split(": ");
                        List<(int From, int To)> fieldValues = fieldDetails[1].Split(" or ").Select(x => x.Split('-').Select(int.Parse).ToList()).Select(x => (x[0], x[1])).ToList();
                        foreach (var (from, to) in fieldValues)
                            for (int i = from; i <= to; i++) fieldCheck[i] = true;

                        fields.Add(new Field(fieldDetails[0], fieldValues[0].From, fieldValues[0].To, fieldValues[1].From, fieldValues[1].To));
                        break;
                    case ReadMode.YourTicket:
                        yourTicket = line.Split(',').Select(int.Parse).ToList();
                        readMode = ReadMode.None;
                        break;
                    case ReadMode.NearbyTickets:
                        var ticket = line.Split(',').Select(int.Parse).ToList();
                        if (ticket.All(x => fieldCheck[x])) tickets.Add(ticket);
                        break;
                }
            }

            var matrix = new List<List<int>>();

            for (int i = 0; i < fields.Count; i++)
            {
                var ticketValues = tickets.Select(x => x[i]).ToList();
                var matrixValues = new List<int>();
                for (int j = 0; j < fields.Count; j++)
                    if (ticketValues.All(x => fields[j].ValueFound(x))) matrixValues.Add(j);

                matrix.Add(matrixValues);
            }

            var removed = new List<int>();
            while (removed.Count < fields.Count)
            {
                for(int i = 0; i < fields.Count; i++)
                {
                    if (removed.Contains(i)) continue;
                    if (matrix[i].Count > 1) continue;
                    for (int j = 0; j < fields.Count; j++)
                    {
                        if (j == i) continue;
                        if (matrix[j].Contains(matrix[i][0])) matrix[j].Remove(matrix[i][0]);
                    }
                    removed.Add(i);
                }
            }

            var returnFields = Enumerable.Range(0, fields.Count).Where(x => fields[x].Name.StartsWith("departure")).ToList();
            return Enumerable.Range(0, fields.Count).Where(x => returnFields.Contains(matrix[x][0])).Aggregate(1L, (sum, x) => sum * yourTicket[x]);
        }

        private class Field {
            public string Name { get; init; }
            private int From1 { get; init; }
            private int To1 { get; init; }
            private int From2 { get; init; }
            private int To2 { get; init; }

            public Field(string name, int from1, int to1, int from2, int to2)
            {
                Name = name;
                From1 = from1;
                To1 = to1;
                From2 = from2;
                To2 = to2;
            }

            public bool ValueFound(int value)
            {
                var test = (value >= From1 && value <= To1) || (value >= From2 && value <= To2);
                return test;
            }
        }
    }
}
