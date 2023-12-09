using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace AoC2023
{
    internal class Day5
    {
        public class SourceDestMap
        {
            List<Tuple<Int64, Int64, Int64>> map = new List<Tuple<long, long, long>>();
         
            public void Parse(List<string> mapGuide)
            {   // Dest source, count
                // 50 98 2
                // 52 50 48
                foreach(string s in mapGuide)
                {
                    var items = s.Split(' ');
                    var dest = Int64.Parse(items[0]);
                    var source = Int64.Parse(items[1]);
                    var count = Int64.Parse(items[2]);
                        map.Add(new Tuple<long, long, long>(source, dest, count));
                }

            }

            public Int64 GetDest(Int64 source)
            {
                Int64 result = source;
                foreach (var item in map)
                {
                    if (source >= item.Item1 && source < item.Item1 + item.Item3)
                        result = (item.Item2 - item.Item1) + source;
                }
                return result;
            }
        }

        public class Almanac
        {
            public Almanac()
            {
                map = new List<SourceDestMap>();
                seeds = new List<Int64>();
                seeds2 = new List<KeyValuePair<Int64, Int64>>();
            }

            public List<SourceDestMap> map { get; }
            public List<Int64> seeds { get; }
            public List<KeyValuePair<Int64, Int64>> seeds2 {get;}

        }

        public static Almanac ParseFile(string file, bool part2 = false)
        {
            Almanac almanac = new Almanac();
            List<string> lines = new List<string>();
            foreach(var line in File.ReadAllLines(file))
            {
                if (line.StartsWith("seeds:"))
                {
                    var parts = line.Split(' ').ToList();
                    parts.RemoveAt(0);
                    if (!part2)
                    {
                        foreach (var part in parts)
                            almanac.seeds.Add(Int64.Parse(part));
                    }
                    else
                    {
                        bool first = true;
                        Int64 start = 0;
                        foreach(var part in parts)
                        {
                            var partNum = Int64.Parse(part);
                            if(!first)
                            {
                                
                                    almanac.seeds2.Add( new KeyValuePair<long, long>( start, partNum));
                                
                                first = true;
                                start = 0;
                            }
                            else
                            {
                                first = false;
                                start = partNum;
                            }
                        }
                    }

                    continue;
                }
                else if (String.IsNullOrEmpty(line))
                {
                    if (lines.Any())
                    {
                        var map = new SourceDestMap();
                        map.Parse(lines);
                        almanac.map.Add(map);
                        lines.Clear();
                    }
                }
                else if (line.Contains(':')) continue;
                else
                {
                    lines.Add(line);
                }
            }
            if(lines.Any()) 
            {
                var map = new SourceDestMap();
                map.Parse(lines);
                almanac.map.Add(map);
            }
            return almanac;
        }

        internal static void Execute(string v)
        {
            Console.WriteLine("Day 5");
            Console.WriteLine($"Part1: {Part1(v)}");
            Console.WriteLine($"Part2: {Part2(v)}");
            Console.WriteLine();

        }

        private static object Part2(string v)
        {
            var almanac = ParseFile(v, true);
            Int64 shortest = Int64.MaxValue;
            foreach (var seed in almanac.seeds2)
            {
                Parallel.For(seed.Key, seed.Key + seed.Value, i =>
                {
                    var source = i;
                    foreach (var path in almanac.map)
                    {
                        source = path.GetDest(source);
                    }
                    if (source < shortest) shortest = source;

                });
            }

            return shortest;
        }

        private static Int64 Part1(string v)
        {
            var almanac = ParseFile(v);
            Int64 shortest = Int64.MaxValue;
            foreach (var seed in almanac.seeds)
            {
                var source = seed;
                foreach(var path in almanac.map)
                {
                    source = path.GetDest(source);
                }
                if(source < shortest) shortest = source;
            }

            return shortest;
        }
    }
}
