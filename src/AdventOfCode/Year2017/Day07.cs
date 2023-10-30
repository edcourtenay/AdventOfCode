using System.Text.RegularExpressions;

namespace AdventOfCode.Year2017;

[Description("Recursive Circus")]
public partial class Day07 : IPuzzle
{
    public object Part1(string input)
    {
        return ParseInput(input).Name;
    }

    public object Part2(string input)
    {
        return ParseInput(input).FindImbalance();
    }

    private static Node ParseInput(string input)
    {
        var nodesByName = new Dictionary<string, Node>();
        var parentChildPairs = new HashSet<ParentChild>();

        foreach (string line in input.ToLines())
        {
            var match = MyRegex().Match(line);
            if (!match.Success)
            {
                throw new Exception($"Failed to parse line: {line}");
            }

            var name = match.Groups["name"].Value;
            var weight = int.Parse(match.Groups["weight"].Value);
            var children = match.Groups["child"].Captures.Select(c => c.Value).ToArray();

            var node = new Node
            {
                Name = name,
                Weight = weight
            };

            nodesByName.Add(name, node);

            foreach (string child in children)
            {
                parentChildPairs.Add(new ParentChild
                {
                    Parent = node,
                    Child = child
                });
            }
        }

        foreach ((Node parent, string childName) in parentChildPairs)
        {
            var node = nodesByName[childName];
            parent.Children.Add(node);
            nodesByName[childName] = node with { Parent = parent };
        }

        return nodesByName.Values
            .First(n => n.Parent == null);
    }

    [GeneratedRegex(@"(?<name>\w+)\s\((?<weight>\d+)\)(\s->\s(?n:(?<child>\w+)(?n:,\s)?)*)?$")]
    private static partial Regex MyRegex();

    public record Node
    {
        public required string Name { get; init; }
        public required int Weight { get; init; }
        public Node? Parent { get; init; } = null;
        public List<Node> Children { get; init; } = new();
        public int TotalWeight => Weight + Children.Sum(c => c.TotalWeight);
        public bool IsBalanced => Children.Select(c => c.TotalWeight).Distinct().Count() == 1;

        public int FindImbalance(int? imbalance = null)
        {
            if (imbalance is not null && IsBalanced)
            {
                return Weight - imbalance.Value;
            }

            var subtreesByWeight = Children.GroupBy(c => c.TotalWeight).ToArray();
            var badTree = subtreesByWeight.First(g => g.Count() == 1).First();
            return badTree.FindImbalance(imbalance ?? Math.Abs(subtreesByWeight.Select(s => s.Key).Aggregate((a, b) => a - b)));
        }
    }

    public record ParentChild
    {
        public void Deconstruct(out Node parent, out string child)
        {
            parent = Parent;
            child = Child;
        }

        public required Node Parent { get; init; }
        public required string Child { get; init; }
    }
}