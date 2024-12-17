namespace AdventOfCode.Solutions.Year2023;

[Description("Clumsy Crucible")]
public class Day17 : IPuzzle
{
    public object Part1(string input)
    {
        var grid = ToGrid(input);

        return Solve(grid, GetDirections, (target, path) => path.Point == target);
    }

    public object Part2(string input)
    {
        var grid = ToGrid(input);

        return Solve(grid, GetDirections2, (target, path) => path.Point == target && path.Length > 3);
    }

    private static int Solve(Dictionary<Point, int> grid, Func<Direction, int, IEnumerable<Direction>> directionsFunc,
        Func<Point, Path, bool> isTargetFunc)
    {
        Dictionary<Path, int> distances = new();

        var queue = new PriorityQueue<Path, int>();

        var startEast = new Path((0, 0), (0, 1), 0);
        var startSouth = new Path((0, 0), (1, 0), 0);

        int maxX = grid.Keys.Max(p => p.X);
        int maxY = grid.Keys.Max(p => p.Y);

        Point target = (maxX, maxY);

        distances[startEast] = 0;
        distances[startSouth] = 0;
        queue.Enqueue(startEast, 0);
        queue.Enqueue(startSouth, 0);

        while (queue.TryDequeue(out var path, out var cost))
        {
            if (isTargetFunc(target, path))
            {
                return cost;
            }

            foreach (var direction in directionsFunc(path.Direction, path.Length))
            {
                Point next = (path.Point.X + direction.X, path.Point.Y + direction.Y);
                if (next.X < 0 || next.X > maxX || next.Y < 0 || next.Y > maxY)
                {
                    continue;
                }

                Path nextPath = new (next, direction, direction == path.Direction ? path.Length + 1 : 1);
                int nextCost = distances[path] + grid[next];

                if (nextCost >= distances.GetValueOrDefault(nextPath, int.MaxValue))
                {
                    continue;
                }

                distances[nextPath] = nextCost;
                queue.Enqueue(nextPath, nextCost + Point.ManhattanDistance(next, target));
            }
        }

        return int.MaxValue;
    }

    private static IEnumerable<Direction> GetDirections(Direction direction, int length)
    {
        if (length < 3)
        {
            yield return direction;
        }

        yield return direction.RotateLeft;
        yield return direction.RotateRight;
    }

    private static IEnumerable<Direction> GetDirections2(Direction direction, int length)
    {
        switch (length)
        {
            case < 4:
                yield return direction;
                break;
            default:
                if (length < 10)
                {
                    yield return direction;
                }

                yield return direction.RotateLeft;
                yield return direction.RotateRight;
                break;
        }
    }

    private static Dictionary<Point, int> ToGrid(string input)
    {
        var grid = new Dictionary<Point, int>();

        foreach ((string data, int y) in input.ToLines().Select<string, (string data, int y)>((s, i) => (data: s, y: i )))
        {
            foreach ((char ch, int x) in data.Select((c, i) => (c, x: i)))
            {
                grid[(x, y)] = ch - '0';
            }
        }

        return grid;
    }

    private readonly record struct Path(Point Point, Direction Direction, int Length);
}