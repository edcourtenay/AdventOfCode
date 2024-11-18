namespace AdventOfCode.Solutions.Year2022;

[Description("No Space Left On Device")]
public class Day07 : IPuzzle
{
    public object Part1(string input)
    {
        Directory root = Parse(input.ToLines());
        return Enumerable.Concat(root.Directories.Flatten(directory => directory.Directories), [root])
            .Select(directory => directory.Size())
            .Where(size => size <= 100000)
            .Sum();
    }

    public object Part2(string input)
    {
        const int totalDiskSize = 70000000;

        Directory root = Parse(input.ToLines());
        int freeSpace = totalDiskSize - root.Size();

        return Enumerable.Concat(root.Directories.Flatten(directory => directory.Directories), [root])
            .Select(directory => directory.Size())
            .Where(size => freeSpace + size >= 30000000)
            .Min();
    }

    private static Directory Parse(IEnumerable<string> lines)
    {
        var root = new Directory { Name = "/", Parent = null };
        var blocks = ParseBlocks(lines);

        _ = blocks.Aggregate(root, (current, block) => ProcessBlock(block, current)!);
        return root;
    }

    private static Directory? ProcessBlock(IEnumerable<string> block, Directory current)
    {
        using IEnumerator<string> enumerator = block.GetEnumerator();

        if (!enumerator.MoveNext())
        {
            return current;
        }

        switch (enumerator.Current)
        {
            case { } s when s.StartsWith("$ cd"):
                return current.ChangeDirectory(s[5..]);
            case { } s when s.StartsWith("$ ls"):
                while (enumerator.MoveNext())
                {
                    string[] a = enumerator.Current.Split(' ');
                    if (a[0] == "dir")
                    {
                        current.GetOrAddDirectory(a[1]);
                    }
                    else
                    {
                        current.Files.Add(new File { Size = int.Parse(a[0]), Name = a[1] });
                    }
                }
                break;
        }

        return current;
    }

    private static IEnumerable<IEnumerable<string>> ParseBlocks(IEnumerable<string> lines)
    {
        List<Queue<string>> blocks = [];
        Queue<string> current = new();

        foreach (string line in lines)
        {
            if (line.StartsWith('$'))
            {
                current = new Queue<string>();
                blocks.Add(current);
            }

            current.Enqueue(line);
        }

        return blocks;
    }

    public record Directory
    {
        public required string Name { get; init; }
        public required Directory? Parent { get; init; }
        public HashSet<File> Files { get; } = [];
        public HashSet<Directory> Directories { get; } = [];

        public int Size()
        {
            return Enumerable.Concat(Directories
                    .Flatten(d => d.Directories), [this])
                .SelectMany(d => d.Files)
                .Sum(file => file.Size);
        }

        public Directory GetOrAddDirectory(string name)
        {
            Directory? firstOrDefault = Directories.FirstOrDefault(directory => directory.Name == name);
            switch (firstOrDefault)
            {
                case null:
                    {
                        Directory newChild = new() { Parent = this, Name = name };
                        Directories.Add(newChild);
                        return newChild;
                    }
                default:
                    return firstOrDefault;
            }
        }

        public Directory? ChangeDirectory(string name)
        {
            switch (name)
            {
                case "/":
                    {
                        var p = this;
                        while (p.Parent != null)
                        {
                            p = p.Parent;
                        }

                        return p;
                    }
                case "..":
                    return Parent;
                default:
                    return Directories.FirstOrDefault(directory => directory.Name == name);
            }
        }
    }

    public record File
    {
        public required string Name { get; init; }
        public required int Size { get; init; }
    }
}