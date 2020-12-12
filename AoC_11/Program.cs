using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_11
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            var columns = lines[0].Length;
            var rows = lines.Length;
            var seatmap = new SeatMap(rows, columns);

            for(int row = 0; row < rows; row++)
            {
                var columnParts = lines[row].ToCharArray();
                for(int column = 0; column < columns; column++)
                {
                    char item = columnParts[column];
                    switch(item)
                    {
                        case 'L':
                            seatmap.SetSeat(row, column);
                            break;
                        case '.':
                            seatmap.SetFloor(row, column);
                            break;
                        case '#':
                            seatmap.SitDown(row, column);
                            break;
                    }
                }
            }
            var part1SeatMap = seatmap.Copy();
            part1SeatMap.Model();
            Console.WriteLine(part1SeatMap.CountOccupied());
            seatmap.Model(5, true);
            Console.WriteLine(seatmap.CountOccupied());


        }
    }

    class SeatMap
    {
        char[,] seatmap;
        int rows, columns;
        public SeatMap(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
            seatmap = new char[rows, columns];
        }
        
        public void SetSeat(int row, int column)
        {
            seatmap[row, column] = 'L';
        }

        public void SetFloor(int row, int column)
        {
            seatmap[row, column] = '.';
        }

        public void SitDown(int row, int column)
        {
            seatmap[row, column] = '#';
        }

        internal void Model(int threshold = 4, bool distant = false)
        {
            var cnt = GetDistantAdjacentCount(0, 2);
            var its = 0;
            SeatMap previous = new SeatMap(rows, columns);
            do
            {
                previous = Copy();
                for (int r = 0; r < rows; r++)
                {
                    for (int c = 0; c < columns; c++)
                    {
                        var adjCnt = 0;
                        if (!distant) { adjCnt = previous.GetAdjacentCount(r, c); }
                        else { adjCnt = previous.GetDistantAdjacentCount(r, c); }
                        if (previous.seatmap[r, c] == 'L' && adjCnt == 0) SitDown(r, c);
                        else if (previous.seatmap[r, c] == '#' && adjCnt >= threshold) SetSeat(r, c);
                    }
                }
                //Print();
                its++;
            }
            while (!SeatMap.Equals(previous, this));
        }

        private void Print()
        {
            for(int i = 0; i < rows; i++)
            {
                for(int j =0; j < columns; j++)
                {
                    Console.Write(seatmap[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private int GetAdjacentCount(int r, int c)
        {
            var adj = 0;
            if (CheckAdj(r - 1, c - 1)) adj++;
            if (CheckAdj(r - 1, c)) adj++;
            if (CheckAdj(r - 1, c + 1)) adj++;
            if (CheckAdj(r, c - 1)) adj++;
            if (CheckAdj(r, c + 1)) adj++;
            if (CheckAdj(r + 1, c - 1)) adj++;
            if (CheckAdj(r + 1, c)) adj++;
            if (CheckAdj(r + 1, c + 1)) adj++;
            return adj;
        }

        private int GetDistantAdjacentCount(int r, int c)
        {
            var adj = 0;
            for (int x = r-1, y = c-1; x >= 0 && y >= 0 && seatmap[x, y] != 'L'; x--, y--) if (CheckAdj(x, y)) { adj++; break; } // up left
            for (int x = r+1, y = c-1; x < rows && y >= 0 && seatmap[x, y] != 'L'; x++, y--) if (CheckAdj(x, y)) { adj++; break; }// down left
            for (int x = r+1, y = c+1; x < rows && y < columns && seatmap[x, y] != 'L'; x++, y++) if (CheckAdj(x, y)) { adj++; break; }  // down right
            for (int x = r-1, y = c+1; x >= 0 && y < columns && seatmap[x, y] != 'L'; x--, y++) if (CheckAdj(x, y)) { adj++; break; }// up right
            for (int x = r-1; x >= 0 && seatmap[x, c] != 'L'; x--) if (CheckAdj(x, c)) { adj++; break; }// up
            for (int x = r+1; x < rows && seatmap[x, c] != 'L'; x++)  if (CheckAdj(x, c)) { adj++; break; }// down
            for (int y = c-1; y >= 0 && seatmap[r, y] != 'L'; y--) if (CheckAdj(r, y)) { adj++; break; }// down
            for (int y = c+1; y < columns && seatmap[r, y] != 'L'; y++) if (CheckAdj(r, y)) { adj++; break; }// right

            return adj;
        }

        private bool CheckAdj(int r, int c)
        {
            if (r >= 0 && r < rows && c >= 0 && c < columns)
                return Char.Equals(seatmap[r, c], '#');
            return false;
        }

        public override bool Equals(object obj)
        {
            var nSeatMap = obj as SeatMap;        
            for (int row = 0; row < rows; row++)
            {
                for (int c = 0; c < columns; c++)
                {
                    //Console.WriteLine($"{nSeatMap.seatmap[row, c]}, {seatmap[row, c]}");
                    //Console.WriteLine();
                    if(!Char.Equals(nSeatMap.seatmap[row, c], seatmap[row, c]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        
        public SeatMap Copy()
        {
            var nSeatMap = new SeatMap(rows, columns);
            for(int row = 0; row < rows; row++ )
            {
                for(int c = 0; c < columns; c++)
                {
                    nSeatMap.seatmap[row, c] = seatmap[row, c];
                }
            }
            return nSeatMap;
        }

        internal int CountOccupied()
        {
            int cnt = 0;
            foreach (var seat in seatmap)
            {

                if (seat == '#') cnt++;
            }
            return cnt;
        }
    }
}
