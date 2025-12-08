namespace AdventOfCode.Solutions.Year2025;

[Description("Playground")]
public class Day08 : IPuzzle
{
    public object Part1(string input)
    {
        var junctionBoxes = ParseInput(input);
        var unionFind = new UnionFind(junctionBoxes.Count);

        var edgeCount = 0;
        foreach (var (fromIdx, toIdx, _) in BuildEdges(junctionBoxes))
        {
            unionFind.Union(fromIdx, toIdx);

            if (++edgeCount >= Part1Iterations)
            {
                break;
            }
        }

        var componentSizes = unionFind.GetComponentSizes();
        var topThree = componentSizes.OrderByDescending(x => x).Take(3).ToArray();

        return topThree[0] * topThree[1] * topThree[2];
    }

    public object Part2(string input)
    {
        var junctionBoxes = ParseInput(input);
        var unionFind = new UnionFind(junctionBoxes.Count);

        foreach (var (fromIdx, toIdx, _) in BuildEdges(junctionBoxes))
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

    private IEnumerable<(int From, int To, long Distance)> BuildEdges(List<JunctionBox> junctionBoxes)
    {
        var pq = new PriorityQueue<(int From, int To), long>();

        for (var i = 0; i < junctionBoxes.Count - 1; i++)
        {
            var boxA = junctionBoxes[i];

            for (var j = i + 1; j < junctionBoxes.Count; j++)
            {
                var boxB = junctionBoxes[j];
                var distance = boxA.SquaredDistanceTo(boxB);
                pq.Enqueue((i, j), distance);
            }
        }

        while (pq.Count > 0)
        {
            var (from, to) = pq.Dequeue();
            var distance = junctionBoxes[from].SquaredDistanceTo(junctionBoxes[to]);
            yield return (from, to, distance);
        }
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
            // Iterative path compression with two-pass algorithm
            // First pass: find root
            var root = x;
            while (_parent[root] != root)
            {
                root = _parent[root];
            }

            // Second pass: compress path by pointing all nodes directly to root
            while (x != root)
            {
                var next = _parent[x];
                _parent[x] = root;
                x = next;
            }

            return root;
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