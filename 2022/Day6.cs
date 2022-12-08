using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AoC2022
{
    class Day6
    {
        internal static void Execute(string file)
        {
            Console.WriteLine("Day 6");
            Console.WriteLine($"Part1: {String.Join(',', Part1(file))}");
            Console.WriteLine($"Part2: {String.Join(',', Part2(file))}");
            Console.WriteLine();
        }

        private static List<int> Part2(string file)
        {
            var items = new List<int>();
            foreach (var line in File.ReadAllLines(file))
            {
                int marker = CalcFirstMarker(line, 14);
                items.Add(marker);
            }
            return items;
        }

        private static List<int> Part1(string file)
        {
            var items = new List<int>();
            foreach (var line in File.ReadAllLines(file))
            {
                int marker = CalcFirstMarker(line, 4);
                items.Add(marker);
            }
            return items;
        }

        private static int CalcFirstMarker(string line, int size)
        {
            
            for (int i = 0, j = size; j <= line.Length; i++, j++)
            {
                if (!HasMatchingElements(line.Substring(i, size))) return j;
            }
            throw new Exception();
        }

        private static bool HasMatchingElements(string v)
        {
            var markArry = new List<char>(v.ToCharArray());
            while(markArry.Count > 0)
            {
                var mark = markArry[0];
                markArry.RemoveAt(0);
                if (markArry.Contains(mark))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
