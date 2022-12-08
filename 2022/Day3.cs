using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AOCDay1
{
    class Day3
    {
        internal static void Execute(string file)
        {
            Console.WriteLine("Day 3");
            Console.WriteLine($"Part1: {Part1(file)}");
            Console.WriteLine($"Part2: {Part2(file)}");
            Console.WriteLine();
        }

        private static int Part2(string file)
        {
            var total = 0;
            var lines = File.ReadAllLines(file);
            Dictionary<char, int> bagItems = new Dictionary<char, int>();
            for(int lineNumber = 0; lineNumber < lines.Length; lineNumber++)
            {
                foreach(var item in lines[lineNumber].ToCharArray().Distinct())
                {
                    if (!bagItems.ContainsKey(item)) bagItems.Add(item, 0);
                    bagItems[item]++;
                }
                if((lineNumber+1) % 3 == 0)
                {
                    foreach(var cnt in bagItems)
                    {
                        if(cnt.Value == 3)
                        {
                            total += CalcValue(cnt.Key);
                            bagItems.Clear();
                        }
                    }
                }
            }
            return total;
        }

        private static int Part1(string file)
        {
            int total = 0;
            foreach(var line in File.ReadAllLines(file))
            {
                HashSet<char> items = new HashSet<char>();
                var midPoint = line.Length / 2;
                char dup = Char.MinValue;
                for (int i = 0; i < line.Length; i++)
                {
                    if (i < midPoint) items.Add(line[i]);
                    else
                    {
                        if (items.Contains(line[i]))
                        {
                            dup = line[i];
                            break;
                        }
                    }
                }

                total += CalcValue(dup);
            }

            return total;
        }

        private static int CalcValue(char dup)
        {
            int value;
            if (char.IsUpper(dup))
                value = (int)dup - 38;
            else value = (int)dup - 96;
            return value;
        }
    }
}
