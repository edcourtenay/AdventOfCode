namespace AdventOfCode.Solutions.Year2019;

[Description("Universal Orbit Map")]
public class Day06 : IPuzzle
{
    public object Part1(string input)
    {
        var pairs = ParseInput(input);
        var root = BuildTree(pairs);
        var depths = GetNodeDepths(root);

        return depths.Values.Sum(x => x.Depth);
    }

    public object Part2(string input)
    {
        var pairs = ParseInput(input);
        var root = BuildTree(pairs);
        var depths = GetNodeDepths(root);

        var (youDepth, youNode) = depths["YOU"];
        var (sanDepth, sanNode) = depths["SAN"];

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        while (depths[youNode.Value].Depth > sanDepth)

        {
            youNode = youNode.Parent;
        }

        while (depths[sanNode.Value].Depth > youDepth)
        {
            sanNode = sanNode.Parent;
        }

        while (youNode.Value != sanNode.Value)
        {
            youNode = youNode.Parent;
            sanNode = sanNode.Parent;
        }

        var commonDepth = depths[youNode.Value].Depth;

        return (sanDepth - commonDepth) + (youDepth - commonDepth) - 2;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }

    private List<(string Parent, string Child)> ParseInput(string input)
    {
        return Parse().ToList();

        IEnumerable<(string Parent, string Child)> Parse()
        {
            var lines = input.ToLines();
            foreach (string line in lines)
            {
                if (line.Split(')') is [{ } parent, { } child])
                {
                    yield return (parent, child);
                }
            }
        }
    }

    public TreeNode<T>? BuildTree<T>(List<(T Parent, T Child)> pairs) where T : notnull
    {
        Dictionary<T, TreeNode<T>> nodeDict = new();
        foreach ((T Parent, T Child) pair in pairs)
        {
            if (!nodeDict.ContainsKey(pair.Parent))
            {
                nodeDict[pair.Parent] = new TreeNode<T>(pair.Parent);
            }

            if (!nodeDict.ContainsKey(pair.Child))
            {
                nodeDict[pair.Child] = new TreeNode<T>(pair.Child);
            }

            TreeNode<T> parentNode = nodeDict[pair.Parent];
            TreeNode<T> childNode = nodeDict[pair.Child];

            parentNode.Children.Add(nodeDict[pair.Child]);
            childNode.Parent = parentNode;
        }

        List<T> rootCandidates = nodeDict.Keys
            .Except(pairs.Select(p => p.Item2))
            .ToList();

        return rootCandidates.Count == 1 ? nodeDict[rootCandidates[0]] : null;
    }


    public Dictionary<T, (int Depth, TreeNode<T> Node)> GetNodeDepths<T>(TreeNode<T>? root) where T : notnull
    {
        Dictionary<T, (int Depth, TreeNode<T> Node)> depths = new();
        WalkTree(root, depths);
        return depths;
    }

    private void WalkTree<T>(TreeNode<T>? root, Dictionary<T, (int Depth, TreeNode<T> Node)> depths) where T : notnull
    {
        if (root is null)
        {
            return;
        }

        var stack = new Stack<(TreeNode<T> Node, int Depth)>();
        stack.Push((root, 0));
        while (stack.Count > 0)
        {
            (var node, int depth) = stack.Pop();
            depths[node.Value] = (depth, node);
            foreach (TreeNode<T> child in node.Children)
            {
                stack.Push((child, depth + 1));
            }
        }
    }

    public class TreeNode<T>
    {
        public List<TreeNode<T>> Children { get; }
        public T Value { get; }
        public TreeNode<T>? Parent { get; set; }

        public TreeNode(T value)
        {
            Value = value;
            Children = [];
        }
    }
}