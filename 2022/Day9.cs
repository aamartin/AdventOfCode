using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AoC2022
{
    class Day9
    {
        internal static void Execute(string file)
        {
            Console.WriteLine("Day 9");
            Console.WriteLine($"Part1: {Part1(file)}");
            Console.WriteLine($"Part2: {Part2(file)}");
            Console.WriteLine();
        }

        private static object Part2(string file)
        {
            RopeEnd head = new RopeEnd(9);
            ProcessCommands(file, head);
            return head.tail.tail.tail.tail.tail.tail.tail.tail.tail.VistedLocations.Count; // Super gross I know...but it works.
        }

        private static void ProcessCommands(string file, RopeEnd head)
        {
            List<string> commands = new List<string>(File.ReadAllLines(file));
            foreach (var command in commands)
            {
                var parts = command.Split();
                var amount = int.Parse(parts[1]);
                switch (parts[0])
                {
                    case "R": head.MoveRight(amount); break;
                    case "U": head.MoveUp(amount); break;
                    case "L": head.MoveLeft(amount); break;
                    case "D": head.MoveDown(amount); break;
                }
            }
        }

        private static object Part1(string file)
        {
            RopeEnd head = new RopeEnd(1);
            ProcessCommands(file, head);
            return head.tail.VistedLocations.Count;
        }
    }

    class RopeEnd
    {
        public RopeEnd(int size, int len = 0)
        {
            x = 0;
            y = 0;
            if(size > len) tail = new RopeEnd(size, len+1);
        }
        public RopeEnd tail { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public HashSet<string> VistedLocations = new HashSet<string>();
        public void MoveUp(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                y++;
                Visit();
                if (tail != null) tail.Follow(x, y);
            }
        }

        public void MoveDown(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                y--;
                Visit();
                if (tail != null) tail.Follow(x, y);
            }
        }

        public void MoveLeft(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                x--;
                Visit();
                if (tail != null) tail.Follow(x, y);
            }
        }

        public void MoveRight(int amount)
        {
            for(int i = 0; i < amount; i++)
            {
                x++;
                Visit();
                if (tail != null) tail.Follow(x, y);
            }
        }

        private void Visit()
        {
            var location = $"{x},{y}";
            if (!VistedLocations.Contains(location)) VistedLocations.Add(location);
        }

        private void Follow(int hx, int hy)
        {
            if ((Distance(hx,x) > 1 || Distance(hy,y) > 1) && hx != x && hy != y)
            {
                if (hx > x) x++; // head right
                if (hx < x) x--; // head left
                if (hy > y) y++; // head above
                if (hy < y) y--; // head below
            }
            else
            {
                if (Distance(hx,x) > 1)
                {
                    if (hx > x) x++; // head right
                    if (hx < x) x--; // head left
                }
                if (Distance(hy,y) > 1)
                {
                    if (hy > y) y++; // head above
                    if (hy < y) y--; // head below
                }
            }
            Visit();
            if (tail != null) tail.Follow(x, y);
        }

        private double Distance(int a, int b)
        {
            var ans = Math.Sqrt(Math.Pow((a - b),2));
            return ans;
        }
    }
}
