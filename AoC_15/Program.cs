using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC_15
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            foreach (var line in input)
            {
                var mem = new Dictionary<int, int[]>();
                var startingNums = line.Split(',').Select(i => int.Parse(i)).ToList();
                for (int turn = 1; turn <= 2020; turn++)
                {
                    SaySomething(mem, startingNums, turn);
                }
                Console.WriteLine(saidLast);

                mem = new Dictionary<int, int[]>();
                for (int turn = 1; turn <= 30000000; turn++) // I could continue from 2020 but whatever
                {
                    SaySomething(mem, startingNums, turn);
                }
                Console.WriteLine(saidLast);
            }
        }

        static int saidLast = -1;
        private static void SaySomething(Dictionary<int, int[]> mem, List<int> startingNums, int turn)
        {
            if (turn - 1 < startingNums.Count)
            {
                mem[startingNums[turn - 1]] = new int[2];
                mem[startingNums[turn - 1]][0] = turn;
                mem[startingNums[turn - 1]][1] = -1;
                saidLast = startingNums[turn - 1];
                return;
            }

            if (mem.ContainsKey(saidLast) && mem[saidLast][1] != -1) // had been spoken before
            {
                saidLast = mem[saidLast][0] - mem[saidLast][1];
            }
            else
            {
                saidLast = 0;
            }

            if (mem.ContainsKey(saidLast))
            {
                mem[saidLast][1] = mem[saidLast][0];
                mem[saidLast][0] = turn;
            }
            else
            {
                mem[saidLast] = new int[2];
                mem[saidLast][0] = turn;
                mem[saidLast][1] = -1;
            }
        }
    }
}
