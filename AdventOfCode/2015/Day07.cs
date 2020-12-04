using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode._2015
{
    public static class Day07
    {
        public static ushort RunPart1()
        {
            var nodes = LoadNodes();

            return nodes["a"].GetValue();
        }

        public static ushort RunPart2()
        {
            var nodes = LoadNodes();
            nodes["b"].SetValue(16076);

            return nodes["a"].GetValue();
        }

        private static Dictionary<string, Node> LoadNodes()
        {
            var instructions = File.ReadAllLines(@"2015\Input\Day07.txt");
            var nodes = new Dictionary<string, Node>();
            var regex = new Regex(@"^(?:(.*?) (AND) (.*?)|(.*?) (OR) (.*?)|(.*?) (LSHIFT) (.*?)|(.*?) (RSHIFT) (.*?)|(NOT) (.*?)|(.*?)) -> (.*)$");

            foreach (var instruction in instructions)
            {
                var match = instruction.Split(" ");
                switch (match.Length)
                {
                    case 3: // Direct insert
                        if (!nodes.ContainsKey(match[2])) nodes.Add(match[2], new Node(match[2]));
                        if (ushort.TryParse(match[0], out ushort value))
                        {
                            nodes[match[2]].SetValue(value);
                        }
                        else
                        {
                            if (!nodes.ContainsKey(match[0])) nodes.Add(match[0], new Node(match[0]));
                            nodes[match[2]].SetValue(nodes[match[0]]);
                        }
                        break;

                    case 4: // NOT
                        if (!nodes.ContainsKey(match[3])) nodes.Add(match[3], new Node(match[3]));
                        if (!nodes.ContainsKey(match[1])) nodes.Add(match[1], new Node(match[1]));
                        nodes[match[3]].SetNot(nodes[match[1]]);
                        break;

                    case 5 when match[1] == "AND":
                        if (!nodes.ContainsKey(match[4])) nodes.Add(match[4], new Node(match[4]));
                        if (!nodes.ContainsKey(match[2])) nodes.Add(match[2], new Node(match[2]));
                        if (ushort.TryParse(match[0], out value))
                        {
                            nodes[match[4]].SetAnd(value, nodes[match[2]]);
                        }
                        else
                        {
                            if (!nodes.ContainsKey(match[0])) nodes.Add(match[0], new Node(match[0]));
                            nodes[match[4]].SetAnd(nodes[match[0]], nodes[match[2]]);
                        }
                        break;

                    case 5 when match[1] == "OR":
                        if (!nodes.ContainsKey(match[4])) nodes.Add(match[4], new Node(match[4]));
                        if (!nodes.ContainsKey(match[2])) nodes.Add(match[2], new Node(match[2]));
                        if (ushort.TryParse(match[0], out value))
                        {
                            nodes[match[4]].SetOr(value, nodes[match[2]]);
                        }
                        else
                        {
                            if (!nodes.ContainsKey(match[0])) nodes.Add(match[0], new Node(match[0]));
                            nodes[match[4]].SetOr(nodes[match[0]], nodes[match[2]]);
                        }
                        break;

                    case 5 when match[1] == "LSHIFT":
                        if (!nodes.ContainsKey(match[4])) nodes.Add(match[4], new Node(match[4]));
                        if (!nodes.ContainsKey(match[0])) nodes.Add(match[0], new Node(match[0]));
                        var shift = ushort.Parse(match[2]);
                        nodes[match[4]].SetLShift(nodes[match[0]], shift);
                        break;

                    case 5 when match[1] == "RSHIFT":
                        if (!nodes.ContainsKey(match[4])) nodes.Add(match[4], new Node(match[4]));
                        if (!nodes.ContainsKey(match[0])) nodes.Add(match[0], new Node(match[0]));
                        shift = ushort.Parse(match[2]);
                        nodes[match[4]].SetRShift(nodes[match[0]], shift);
                        break;
                }
            }

            return nodes;
        }

        private class Node
        {
            private enum Type { And, AndValue, Or, OrValue, Not, LShift, RShift, Node, Value }

            public string Name { get; private set; }
            private Type? type;
            private Node? nodeA;
            private Node? nodeB;
            private ushort? value;
            private ushort? nodeValue;

            public Node(string name)
            {
                Name = name;
            }

            public void SetAnd(ushort value, Node node)
            {
                type = Type.AndValue;
                this.value = value;
                nodeA = node;
            }

            public void SetAnd(Node nodeA, Node nodeB)
            {
                type = Type.And;
                this.nodeA = nodeA;
                this.nodeB = nodeB;
            }

            public void SetOr(ushort value, Node node)
            {
                type = Type.OrValue;
                this.value = value;
                nodeA = node;
            }

            public void SetOr(Node nodeA, Node nodeB)
            {
                type = Type.Or;
                this.nodeA = nodeA;
                this.nodeB = nodeB;
            }

            public void SetNot(Node node)
            {
                type = Type.Not;
                nodeA = node;
            }

            public void SetLShift(Node node, ushort value)
            {
                type = Type.LShift;
                nodeA = node;
                this.value = value;
            }

            public void SetRShift(Node node, ushort value)
            {
                type = Type.RShift;
                nodeA = node;
                this.value = value;
            }

            public void SetValue(Node node)
            {
                type = Type.Node;
                nodeA = node;
            }

            public void SetValue(ushort value)
            {
                type = Type.Value;
                this.value = value;
            }

            public ushort GetValue()
            {
                if (nodeValue.HasValue) return nodeValue.Value;

                ushort thisValue = 0;

                switch (type)
                {
                    case Type.And:
                        thisValue = (ushort)(nodeA.GetValue() & nodeB.GetValue());
                        //Console.WriteLine($"{nodeA.Name} AND {nodeB.Name} => {Name} : {thisValue}");
                        break;

                    case Type.AndValue:
                        thisValue = (ushort)(value & nodeA.GetValue());
                        //Console.WriteLine($"{value} AND {nodeA.Name} => {Name} : {thisValue}");
                        break;

                    case Type.Or:
                        thisValue = (ushort)(nodeA.GetValue() | nodeB.GetValue());
                        //Console.WriteLine($"{nodeA.Name} OR {nodeB.Name} => {Name} : {thisValue}");
                        break;

                    case Type.OrValue:
                        thisValue = (ushort)(value | nodeA.GetValue());
                        //Console.WriteLine($"{value} OR {nodeA.Name} => {Name} : {thisValue}");
                        break;

                    case Type.Not:
                        thisValue = (ushort)~nodeA.GetValue();
                        //Console.WriteLine($"NOT {nodeA.Name} => {Name} : {thisValue}");
                        break;

                    case Type.LShift:
                        thisValue = (ushort)(nodeA.GetValue() << value);
                        //Console.WriteLine($"{nodeA.Name} LSHIFT {value} => {Name} : {thisValue}");
                        break;

                    case Type.RShift:
                        thisValue = (ushort)(nodeA.GetValue() >> value);
                        //Console.WriteLine($"{nodeA.Name} RSHIFT {value} => {Name} : {thisValue}");
                        break;

                    case Type.Node:
                        thisValue = nodeA.GetValue();
                        //Console.WriteLine($"{nodeA.Name} => {Name} : {thisValue}");
                        break;

                    case Type.Value:
                        thisValue = value.Value;
                        //Console.WriteLine($"{value} => {Name} : {thisValue}");
                        break;

                    default:
                        throw new System.Exception($"Unknown type in node {Name}!");
                }

                nodeValue = thisValue;
                return thisValue;
            }
        }
    }
}
