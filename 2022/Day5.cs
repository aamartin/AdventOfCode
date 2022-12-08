using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AOCDay1
{
    class Day5
    {
        internal static void Execute(string file)
        {
            Console.WriteLine("Day 5");
            Console.WriteLine($"Part1: {Part1(file)}");
            Console.WriteLine($"Part2: {Part2(file)}");
            Console.WriteLine();
        }

        private static string Part2(string file)
        {
            List<Stack<string>> items = new List<Stack<string>>();
            List<List<string>> lines = new List<List<string>>();
            //List<Stack<string>> temp = new List<Stack<string>>();
            foreach (var line in File.ReadLines(file))
            {
                if (line.Contains('['))
                {
                    lines.Add(ParseLine(line));
                }
                if (String.IsNullOrWhiteSpace(line)) items = ParseContainers(lines);
                if (line.Contains("move"))
                {
                    //if(temp.Count == 0) fo
                    var parts = line.Split(' ');
                    var ammount = int.Parse(parts[1]);
                    var from = int.Parse(parts[3]);
                    var to = int.Parse(parts[5]);

                    var temp = new Stack<string>();
                    for (int i = 0; i < ammount; i++)
                    {
                        var item = items[from - 1].Pop();
                        temp.Push(item);
                        
                    }

                    while(temp.Count > 0)
                    {
                        items[to - 1].Push(temp.Pop());
                    }
                }
            }

            StringBuilder result = new StringBuilder();
            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i].Peek();
                result.Append(item[1]);
            }
            return result.ToString();
        }

        private static string Part1(string file)
        {
            List<Stack<string>> items = new List<Stack<string>>();
            List<List<string>> lines = new List<List<string>>();
            foreach (var line in File.ReadLines(file))
            {
                if (line.Contains('['))
                {
                    lines.Add(ParseLine(line));
                }
                if (String.IsNullOrWhiteSpace(line)) items = ParseContainers(lines);
                if(line.Contains("move"))
                {
                    var parts = line.Split(' ');
                    var ammount = int.Parse(parts[1]);
                    var from = int.Parse(parts[3]);
                    var to = int.Parse(parts[5]);

                    for(int i = 0; i < ammount; i++)
                    {
                        var item = items[from - 1].Pop();
                        items[to - 1].Push(item);
                    }
                }
            }
            StringBuilder result = new StringBuilder();
            for(int i = 0; i < items.Count; i++)
            {
                var item = items[i].Peek();
                result.Append(item[1]);
            }
            return result.ToString();
        }

        private static List<Stack<string>> ParseContainers(List<List<string>> lines)
        {
            var items = new List<Stack<string>>();
            lines.Reverse();
            foreach(var line in lines)
            {
                if(items.Count == 0)
                {
                    for (int i = 0; i < line.Count; i++) items.Add(new Stack<string>());
                }

                for(int i = 0; i < line.Count; i++)
                {
                    if (!String.IsNullOrWhiteSpace(line[i])) items[i].Push(line[i]);
                }
            }

            return items;
        }

        private static List<string> ParseLine(string line)
        {
            var items = new List<string>();
            StringBuilder item = new StringBuilder();
            int i = 0;
            while (i < line.Length)
            {
                item.Append(line[i]);
                item.Append(line[i + 1]);
                item.Append(line[i + 2]);

                items.Add(item.ToString());
                item.Clear();

                i += 4;
            }

            return items;
        }
    }
}
