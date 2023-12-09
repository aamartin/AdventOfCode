using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2023
{
    internal class Day6
    {
        class Race
        {
            public Race(long time, long distance)
            {
                Time = time;
                Distance = distance;    
            }

            public long Time { get; internal set; }
            public long Distance { get; internal set; }
        }

        internal static void Execute(string v)
        {
            Console.WriteLine("Day 6");
            Console.WriteLine($"Part1: {Part1(v)}");
            Console.WriteLine($"Part2: {Part2(v)}");
            Console.WriteLine();
        }

        private static long Part1(string v)
        {
            long result = 1;
            var races = ParseFile(v);
            foreach(var race in races)
            {
                var raceWins = CalculateWays(race.Time, race.Distance);
                result = result * raceWins;
            }

            return result;
        }

        private static List<Race> ParseFile(string v, bool part2 = false)
        {
            var result = new List<Race>();
            var times = new List<long>();
            var distances = new List<long>();
            foreach(var line in File.ReadAllLines(v))
            {
                var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
                parts.RemoveAt(0);
                if (!times.Any())
                {
                    if(!part2) 
                        times.AddRange(parts.Select(long.Parse));
                    else
                    {
                        times.Add(long.Parse(String.Join("", parts)));
                    }
                }
                else
                {
                    if(!part2) 
                        distances.AddRange(parts.Select(long.Parse));
                    else
                    {
                        distances.Add(long.Parse(String.Join("", parts)));
                    }
                }
            }

            for(int i = 0; i < times.Count; i++)
            {
                result.Add(new Race(times[i], distances[i]));
            }
            return result;
        }

        private static long CalculateWays(long time, long distance)
        {
            var waysToWin = 0;
            for(long holdtime = 0; holdtime <= time; holdtime++)
            {
                var timeleft = time - holdtime;
                var travelDist = holdtime * timeleft;
                if(travelDist > distance) waysToWin++;
            }
            return waysToWin;
        }

        private static long Part2(string v)
        {
            long result = 1;
            var races = ParseFile(v,true);
            foreach (var race in races)
            {
                var raceWins = CalculateWays(race.Time, race.Distance);
                result = result * raceWins;
            }

            return result;
        }
    }
}
