using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2022
{
    internal class Day13
    {
        internal static void Execute(string file)
        {
            Console.WriteLine("Day 13");
            Console.WriteLine($"Part1: {Part1(file)}");
            //Console.WriteLine($"Part2: {Part2(file)}");
            Console.WriteLine();
        }

        private static int Part2(string file)
        {
            throw new NotImplementedException();
        }

        private static int Part1(string file)
        {
            var good = new List<int>();
            var output = ParseFile(file);
            for(int i = 0; i< output.Count; i++)
            {
                var packet1 = output[i].Key;
                var packet2 = output[i].Value;
                if (ComparePackets(packet1, packet2, false)) good.Add(i+1);
            }
            var total = 0;
            foreach (var item in good) total += item;
            return total;
        }

        private static bool ComparePackets(Queue<object> packet1, Queue<object> packet2, bool wasConversion)
        {
            bool result = true;
            while (packet1.Any() && packet2.Any())
            {
                var left = packet1.Dequeue();
                var right = packet2.Dequeue();
                if (left is Queue<object> && right is Queue<object>)
                {
                    var newleft = left as Queue<object>;
                    var newright = right as Queue<object>;
                    result = result && ComparePackets(newleft, newright, false);
                }
                if(left is int && right is Queue<object>)
                {
                    Queue<object> queue = new Queue<object>();
                    queue.Enqueue(left);
                    var newRight = right as Queue<object>;
                    result = result && ComparePackets(queue, newRight, true);
                }
                if(right is int && left is Queue<object>)
                {
                    Queue<object> queue = new Queue<object>();
                    queue.Enqueue(right);
                    result = result && ComparePackets(left as Queue<object>, queue, true);
                }
                if(left is int && right is int)
                {
                    if((int)right < (int)left) { return false; }
                }
                    
            }
            if(packet1.Any() && !wasConversion)
            {
                return false;
            }

            return result;
        }

        private static List<KeyValuePair<Queue<object>, Queue<object>>> ParseFile(string file)
        {
            var results = new List<KeyValuePair<Queue<object>, Queue<object>>>();
            var lines = File.ReadLines(file).ToList();
            Queue<object> left = null;
            Queue<object> right = null;
            for (int i = 0; i < lines.Count; i++)
            {

                if(i%3 == 2)
                {
                    results.Add(new KeyValuePair<Queue<object>, Queue<object>>(left, right));
                    left = null;
                    right = null;
                }
                else if(i%3 == 1)
                {
                    int pos = 0;
                    right = ParsePackets(lines[i].ToCharArray(), ref pos).Dequeue() as Queue<object>;
                }
                else if(i % 3 == 0)
                {
                    int pos = 0;
                    left = ParsePackets(lines[i].ToCharArray(), ref pos).Dequeue() as Queue<object>;
                }
            }
            if(left != null && right != null) { results.Add(new KeyValuePair<Queue<object>, Queue<object>>(left, right)); }
            return results;
        }

        private static Queue<object> ParsePackets(char[] value, ref int pos)
        {
            var curQueue = new Queue<object>();
            var innerPos = pos;
            while(pos < value.Length)
            {
                switch(value[pos]) 
                {
                    case '[':
                        pos++;
                        curQueue.Enqueue(ParsePackets(value, ref pos));
                        break;
                    case ']':
                        pos++;
                        return curQueue;
                    case ',':
                        pos++;
                        break;
                    default:
                        char c = value[pos];
                        int curItem = int.Parse(c.ToString());
                        curQueue.Enqueue(curItem);
                        pos++;
                        break;
                }
            }

            return curQueue;

            //var result = new Packet();
            //List<Packet> curPackets = new List<Packet>();
            //foreach(var element in value.ToCharArray())
            //{
            //    switch(element)
            //    {
            //        case '[':
            //            break;
            //        case ',':
            //            break;
            //        case ']':
            //            break;
            //        default:
            //            break;
            //    }
            //}
            //
            //return result;
        }
    }

    class Packet 
    {
        public Packet()
        {
            packets = new List<Packet>();
            values = new List<int>();
        }

        public List<Packet> packets;
        public List<int> values;

    }
}
