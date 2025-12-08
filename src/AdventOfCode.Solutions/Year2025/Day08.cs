namespace AdventOfCode.Solutions.Year2025;

[Description("Playground")]
public class Day08 : IPuzzle
{
    public object Part1(string input)
    {
        var junctionBoxes = ParseInput(input);
        var unionFind = new UnionFind(junctionBoxes.Count);
        var edges = BuildEdges(junctionBoxes);

        for (var i = 0; i < Part1Iterations && i < edges.Length; i++)
        {
            var (fromIdx, toIdx, _) = edges[i];
            unionFind.Union(fromIdx, toIdx);
        }

        var componentSizes = unionFind.GetComponentSizes();
        var topThree = componentSizes.OrderByDescending(x => x).Take(3).ToArray();

        return topThree[0] * topThree[1] * topThree[2];
    }

    public object Part2(string input)
    {
        var junctionBoxes = ParseInput(input);
        var unionFind = new UnionFind(junctionBoxes.Count);
        var edges = BuildEdges(junctionBoxes);

        foreach ((int fromIdx, int toIdx, _) in edges)
        {
            unionFind.Union(fromIdx, toIdx);

            // Check if all components have size > 1 (no singletons left)
            if (unionFind.SingletonCount == 0)
            {
                var fromBox = junctionBoxes[fromIdx];
                var toBox = junctionBoxes[toIdx];
                return fromBox.X * toBox.X;
            }
        }

        return 0;
    }

    private List<JunctionBox> ParseInput(string input)
    {
        return input.ToLines()
            .Select(line => line.Split(','))
            .Select(parts => new JunctionBox(long.Parse(parts[0]), long.Parse(parts[1]), long.Parse(parts[2])))
            .ToList();
    }

    private (int From, int To, long Distance)[] BuildEdges(List<JunctionBox> junctionBoxes)
    {
        var edges = new List<(int From, int To, long Distance)>(junctionBoxes.Count * (junctionBoxes.Count - 1) / 2);

        for (var i = 0; i < junctionBoxes.Count - 1; i++)
        {
            var boxA = junctionBoxes[i];

            for (var j = i + 1; j < junctionBoxes.Count; j++)
            {
                var boxB = junctionBoxes[j];
                edges.Add((i, j, boxA.SquaredDistanceTo(boxB)));
            }
        }

        return edges.OrderBy(e => e.Distance).ToArray();
    }

    public int Part1Iterations { get; set; } = 1000;

    readonly record struct JunctionBox(long X, long Y, long Z)
    {
        public long SquaredDistanceTo(JunctionBox other)
        {
            var dx = X - other.X;
            var dy = Y - other.Y;
            var dz = Z - other.Z;
            return dx * dx + dy * dy + dz * dz;
        }
    }

    private class UnionFind
    {
        private readonly int[] _parent;
        private readonly int[] _size;
        private int _singletonCount;

        public int ComponentCount { get; private set; }
        public int TotalNodes { get; }

        public int SingletonCount => _singletonCount;

        public UnionFind(int n)
        {
            TotalNodes = n;
            ComponentCount = n;
            _singletonCount = n;
            _parent = new int[n];
            _size = new int[n];

            for (var i = 0; i < n; i++)
            {
                _parent[i] = i;
                _size[i] = 1;
            }
        }

        public int Find(int x)
        {
            if (_parent[x] != x)
            {
                _parent[x] = Find(_parent[x]); // Path compression
            }
            return _parent[x];
        }

        public bool Union(int x, int y)
        {
            var rootX = Find(x);
            var rootY = Find(y);

            if (rootX == rootY)
            {
                return false; // Already in same set
            }

            // Union by size
            if (_size[rootX] < _size[rootY])
            {
                (rootX, rootY) = (rootY, rootX);
            }

            // Track singleton count
            if (_size[rootX] == 1) _singletonCount--;
            if (_size[rootY] == 1) _singletonCount--;

            _parent[rootY] = rootX;
            _size[rootX] += _size[rootY];
            ComponentCount--;

            return true;
        }

        public List<int> GetComponentSizes()
        {
            var sizes = new Dictionary<int, int>();

            for (var i = 0; i < TotalNodes; i++)
            {
                var root = Find(i);
                if (!sizes.ContainsKey(root))
                {
                    sizes[root] = _size[root];
                }
            }

            return sizes.Values.ToList();
        }
    }
}