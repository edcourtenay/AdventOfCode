using System.Text;

namespace AdventOfCode.Year2017;

public class Day09 : IPuzzle
{
    public object Part1(string input)
    {
        Groups(input);

        return string.Empty;
    }

    public object Part2(string input) => string.Empty;

    public static Node Parse(string input)
    {
        var node = new Node { Parent = null, Depth = 1, Text = input };
        Parse(input, node);
        return node;
    }

    private static void Parse(string input, Node node)
    {
        if (input.Length < 2)
        {
            return;
        }

        var groups = Groups(input[1..^1]).ToArray();
        foreach (string group in groups)
        {
            var child = node.AddChild(group);
            Parse(group, child);
        }
    }

    public static IEnumerable<string> Groups(string input)
    {
        bool ignoreNext = false;
        bool inGarbage = false;
        int depth = 0;
        var sb = new StringBuilder(input.Length);

        foreach (var c in input)
        {
            switch (c)
            {
                case '!':
                    ignoreNext = true;
                    continue;
                case '<' when !ignoreNext:
                    inGarbage = true;
                    continue;
                case '>' when !ignoreNext:
                    inGarbage = false;
                    continue;
            }

            if (ignoreNext) { ignoreNext = false; continue; }

            if (inGarbage) continue;

            sb.Append(c);

            switch (c)
            {
                case '{':
                    depth++;
                    continue;
                case '}':
                    if (--depth == 0)
                    {
                        yield return sb.ToString();
                        sb.Clear();
                    }
                    continue;
            }
        }
    }

    public class Node
    {
        public Node? Parent { get; init; }
        public int Depth { get; init; }
        public List<Node> Children { get; } = new();
        public int Score { get => Children.Flatten(node => node.Children).Sum(node => node.Depth); }
        public string Text { get; set; }

        public Node AddChild(string s)
        {
            Node item = new Node
            {
                Parent = this,
                Text = s,
                Depth = Depth + 1
            };
            Children.Add(item);
            return item;
        }
    }
}