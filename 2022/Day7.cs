using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AoC2022
{
    class Day7
    {
        internal static void Execute(string file)
        {
            Console.WriteLine("Day 7");
            Console.WriteLine($"Part1: {Part1(file)}");
            Console.WriteLine($"Part2: {Part2(file)}");
            Console.WriteLine();
        }

        private static int Part2(string file)
        {
            var root = PopulateTree(file);
            List<INode> dirs = new List<INode>();
            var unusedSpace = 70000000 - root.Size();
            var requiredCleanup = 30000000 - unusedSpace;
            FindLarger(root, dirs, requiredCleanup);

            int smallest = int.MaxValue;
            foreach(var dir in dirs)
            {
                if(dir.Size() < smallest)
                {
                    smallest = dir.Size();
                }
            }

            return smallest;
        }

        private static INode PopulateTree(string file)
        {
            Stack<string> currentPath = new Stack<string>();
            INode root = new INode("/", null);
            INode currentDir = root;
            foreach (var line in File.ReadAllLines(file))
            {
                if (line.StartsWith('$'))
                {
                    if (line.StartsWith("$ cd"))
                    {
                        var newDir = line.Substring(5);
                        switch (newDir)
                        {
                            case "..":
                                currentPath.Pop();
                                currentDir = currentDir.Parent;
                                break;
                            case "/":
                                currentDir = root;
                                currentPath.Clear();
                                break;
                            default:
                                currentPath.Push(newDir);
                                currentDir = currentDir.NavigateTo(newDir);
                                break;
                        }
                    }
                }
                else
                {
                    if (line.StartsWith("dir"))
                    {
                        var newdir = line.Replace("dir ", "");
                        currentDir.AddDir(newdir);
                    }
                    else
                    {
                        var parts = line.Split(" ");
                        currentDir.AddFile(parts[1], int.Parse(parts[0]));
                    }
                }
            }

            return root;

        }

        private static int Part1(string file)
        {
            var root = PopulateTree(file);

            int total = 0;
            List<INode> dirs = new List<INode>();
            FindSmaller(root, dirs, 100000);
            foreach (var dir in dirs)
            {
                total += dir.Size();
            }

            return total;
        }

        private static void FindSmaller(INode root, List<INode> inodes, int size)
        {
            if (root.Size() < size) inodes.Add(root);
            foreach(var dir in root.Directories)
            {
                FindSmaller(dir, inodes, size);
            }
        }

        private static void FindLarger(INode root, List<INode> inodes, int size)
        {
            if (root.Size() >= size) inodes.Add(root);
            foreach (var dir in root.Directories)
            {
                FindLarger(dir, inodes, size);
            }
        }
    }

    class INode
    {
        public INode(string name, INode parent)
        {
            Name = name;
            Directories = new List<INode>();
            Files = new List<AocFile>();
            Parent = parent;
        }
        string Name { get; set; }
        public List<INode> Directories { get; set; }
        List<AocFile> Files { get; set; }   
        public INode Parent { get; set; }

        internal INode NavigateTo(string newDir)
        {
            var dir = Directories.Find(a => String.Equals(a.Name, newDir));
            if(dir == null)
            {
                dir = new INode(newDir, this);
                Directories.Add(dir);
            }
            return dir;
        }

        internal void AddDir(string newdir)
        {
            Directories.Add(new INode(newdir, this));
        }

        internal void AddFile(string name, int size)
        {
            Files.Add(new AocFile(name, size));
        }

        internal int Size()
        {
            var total = 0;
            foreach(var file in Files) { total += file.Size; }
            foreach(var dir in Directories) { total += dir.Size(); }
            return total;
        }

        public override string ToString()
        {
            return $"{Size()} {Name}";
        }
    }

    class AocFile
    {
        public AocFile(string name, int size)
        {
            Name = name;
            this.Size = size;
        }

        string Name { get; set; }
        public int Size { get; set; }

        public override string ToString()
        {
            return $"{Size} {Name}";
        }
    } 
}
