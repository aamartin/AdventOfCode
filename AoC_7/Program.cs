using System;
using System.Collections.Generic;
using System.IO;

namespace AoC_7
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            Dictionary<string, Dictionary<string, int>> bags = new Dictionary<string, Dictionary<string, int>>(); 
            var total = 0;
            foreach(var line in lines)
            {
                var newline = line.Replace("contain", "_");
                newline = newline.Replace("bags", "");
                newline = newline.Replace("bag", "");
                newline = newline.Replace(".", "");
                newline = newline.Trim();
                var parts = newline.Split('_');

                var outter = new Dictionary<string, Dictionary<string, int>>();
                var outerbag = parts[0].Trim();
                var innerbags = parts[1].Split(',');
                Dictionary<string, int> ibag = new Dictionary<string, int>();
                foreach (var inner in innerbags)
                {
                    var triminner = inner.Trim('.', ' ');
                    if (String.Equals("no other", triminner))
                    {
                        ibag = null;
                    }
                    else
                    {
                        var number = triminner.Substring(0, 1);
                        int count = int.Parse(number);
                        triminner = triminner.Remove(0, 1);
                        ibag[triminner.Trim()] = count;
                    }
                }
                bags[outerbag] = ibag;
            }
            //var finbags = new List<string>();
            //var trails = new Dictionary<string, List<string>>();
            foreach (var outterbag in bags.Keys)
            {
                if (bags[outterbag] != null)
                {
                    var trail = new List<string>();
                    if (CheckMatches(bags, bags[outterbag], "shiny gold", trail))
                    {
                        total++;
                        //finbags.Add(outterbag);
                        //trails[outterbag] = trail;
                    }
                }
            }
            //int foo = finbags.Distinct().Count();
            Console.WriteLine(total);
            var depth = GoDeep(bags, bags["shiny gold"]);
        }

        private static int GoDeep(Dictionary<string, Dictionary<string, int>> bags, Dictionary<string, int> insideBags)
        {
            int total = 0;
            if (insideBags == null) return 0;
            foreach(var bag in insideBags.Keys)
            {
                total = total + insideBags[bag];
                total = total + (insideBags[bag] * GoDeep(bags, bags[bag]));
            }
            return total;
        }

        private static bool CheckMatches(Dictionary<string, Dictionary<string, int>> bags, Dictionary<string, int> value, string v, List<string> trial)
        {
            if (value == null) return false;
            foreach (var bag in value.Keys)
            {
                if (string.Equals(bag, v)) return true;
                else
                {
                    if(bags.ContainsKey(bag) && bags[bag] != null)
                    {
                        trial.Add(bag);
                        if (CheckMatches(bags, bags[bag], v, trial)) return true;
                    }
                }
            }
            return false;
        }
    }
}
