using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AoC2022
{
    class Day12
    {
        internal static void Execute(string file)
        {
            Console.WriteLine("Day 12");
            Console.WriteLine($"Part1: {Part1(file)}");
            Console.WriteLine($"Part2: {Part2(file)}");
            Console.WriteLine();
        }

        private static int Part2(string file)
        {
            int maxX, maxY = 0;
            var map = GenMap(file, out maxX, out maxY);

            var starts = FindStarts(map, 'a');
            var end = FindStart(map, 'E');

            return FindShortestPath(starts, end, map, maxX, maxY);
        }

        private static int Part1(string file)
        {
            int maxX, maxY = 0;
            var map = GenMap(file, out maxX, out maxY);

            var start = FindStart(map, 'S');
            var end = FindStart(map, 'E');

            return FindShortestPath(new List<Point>() { start }, end, map, maxX, maxY);
        }

        private static int FindShortestPath(List<Point> starts, Point end, List<List<Point>> map, int maxX, int maxY)
        {
            var visits = new HashSet<Point>();
            var pathQueue = new PriorityQueue<Point, int>();
            foreach (var start in starts)
            {
                pathQueue.Enqueue(start, 0);
            }
            Point curPoint = null;
            int len = 0;

            while (pathQueue.TryDequeue(out curPoint, out len))
            {
                if (!visits.Add(curPoint))
                    continue;

                if (curPoint == end)
                    break;

                // Move up
                if (curPoint.y + 1 < maxY)
                {
                    var nextPoint = map[curPoint.y + 1][curPoint.x];
                    if (!visits.Contains(nextPoint) && curPoint.CanMove(nextPoint)) pathQueue.Enqueue(nextPoint, len + 1);
                }

                // Move down
                if (curPoint.y - 1 >= 0)
                {
                    var nextPoint = map[curPoint.y - 1][curPoint.x];
                    if (!visits.Contains(nextPoint) && curPoint.CanMove(nextPoint)) pathQueue.Enqueue(nextPoint, len + 1);
                }

                // Move left
                if (curPoint.x - 1 >= 0)
                {
                    var nextPoint = map[curPoint.y][curPoint.x - 1];
                    if (!visits.Contains(nextPoint) && curPoint.CanMove(nextPoint)) pathQueue.Enqueue(nextPoint, len + 1);
                }

                // Move left
                if (curPoint.x + 1 < maxX)
                {
                    var nextPoint = map[curPoint.y][curPoint.x + 1];
                    if (!visits.Contains(nextPoint) && curPoint.CanMove(nextPoint)) pathQueue.Enqueue(nextPoint, len + 1);
                }
            }

            return len;
        }

        private static List<Point> FindStarts(List<List<Point>> map, char v)
        {
            List<Point> results = new List<Point>();
            foreach (var line in map)
            {
                foreach (var point in line)
                {
                    if (point.value == v || point.value == 'S') results.Add(point);
                }
            }
            return results;
        }

        private static void PrintMap(List<List<Point>> map)
        {
            for (int y = 0; y < map.Count; y++)
            {
                var line = map[y];
                for (int x = 0; x < line.Count; x++)
                {
                    if (line[x].isStart) Console.Write('S');
                    else if (line[x].isEnd) Console.Write('E');
                    else Console.Write(line[x].value);
                }
                Console.WriteLine();
            }
        }

        private static void printVisited(HashSet<Point> visits, int maxX, int maxY)
        {
            for(int y = 0; y < maxY; y++)
            {
                for (int x = 0; x < maxX; x++)
                {
                    var point = new Point() { x = x, y = y };
                    if (visits.Contains(point))
                    {
                        Point actualPoint;
                        visits.TryGetValue(point, out actualPoint);
                        Console.Write(actualPoint.value);
                    }
                    else Console.Write(".");
                }
                Console.WriteLine();
            }
        }

        private static Point FindStart(List<List<Point>> map, char v)
        {
            Point result = null;
            foreach(var line in map)
            {
                foreach(var point in line)
                {
                    if (result != null) return result;
                    switch(v)
                    {
                        case 'S': if( point.isStart) result = point; break;
                        case 'E': if (point.isEnd) result = point; break;
                        default: break;
                    }
                }
            }
            return null;
        }

        class Point
        {
            public int x;
            public int y;
            public char _value;
            public bool isStart;
            public bool isEnd;
            public char value
            {
                get { return _value; }
                set {
                    if (value == 'S')
                    {
                        _value = 'a';
                        isStart = true;
                    }
                    else if (value == 'E')
                    {
                        _value = 'z'; 
                        isEnd = true;
                    }
                    else _value = value;
                }
            }

            public override bool Equals(object obj)
            {
                Point other = obj as Point;
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return x == other.x && y == other.y;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(x, y);
            }

            public override string ToString()
            {
                return $"{x},{y} - {value}";
            }

            internal bool CanMove(Point nextPoint)
            {
                return this.value >= nextPoint.value || this.value + 1 == nextPoint.value;
                //return Math.Abs(nextPoint.value - this.value) <= 1;
            }
        }

        private static List<List<Point>> GenMap(string file, out int maxX, out int maxY)
        {
            maxX = 0;
            maxY = 0;
            List<List<Point>> result = new List<List<Point>>();
            foreach (var line in File.ReadAllLines(file))
            {
                List<Point> points = new();
                var splitLine = line.ToCharArray().ToList();
                for(int i = 0; i < splitLine.Count; i++)
                {
                    points.Add(new Point() { x = i, y = maxY, value = splitLine[i] });
                }
                maxX = splitLine.Count;
                result.Add(points);
                maxY++;
            }
            maxY = result.Count;
            return result;
        }
    }
}
