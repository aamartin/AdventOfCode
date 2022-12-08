
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;


namespace AoC_17
{
    class Program
    {
        static int maxdim = 30;
        
        static void Main(string[] args)
        {

            var lines = File.ReadAllLines("input.txt");
            bool[,,] pocketSpace = new bool[maxdim, maxdim, maxdim];
            bool[,,,] pocketSpace4d = new bool[maxdim, maxdim, maxdim, maxdim];
            var midSlice = (int)Math.Floor((double)maxdim/2);
            int y = 0;
            foreach(var line in lines)
            {
                var parts = line.ToCharArray();
                for (int x = 0; x < parts.Length; x++)
                {
                    if (parts[x] == '#') pocketSpace[midSlice + x-parts.Length, midSlice + y - parts.Length, midSlice] = true;
                    if (parts[x] == '#') pocketSpace4d[midSlice + x - parts.Length, midSlice + y - parts.Length, midSlice, midSlice] = true;
                }
                y++;
            }
            
            for(int i = 0; i< 6; i++)
            {
                pocketSpace = Simulate(pocketSpace);
                pocketSpace4d = Simulate4D(pocketSpace4d);
            }
            
            int total = 0;
            for(int dx =0; dx < maxdim; dx++)
            {
                for(int dy = 0; dy < maxdim; dy++)
                {
                    for(int dz =0; dz < maxdim; dz++)
                    {
                        if (pocketSpace[dx, dy, dz])
                        {
                            total++;
                        }
                    }
                }
            }

            int totalPart2 = 0;
            for (int dx = 0; dx < maxdim; dx++)
            {
                for (int dy = 0; dy < maxdim; dy++)
                {
                    for (int dz = 0; dz < maxdim; dz++)
                    {
                        for (int dw = 0; dw < maxdim; dw++)
                        {
                            if (pocketSpace4d[dx, dy, dz, dw])
                            {
                                totalPart2++;
                            }
                        }
                    }
                }
            }

            Console.WriteLine($"Part 1: {total}");
            Console.WriteLine($"Part 2: {totalPart2}");

        }

        private static bool[,,] Simulate(bool[,,] pocketSpace)
        {
            var newSpace = new bool[maxdim, maxdim, maxdim];
            for(int x = 1; x< maxdim-2; x++)
            {
                for(int y = 1; y < maxdim-2; y++)
                {
                    for(int z = 1; z < maxdim-2; z++)
                    {
                        var activeNeighboors = CountNeighboors(pocketSpace, x, y, z);
                        if(pocketSpace[x,y,z] && activeNeighboors >= 2 && activeNeighboors <= 3 )
                        {
                            newSpace[x, y, z] = true;
                        }
                        else if(!pocketSpace[x,y,z] && activeNeighboors == 3)
                        {
                            newSpace[x, y, z] = true;
                        }
                        else { newSpace[x, y, z] = false; }
                    }
                }
            }
            return newSpace;
        }

        private static bool[,,,] Simulate4D(bool[,,,] pocketSpace)
        {
            var newSpace = new bool[maxdim, maxdim, maxdim ,maxdim];
            for (int x = 1; x < maxdim - 2; x++)
            {
                for (int y = 1; y < maxdim - 2; y++)
                {
                    for (int z = 1; z < maxdim - 2; z++)
                    {
                        for (int w = 1; w < maxdim - 2; w++)
                        {
                            var activeNeighboors = Count4dNeighboors(pocketSpace, x, y, z, w);
                            if (pocketSpace[x, y, z, w] && activeNeighboors >= 2 && activeNeighboors <= 3)
                            {
                                newSpace[x, y, z, w] = true;
                            }
                            else if (!pocketSpace[x, y, z, w] && activeNeighboors == 3)
                            {
                                newSpace[x, y, z, w] = true;
                            }
                            else { newSpace[x, y, z, w] = false; }
                        }
                    }
                }
            }
            return newSpace;
        }

        private static int CountNeighboors(bool[,,] pocketSpace, int x, int y, int z)
        {

            var activeNeigboors = 0;
            foreach (var dx in Enumerable.Range(-1, 3))
                foreach (var dy in Enumerable.Range(-1, 3))
                    foreach (var dz in Enumerable.Range(-1, 3))
                    {
                        if (dx == 0 && dy == 0&& dz == 0)
                            continue;
                        if (pocketSpace[dx + x, dy + y, dz + z]) activeNeigboors++;
                    }

            return activeNeigboors;
        }

        private static int Count4dNeighboors(bool[,,,] pocketSpace, int x, int y, int z, int w)
        {
            var activeNeigboors = 0;
            foreach (var dx in Enumerable.Range(-1, 3))
                foreach (var dy in Enumerable.Range(-1, 3))
                    foreach (var dz in Enumerable.Range(-1, 3))
                        foreach (var dw in Enumerable.Range(-1, 3))
                        {
                        if (dx == 0 && dy == 0 && dz == 0 && dw == 0)
                            continue;
                        if (pocketSpace[dx + x, dy + y, dz + z, dw + w]) activeNeigboors++;
                    }

            return activeNeigboors;
        }

        private static void PrintSlice(bool[,,] pocketSpace, int z)
        {
            Console.WriteLine(" 012345678901234567890123456789" );
            for (int y = 0; y < maxdim; y++)
            {
                Console.Write(y);
                for(int x = 0; x < maxdim; x++)
                {
                    if (pocketSpace[x, y, z]) Console.Write("#");
                    else { Console.Write("."); }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

    }
}
