using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

namespace AoC2022
{
    class Day11
    {
        internal static void Execute(string file)
        {
            //var answer = new Answer();
            //answer.Part2(file);
            Console.WriteLine("Day 11");
            //Console.WriteLine($"Part1: {Part1(file)}");
            Console.WriteLine($"Part2: {Part2(file)}");
            Console.WriteLine();
        }

        private static ulong Part2(string file)
        {
            var monkies = GetMonkeys(file, false);
            ulong superMod = 1;
            foreach(var monkey in monkies)
            {
                superMod *= monkey.Test;
            }
            for (int i = 0; i < 10000; i++)
            {
                foreach (var monkey in monkies)
                {
                    var result = monkey.Eval(superMod);
                    foreach (var toss in result)
                    {
                        monkies[(int)toss.Value].Catch(toss.Key);
                    }
                }
            }

            List<ulong> inspections = new List<ulong>();
            foreach (var monkey in monkies)
            {
                inspections.Add(monkey.Inspections);
            }
            inspections.Sort();
            inspections.Reverse();

            return inspections[0] * inspections[1];
        }

        private static object Part1(string file)
        {
            var monkies = GetMonkeys(file);
            for (int i = 0; i < 20; i++)
            {
                foreach (var monkey in monkies)
                {
                    var result = monkey.Eval(1);
                    foreach (var toss in result)
                    {
                        monkies[(int)toss.Value].Catch(toss.Key);
                    }
                }
            }

            List<ulong> inspections = new List<ulong>();
            foreach (var monkey in monkies)
            {
                inspections.Add(monkey.Inspections);
            }
            inspections.Sort();
            inspections.Reverse();

            return inspections[0] * inspections[1];
        }

        private static List<Monkey> GetMonkeys(string file, bool extraWorry = true)
        {
            List<Monkey> monkeys = new List<Monkey>();
            Monkey curMonkey = null;
            foreach (var line in File.ReadAllLines(file))
            {
                if (curMonkey == null) curMonkey = new Monkey(extraWorry);
                if (String.IsNullOrWhiteSpace(line))
                {
                    monkeys.Add(curMonkey);
                    curMonkey = null;
                }
                if (line.StartsWith("Monkey"))
                {
                    var parts = line.Split(" ");
                    curMonkey.Number = ulong.Parse(parts[1].Replace(":", ""));

                }
                if (line.StartsWith("  Starting items"))
                {
                    var parts = line.Split(": ");
                    var items = parts[1].Split(", ");
                    foreach (var item in items) { curMonkey.items.Enqueue(ulong.Parse(item)); }
                }
                if (line.StartsWith("  Operation:"))
                {
                    var parts = line.Split(": ");
                    curMonkey.Op = parts[1];
                }
                if (line.StartsWith("  Test:"))
                {
                    var parts = line.Split(": ");
                    var test = parts[1];
                    var testparts = test.Split(" by ");
                    ulong value = ulong.Parse(testparts[1]);
                    curMonkey.Test = value;
                }
                if (line.StartsWith("    If true:"))
                {
                    var parts = line.Split(": ");
                    curMonkey.TrueCond = ulong.Parse(parts[1].Replace("throw to monkey ", ""));
                }
                if (line.StartsWith("    If false:"))
                {
                    var parts = line.Split(": ");
                    curMonkey.FalseCond = ulong.Parse(parts[1].Replace("throw to monkey ", ""));
                }
            }
            monkeys.Add(curMonkey);
            return monkeys;
        }
    }

    class Monkey
    {
        public Monkey(bool extraWorry)
        {
            items = new Queue<ulong>();
            Inspections = 0;
            this.extraWorry = extraWorry;
        }
        public ulong Number { get; set; }
        public Queue<ulong> items { get; set; }
        public string Op { get; set; }
        public ulong TrueCond { get; set; }
        public ulong FalseCond { get; set; }
        public ulong Test { get; set; }
        public ulong value { get; set; }
        public ulong Inspections { get; set; }

        private bool extraWorry;

        public void Catch(ulong item)
        {
            items.Enqueue(item);
        }

        public List<KeyValuePair<ulong, ulong>> Eval(ulong superMod)
        {
            var throws = new List<KeyValuePair<ulong, ulong>>();

            while (items.Count > 0)
            {
                Inspections++;
                var item = items.Dequeue();
                ulong newItem = evalOp(item, Op, superMod);
                bool passed = PassTest(newItem, Test);
                if (passed) throws.Add(new KeyValuePair<ulong, ulong>(newItem, TrueCond));
                else throws.Add(new KeyValuePair<ulong, ulong>(newItem, FalseCond));
            }
            return throws;
        }

        private bool PassTest(ulong newItem, ulong test)
        {
            return ((newItem % test) == 0);
        }

        private ulong evalOp(ulong item, string exp, ulong superMod)
        {
            var parts = exp.Split(" ");
            var left = GetValue(parts[2], item);
            var op = parts[3];
            var right = GetValue(parts[4], item);
            ulong worryLevel;
            switch (op)
            {
                case "+": worryLevel = left + right; break;
                case "-": worryLevel = left - right; break;
                case "*": worryLevel = left * right; break;
                case "/": worryLevel = left / right; break;
                default: throw new Exception($"Unknown op '{op}'");
            }

            if (extraWorry) return (ulong)Math.Floor((decimal)(worryLevel / 3));
            else
            {
                
                    return worryLevel % superMod;
            }


        }

        private ulong GetValue(string v, ulong item)
        {
            ulong value = 0;
            if (ulong.TryParse(v, out value)) return value;
            else return item;
        }
    }
}
    


