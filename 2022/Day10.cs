using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AoC2022
{
    class Day10
    {
        internal static void Execute(string file)
        {
            Console.WriteLine("Day 10");
            Console.WriteLine($"Part1: {Part1(file)}");
            Console.WriteLine($"Part2: {Part2(file)}");
            Console.WriteLine();
        }

        private static object Part2(string file)
        {
            List<int> signalStrength = new List<int>();
            var total = 0;
            Stack<int> ops = new Stack<int>();
            int cycle = 0;
            int pixelPos = 0;
            int X = 1;
            var commands = File.ReadAllLines(file).ToList();
            commands.Reverse();
            foreach (var command in commands)
            {
                var parts = command.Split(" ");
                switch (parts[0])
                {
                    case "addx":
                        var value = int.Parse(parts[1]);
                        ops.Push(value);
                        ops.Push(0);
                        break;
                    default:
                        ops.Push(0);
                        break;
                }
            }
            ops.Push(0);
            while (ops.Count > 0)
            {
                var op = ops.Pop();
                X += op;
                cycle++;
                if (X == (pixelPos - 1) || X == pixelPos || X == (pixelPos + 1)) Console.Write("#");
                else Console.Write(".");

                if (cycle % 40 == 0 && pixelPos != 0)
                {
                    Console.WriteLine();
                }
                pixelPos++;
                if (pixelPos == 40) pixelPos = 0;
            }

            return total;
        }

        private static object Part1(string file)
        {
            List<int> signalStrength = new List<int>();
            var total = 0;
            Stack<int> ops = new Stack<int>();
            int cycle = 0;
            int X = 1;
            var commands = File.ReadAllLines(file).ToList();
            commands.Reverse();
            foreach(var command in commands)
            {
                var parts = command.Split(" ");
                switch(parts[0])
                {
                    case "addx":
                        var value = int.Parse(parts[1]);
                        ops.Push(value);
                        ops.Push(0);
                        break;
                    default:
                        ops.Push(0);
                        break;
                }
            }
            ops.Push(0);
            while(ops.Count > 0)
            {
                var op = ops.Pop();
                X += op;
                cycle++;
                if(cycle == 20 || cycle == 60 || cycle == 100 || cycle == 140 || cycle == 180 || cycle == 220)
                {
                    total += (cycle * X);
                }
            }

            return total;

        }

        private static void AddCycle(int v)
        {
            throw new NotImplementedException();
        }
    }
}
