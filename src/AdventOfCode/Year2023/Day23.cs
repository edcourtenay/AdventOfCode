using Point = (int x, int y);
using Edge = ((int x, int y) point, int cost);
using Edges = System.Collections.Generic.Dictionary<(int x, int y), System.Collections.Generic.HashSet<((int x, int y), int cost)>>;
namespace AdventOfCode.Year2023;

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
            .OrderBy(kvp => kvp.Key.y)
            .ThenBy(kvp => kvp.Key.x)
            .ToArray();

        var start = list[0].Key;
        var end = list[^1].Key;

        Stack<Edge> q = new();
        q.Push((start, 0));
        HashSet<(int, int)> visited = [];

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

            q.Push((current.point, -1));
            foreach ((Point next, int cost) in edges[current.point])
            {
                q.Push((next, cost + current.cost));
            }
        }

        return best;
    }

    private static Edges ParseInput(string input, bool ignoreSlopes = false)
    {
        var directions = new[] { (0, -1), (-1, 0) };

        Edges edges = new();
        var points = input.ToLines().SelectMany((s, y) =>
                s.Select(((c, x) => (c, x, y))))
            .Where(t => t.c != '#');

        var queue = new Queue<(char c, int x, int y)>();

        foreach ((char c, int x, int y) current in points)
        {
            HashSet<Edge> targets = [];

            foreach (Point direction in directions)
            {
                Point next = (current.x + direction.x, current.y + direction.y);
                if (!edges.ContainsKey(next))
                {
                    continue;
                }

                targets.Add((next, 1));
                edges[next].Add(((current.x, current.y), 1));

                if (ignoreSlopes || current.c == '.')
                {
                    continue;
                }

                queue.Enqueue(current);
            }

            edges[(current.x, current.y)] = targets;
        }

        while(queue.TryDequeue(out var current))
        {
            edges[(current.x, current.y)] = current.c switch
            {
                '>' => [((current.x + 1, current.y), 1)],
                '<' => [((current.x - 1, current.y), 1)],
                '^' => [((current.x, current.y - 1), 1)],
                'v' => [((current.x, current.y + 1), 1)],
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        CollapseEdges(edges);

        return edges;
    }

    private static void CollapseEdges(Edges edges)
    {
        var remove = new List<Point>();
        foreach (KeyValuePair<Point, HashSet<(Point, int cost)>> kvp in edges)
        {
            if (kvp.Value.Count != 2)
            {
                continue;
            }

            Edge first = kvp.Value.First();
            Edge last = kvp.Value.Last();
            int costDelta = first.cost + last.cost;

            remove.Add(kvp.Key);

            edges[first.point].Remove((kvp.Key, first.cost));
            edges[last.point].Remove((kvp.Key, last.cost));

            edges[first.point].Add((last.point, costDelta));
            edges[last.point].Add((first.point, costDelta));
        }

        foreach (Point point in remove)
        {
            edges.Remove(point);
        }
    }
}