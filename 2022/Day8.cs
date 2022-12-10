using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AoC2022
{
    class Day8
    {
        internal static void Execute(string file)
        {
            Console.WriteLine("Day 8");
            Console.WriteLine($"Part1: {Part1(file)}");
            Console.WriteLine($"Part2: {Part2(file)}");
            Console.WriteLine();
        }

        private static int Part2(string file)
        {
            int width = 0;
            var elements = PopulateMap(file, out width);
            int highscore = 0;
            for(int i =0; i< elements.Count; i++)
            {
                for(int j = 0; j < width; j++)
                {
                    int score = CalcScenicScore(elements, width, i, j);
                    if (score > highscore) { highscore = score; }
                }
            }
            return highscore;
        }

        private static int CalcScenicScore(List<List<int>> elements, int width, int row, int col)
        {
            var curTree = elements[row][col];
            // look up
            var tempRow = row;
            var treesUp = 0;
            while(tempRow > 0)
            {
                tempRow--;
                treesUp++;
                if (elements[tempRow][col] >= curTree) break;
            }

            //look down
            tempRow = row;
            var treesDown = 0;
            while (tempRow < elements.Count-1)
            {
                tempRow++;
                treesDown++;
                if (elements[tempRow][col] >= curTree) break;
            }

            // look left
            int tempCol = col;
            var treesLeft = 0;
            while (tempCol > 0)
            {
                tempCol--;
                treesLeft++;
                if (elements[row][tempCol] >= curTree) break;
            }

            // look right
            tempCol = col;
            var treesRight = 0;
            while (tempCol < width-1)
            {
                tempCol++;
                treesRight++;
                if (elements[row][tempCol] >= curTree) break;
            }

            return treesDown * treesLeft * treesRight * treesUp;
        }

        private static List<List<int>> PopulateMap(string file, out int width )
        {
            List<List<int>> elements = new List<List<int>>();
            width = 0;
            foreach (var line in File.ReadAllLines(file))
            {
                var parts = line.ToCharArray();
                width = parts.Length;
                var lineArry = new List<int>();
                foreach (var part in parts)
                {
                    lineArry.Add(int.Parse(part.ToString()));
                }
                elements.Add(lineArry);
            }
            return elements;
        }

        private static int Part1(string file)
        {
            int width = 0;
            var elements = PopulateMap(file, out width);

            HashSet<string> visable1 = new HashSet<string>();
            List<List<Tree>> leftToRight = new List<List<Tree>>();
            List<List<Tree>> rightToLeft = new List<List<Tree>>();
            List<List<Tree>> topToBottom = new List<List<Tree>>();
            List<List<Tree>> bottomToTop = new List<List<Tree>>();

            for (int i = 0; i < elements.Count; i++)
            {
                List<Tree> row = new List<Tree>();
                for (int j = 0; j < width; j++)
                {
                    row.Add(new Tree(i, j, elements[i][j]));
                }
                leftToRight.Add(row);
                var revRow = new List<Tree>(row);
                revRow.Reverse();
                rightToLeft.Add(revRow);
            }
            for (int i = 0; i < width; i++)
            {
                List<Tree> row = new List<Tree>();
                for (int j = 0; j < elements.Count; j++)
                {
                    row.Add(new Tree(j, i, elements[j][i]));
                }
                topToBottom.Add(row);
                var revRow = new List<Tree>(row);
                revRow.Reverse();
                bottomToTop.Add(revRow);
            }
            CalcHighest(visable1, leftToRight);
            CalcHighest(visable1, rightToLeft);
            CalcHighest(visable1, topToBottom);
            CalcHighest(visable1, bottomToTop);
            return visable1.Count;
        }

        private static void CalcHighest(HashSet<string> visable1, List<List<Tree>> trees)
        {
            foreach(var treeList in trees)
            {
                int highest = int.MinValue;
                foreach( var tree in treeList)
                {
                    if (tree.height > highest)
                    {
                        if(!visable1.Contains(tree.Id)) visable1.Add(tree.Id);
                        highest = tree.height;
                    }
                }
            }
        }

        class Tree 
        {
            public Tree(int row, int col, int height)
            {
                this.row = row; this.col = col; this.height = height;
            }
            public string Id {  get { return $"{row}.{col}.{height}"; } }
            public int row { get; set; }
            public int col { get; set; }
            public int height { get; set; }
            public override string ToString()
            {
                return Id;
            }
        }
    }
}
