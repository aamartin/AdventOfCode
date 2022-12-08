using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AoC2022
{
    class Day4
    {
        internal static void Execute(string file)
        {
            Console.WriteLine("Day 4");
            Console.WriteLine($"Part1: {Part1(file)}");
            Console.WriteLine($"Part2: {Part2(file)}");
            Console.WriteLine();
        }

        private static int Part2(string file)
        {
            var total = 0;
            foreach (var line in File.ReadAllLines(file))
            {
                //bool included = false;
                var parts = line.Split(',');
                var area1 = PopulateMap(parts[0]);
                var area2 = PopulateMap(parts[1]);
                if (area1.Count <= area2.Count) { if(AreaOverlaps(new HashSet<int>(area2), area1)) total++; }
                else { if(AreaOverlaps(new HashSet<int>(area1), area2)) total++; }
            }
            return total;
        }

        private static bool AreaOverlaps(HashSet<int> area1, List<int> area2)
        {
           
            foreach (var item in area2)
            {
                if (area1.Contains(item))
                    return true;
            }
            return false;
        }

        private static int Part1(string file)
        {
            var total = 0;
            foreach(var line in File.ReadAllLines(file))
            {
                //bool included = false;
                var parts = line.Split(',');
                var area1 = PopulateMap(parts[0]);
                var area2 = PopulateMap(parts[1]);
                if (area1.Count <= area2.Count) { if (AreaIncluded(new HashSet<int>(area2), area1)) total++; }
                else { if (AreaIncluded(new HashSet<int>(area1), area2)) total++; }
            }
            return total;
        }

        private static bool AreaIncluded(HashSet<int> area1, List<int> area2)
        {
            foreach (var item in area2)
            {
                if (!area1.Contains(item)) 
                    return false;
            }
            return true;
        }

        private static List<int> PopulateMap(string v)
        {
            var result = new List<int>();
            var parts = v.Split('-');
            for(int i = int.Parse(parts[0]); i <= int.Parse(parts[1]); i++)
            {
                result.Add(i);
            }
            return result;
        }
    }
}
