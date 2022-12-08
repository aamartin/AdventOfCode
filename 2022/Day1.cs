using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AoC2022
{
    class ElfComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            var elf1 = (Tuple<int, int>)x;
            var elf2 = (Tuple<int, int>)y;

            return elf2.Item2.CompareTo(elf1.Item2);
        }
    }

    class Day1
    {
        internal static void Execute(string file)
        {
            Console.WriteLine("Day 1");
            Console.WriteLine($"Part1: {Part1(file)}");
            Console.WriteLine($"Part2: {Part2(file)}");
            Console.WriteLine();
        }

        private static int Part2(string file)
        {
            int elfCnt = 0;
            int totalCals = 0;
            var lines = File.ReadAllLines(file);
            List<Tuple<int, int>> elfMap = new List<Tuple<int, int>>();
            foreach (var line in lines)
            {
                if (String.IsNullOrWhiteSpace(line))
                {
                    elfCnt++;
                    elfMap.Add(new Tuple<int, int>(elfCnt, totalCals));
                    totalCals = 0;
                }
                else
                {
                    totalCals += int.Parse(line);
                }
            }
            elfCnt++;
            elfMap.Add(new Tuple<int, int>(elfCnt, totalCals));

            var elfArray = elfMap.ToArray();
            Array.Sort(elfArray, new ElfComparer());
            return elfArray[0].Item2 + elfArray[1].Item2 + elfArray[2].Item2;
        }

        private static int Part1(string file)
        {
            int elfCnt = 0;
            int biggestElf = 0;
            int totalCals = 0;
            int biggestCals = 0;
            var lines = File.ReadAllLines(file);
            foreach (var line in lines)
            {
                if (String.IsNullOrWhiteSpace(line))
                {
                    elfCnt++;
                    if (totalCals >= biggestCals)
                    {
                        biggestElf = elfCnt;
                        biggestCals = totalCals;
                    }
                    totalCals = 0;
                }
                else
                {
                    totalCals += int.Parse(line);
                }
            }
            return biggestCals;
        }
    }
}
