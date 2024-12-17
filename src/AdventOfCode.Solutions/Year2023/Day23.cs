//using Edge = (AdventOfCode.Solutions.Point point, int cost);
using Edges = System.Collections.Generic.Dictionary<AdventOfCode.Solutions.Point<int>, System.Collections.Generic.HashSet<AdventOfCode.Solutions.Year2023.Day23.Edge>>;
namespace AdventOfCode.Solutions.Year2023;

[Description("A Long Walk")]
public class Day23 : IPuzzle
{
    public object Part1(string input)
    {
        var edges = ParseInput(input);

        return CalculateLongestPath(edges);
    }

    public object Part2(string input)
    {
        var edges = ParseInput(input, true);

        return CalculateLongestPath(edges);
    }

    private static int CalculateLongestPath(Edges edges)
    {
        var list = edges.Where(kvp => kvp.Value.Count == 1)
            .OrderBy(kvp => kvp.Key.X)
            .ThenBy(kvp => kvp.Key.Y)
            .ToArray();

        var start = list[0].Key;
        var end = list[^1].Key;

        Stack<Edge> q = new();
        q.Push(new Edge(start, 0));
        HashSet<Point> visited = [];

        int best = 0;
        while (q.TryPop(out var current)) {
            if (current.cost == -1) {
                visited.Remove(current.point);
                continue;
            }

            if (current.point == end) {
                best = Math.Max(best, current.cost);
                continue;
            }

            if (!visited.Add(current.point)) {
                continue;
            }

            q.Push(current with { cost = -1 });
            foreach ((Point next, int cost) in edges[current.point])
            {
                q.Push(new Edge(next, cost + current.cost));
            }
        }

        return best;
    }

    private static Edges ParseInput(string input, bool ignoreSlopes = false)
    {
        Direction[] directions = [(0, -1), (-1, 0)];

        Edges edges = new();
        var points = input
            .ToLines()
            .SelectMany((s, y) =>
                s.Select(((c, x) => (c, new Point(x, y)))))
            .Where(t => t.c != '#');

        var queue = new Queue<(char c, Point p)>();

        foreach ((char c, Point p) current in points)
        {
            HashSet<Edge> targets = [];

            foreach (Direction direction in directions)
            {
                Point next = current.p + direction;
                if (!edges.ContainsKey(next))
                {
                    continue;
                }

                targets.Add(new Edge(next, 1));
                edges[next].Add(new Edge(current.p, 1));

                if (ignoreSlopes || current.c == '.')
                {
                    continue;
                }

                queue.Enqueue(current);
            }

            edges[current.p] = targets;
        }

        while(queue.TryDequeue(out var current))
        {
            edges[current.p] = current.c switch
            {
                '>' => [new Edge(current.p + Direction.East, 1)],
                '<' => [new Edge(current.p + Direction.West, 1)],
                '^' => [new Edge(current.p + Direction.North, 1)],
                'v' => [new Edge(current.p + Direction.South, 1)],
                _ => throw new ArgumentOutOfRangeException(nameof(current.c))
            };
        }

        CollapseEdges(edges);

        return edges;
    }

    private static void CollapseEdges(Edges edges)
    {
        var remove = new List<Point>();
        foreach (KeyValuePair<Point, HashSet<Edge>> kvp in edges)
        {
            if (kvp.Value.Count != 2)
            {
                continue;
            }

            Edge first = kvp.Value.First();
            Edge last = kvp.Value.Last();
            int costDelta = first.cost + last.cost;

            remove.Add(kvp.Key);

            edges[first.point].Remove(first with { point = kvp.Key });
            edges[last.point].Remove(last with { point = kvp.Key });

            edges[first.point].Add(last with { cost = costDelta });
            edges[last.point].Add(first with { cost = costDelta });
        }

        foreach (Point point in remove)
        {
            edges.Remove(point);
        }
    }

    public readonly record struct Edge(Point<int> point, int cost);
}