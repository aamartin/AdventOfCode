using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;

namespace AoC_14
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            char[] mask = new char[64];
            Dictionary<UInt64, UInt64> storage = new Dictionary<UInt64, UInt64>();
            foreach(var line in lines)
            {
                if(line.StartsWith("mask"))
                {
                    var newline = line.Replace("mask = ", "");
                    newline = newline.PadLeft(64, 'X');
                    mask = newline.ToCharArray();
                }
                else
                {
                    var parts = line.Split(" = ");
                    var memAddress = GetMemAddress(parts[0]);
                    var value = UInt32.Parse(parts[1]);
                    var maskedValue = ApplyMask(mask, value);
                    storage[memAddress] = maskedValue;
                }
                
            }
            UInt64 answer = 0;
            foreach (var value in storage.Values)
            {
                answer = answer + value;
            }
            Console.WriteLine(answer);

            // Part 2
            lines = File.ReadAllLines("input.txt");
            mask = new char[64];
            storage = new Dictionary<UInt64, UInt64>();
            foreach (var line in lines)
            {
                if (line.StartsWith("mask"))
                {
                    var newline = line.Replace("mask = ", "");
                    newline = newline.PadLeft(64, '0');
                    mask = newline.ToCharArray();
                }
                else
                {
                    var parts = line.Split(" = ");
                    var memAddresses = GetFloatingMemAddresses(parts[0], mask);
                    var value = UInt32.Parse(parts[1]);
                    foreach (var address in memAddresses)
                    {
                        storage[address] = value;
                    }
                }

            }
            answer = 0;
            foreach (var value in storage.Values)
            {
                answer = answer + value;
            }
            Console.WriteLine(answer);


        }

        private static UInt64 ApplyMask(char[] mask, UInt32 value)
        {
            var foo = Convert.ToString(value, 2);
            foo = foo.PadLeft(64, '0');
            var foobits = foo.ToCharArray();
            //var foo = BitConverter.GetBytes(value);
            for (var i = 0; i < mask.Length; i++)
            {
                if (mask[i] == '0') { foobits[i] = '0'; }
                else if (mask[i] == '1') foobits[i] = '1';
            }
            var masked = new String(foobits);
            //return 0;
            return Convert.ToUInt64(masked, 2);
        }

        private static UInt64 GetMemAddress(string v)
        {
            v = v.Replace("mem[", "");
            v = v.Replace("]", "");
            return UInt64.Parse(v);
        }

        private static IEnumerable<UInt64> GetFloatingMemAddresses(string v, char[] mask)
        {
            v = v.Replace("mem[", "");
            v = v.Replace("]", "");
            UInt32 baseAddresInt = UInt32.Parse(v);
            var baseAddress = Convert.ToString(baseAddresInt, 2);
            baseAddress = baseAddress.PadLeft(64, '0');
            var baseAddressBits = baseAddress.ToCharArray();
            
            for (var i = 0; i < mask.Length; i++)
            {
                if (mask[i] == '1') baseAddressBits[i] = '1';
            }
            var addresses = new List<UInt64>();
            FindAddresses(baseAddressBits, mask, 0, addresses);
            return addresses;
        }

        private static void FindAddresses(char[] baseAddressBits, char[] mask, int index, List<ulong> addresses)
        {
            if (index >= 64)
            {
                var masked = new String(baseAddressBits);
                addresses.Add(Convert.ToUInt64(masked, 2));
                return;
            }
            if (mask[index] == 'X')
            {
                char[] oneCopy = (char[])baseAddressBits.Clone();
                oneCopy[index] = '1';
                char[] zeroCopy = (char[])baseAddressBits.Clone();
                zeroCopy[index] = '0';
                FindAddresses(oneCopy, mask, index + 1, addresses);
                FindAddresses(zeroCopy, mask, index + 1, addresses);
            }
            else
            {
                FindAddresses(baseAddressBits, mask, index + 1, addresses);
            }

        }
    }
}
