using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

namespace AoC_16
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");

            int errors = 0;
            var allValid = new HashSet<int>();
            var feilds = new List<HashSet<int>>();
            var validTickets = new List<string>();
            List<int> departureFeilds = new List<int>();
            var highestFieldValue = -1;
            string myTicket = string.Empty;
            int section = 0;
            int index = 0;
            foreach(var line in lines)
            {
                if(String.IsNullOrEmpty(line))
                {
                    section++;
                    continue;
                }
                if (section == 0) // Init block
                {
                    if(line.Contains("departure")) { departureFeilds.Add(index); }
                    var parts = line.Split(' ');
                    var validFields = new HashSet<int>();
                    foreach (var part in parts)
                    {
                        if (part.Contains("-"))
                        {
                            var startend = part.Split('-');
                            for (int i = int.Parse(startend[0]); i <= int.Parse(startend[1]); i++)
                            {
                                allValid.Add(i);
                                validFields.Add(i);
                                if (i > highestFieldValue) highestFieldValue = i;
                            }
                        }
                    }
                    
                    feilds.Add(validFields);
                    index++;
                }
                else if (section == 1)
                {
                    if (line.Contains(":")) continue;
                    myTicket = line;
                }
                else if (section == 2)
                {
                    if (line.Contains(":")) continue;
                    var values = line.Split(',');
                    bool validTicket = true;
                    foreach (var value in values)
                    {
                        var intValue = int.Parse(value);
                        if (!allValid.Contains(intValue))
                        {
                            errors = errors + intValue;
                            validTicket = false;
                        }
                    }
                    if (validTicket)
                    {
                        validTickets.Add(line);
                    }
                }
            }
            Console.WriteLine($"Part 1: {errors}");

            var part2 = Part2(myTicket, feilds, validTickets, highestFieldValue);
            var myTicketParts = myTicket.Split(',');
            long part2Answer = 1;
            foreach(var departureField in departureFeilds)
            {
                part2Answer *= int.Parse(myTicketParts[part2[departureField]]);
            }

            Console.WriteLine($"Part 2: {part2Answer}");
        }

        private static int[] Part2(string myTicket, List<HashSet<int>> feilds, List<string> validTickets, int highestFieldValue)
        {
            // Build a bitmask for each feild that could contain a valid feild
            int[] possiblefieldsByValue = new int[highestFieldValue+1];
            for(int i = 0; i < feilds.Count; i++)
            {
                int flag = 1 << i;
                foreach(var field in feilds[i])
                {
                    possiblefieldsByValue[field] |= flag;
                }
            }

            // initilize a bitmap table that stores the feild per ticket mapping.
            var fieldCandidates = new int[feilds.Count];
            for(int i = 0; i < feilds.Count; i++)
                fieldCandidates[i] = -1;

            foreach(var ticket in validTickets)
            {
                var ticketParts = ticket.Split(',');
                for(int i = 0; i < feilds.Count; i++)
                {
                    fieldCandidates[i] &= possiblefieldsByValue[int.Parse(ticketParts[i])];
                }
            }

            return GetFieldIndexes(fieldCandidates);
        }

        public static int CountTrailingZero(int x)
        {
            int count = 0;

            while ((x & 1) == 0)
            {
                x = x >> 1;
                count++;
            }
            return count;
        }

        private static int[] GetFieldIndexes(int[] candidates)
        {
            int[] fieldIndexes = new int[candidates.Length];

            // This loop will identify a single field index each iteration
            // so this loop runs candidates.Length times 
            for (int fieldsLeft = candidates.Length; fieldsLeft > 0; fieldsLeft--)
            {
                int fieldToRemove = 0;
                for (int j = 0; j < candidates.Length; j++)
                {
                    int fields = candidates[j];

                    // if we have already identified the field index, then fields will be 0 so we skip it
                    if (fields == 0)
                        continue;

                    // x & (x - 1) == 0 is a way to test that x only has 1 bit set
                    // when there is only 1 bit set, it means we have identified the field index
                    if ((fields & (fields - 1)) == 0)
                    {
                        // TrailingZeroCount is a fast way to identify which bit is set to 1
                        int field = CountTrailingZero(fields);
                        fieldIndexes[field] = j;
                        fieldToRemove = fields;
                        break;
                    }
                }

                // remove the field from each of the candidates now that we have identified it's field index
                for (int j = 0; j < candidates.Length; j++)
                {
                    candidates[j] &= ~fieldToRemove;
                }
            }

            return fieldIndexes;
        }
    }
}
