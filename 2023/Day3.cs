using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace AoC2023
{
    class Number
    {
        public int Value { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int Length { get { return Value.ToString().Length; } }
        public override string ToString()
        {
            return Value.ToString();
        }

        internal bool HasOpAround(List<List<char>> map)
        {
            for(int i = x-1; i < x+Length+1; i++)
            {
                if (i < 0 || i > map[0].Count-1) continue;
                for(int j = y-1; j <= y+1; j++)
                {
                    if (j < 0 || j > map.Count-1) continue;
                    if (map[j][i] != '.' && !Char.IsDigit(map[j][i])) return true;
                }
            }
            return false;
        }
    }

    class NumberComparer : IEqualityComparer<Number>
    {
        public bool Equals(Number x, Number y)
        {

            //Check whether the compared objects reference the same data.
            if (Object.ReferenceEquals(x, y)) return true;

            //Check whether any of the compared objects is null.
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            //Check whether the products' properties are equal.
            return x.x == y.x && x.y == y.y && x.Value == y.Value;
        }

        // If Equals() returns true for a pair of objects
        // then GetHashCode() must return the same value for these objects.

        public int GetHashCode(Number num)
        {
            //Check whether the object is null
            if (Object.ReferenceEquals(num, null)) return 0;

            return $"{num.Value}:{num.x}:{num.y}".GetHashCode();
        }
    }

    class Day3
    {
        internal static void Execute(string file)
        {
            List<List<char>> map = new List<List<char>>();

            var lines = File.ReadAllLines(file);
            foreach (var line in lines)
            {
                map.Add(line.ToCharArray().ToList());
            }

            Console.WriteLine("Day 3");
            Console.WriteLine($"Part1: {Part1(map)}");
            Console.WriteLine($"Part2: {Part2(map)}");
            Console.WriteLine();
        }

        private static int Part2(List<List<char>> map)
        {
           

            var total = 0;
            for (int y = 0; y < map.Count; y++)
            {
                var row = map[y];
                for (int x = 0; x < row.Count; x++)
                {
                    if (map[y][x] == '*') // a gear
                    {
                        var gears = FindDigits(map, y, x).Distinct(new NumberComparer()).ToList();
                        if (gears.Count == 2) 
                            total = total + (gears[0].Value * gears[1].Value);
                    }
                }
            }
            return total;
        }

        private static int Part1(List<List<char>> map)
        {

            List<Number> numbers = new List<Number>();
            for (int y = 0; y < map.Count; y++)
            {
                var row = map[y];
                for (int x = 0; x < row.Count; x++)
                {
                    if (map[y][x] != '.' && !Char.IsDigit(map[y][x])) // Not a . or a number must be a symbol
                    {
                        numbers.AddRange(FindDigits(map, y, x));
                    }
                }
            }
            var unDupped = numbers.Distinct(new NumberComparer());
            var total = 0;
            
            foreach (var number in unDupped)
            {
                total = total + number.Value;
            }
            return total;
        }
    

        private static IEnumerable<Number> FindDigits(List<List<char>> map, int y, int x)
        {
            if (y > 0) // Look north 
            {
                var digits = new List<int>();
                var nw = FindDigit(map, y - 1, x - 1);
                var n = FindDigit(map, y - 1, x);
                var ne = FindDigit(map, y - 1, x + 1);
                if (ne != null) yield return ne;
                if(n != null) yield return n;
                if(nw != null) yield return nw;
            }
            if(y < map.Count-1) // look south
            {
                var digits = new List<int>();
                var sw = FindDigit(map, y + 1, x - 1);
                var s = FindDigit(map, y + 1, x);
                var se = FindDigit(map, y + 1, x + 1);
                if (se != null) yield return se;
                if (s != null) yield return s;
                if (sw != null) yield return sw;
            }
            var rowSize = map[y].Count;
            if(x < rowSize-1) // look east
            {
                var e = FindDigit(map, y, x + 1);
                if (e != null) yield return e;
            }
            if(x > 0) // look west
            {
                var w = FindDigit(map, y, x - 1);
                if(w != null) yield return w;
            }
        }

        private static Number FindDigit(List<List<char>> map, int y, int x)
        {
            Number num = null;
            if (!Char.IsDigit(map[y][x])) return null;

            StringBuilder numStr = new StringBuilder();
            var row = map[y];
            bool foundNum = false;
            for(int i = 0; i < row.Count; i++)
            {
                if (num != null && !Char.IsDigit(row[i]))
                {
                    if (foundNum) break;
                    else 
                    { 
                        num = null; 
                        numStr.Clear(); 
                    }
                }

                if (char.IsDigit(map[y][i]))
                {
                    if(num == null)
                    {
                        num = new Number();
                        num.x = i;
                    }
                    numStr.Append(map[y][i]);
                }
                if(i == x) foundNum = true;
            }
            if (numStr.Length > 0)
            {
                //num = new Number();
                num.Value = int.Parse(numStr.ToString());
                num.y = y;
            }
            return num;
        }
    }
}
