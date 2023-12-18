using System.Numerics;

using Point = (int x, int y);
using Direction = (int dX, int dY);
using Path = ((int x, int y) point, (int dX, int dY) direction, int length);

namespace AdventOfCode.Year2023;

[Description("Clumsy Crucible")]
public class Day17 : IPuzzle
{
    public object Part1(string input)
    {
        var grid = ToGrid(input);

        return Solve(grid, GetDirections, (target, path) => path.point == target);
    }

    public object Part2(string input)
    {
        var grid = ToGrid(input);

        return Solve(grid, GetDirections2, (target, path) => path.point == target && path.length > 3);
    }

    private static int Solve(Dictionary<Point, int> grid, Func<Direction, int, IEnumerable<Direction>> directionsFunc,
        Func<Point, Path, bool> isTargetFunc)
    {
        Dictionary<Path, int> distances = new();

        var queue = new PriorityQueue<Path, int>();

        var startEast = ((0, 0), (0, 1), 0);
        var startSouth = ((0, 0), (1, 0), 0);

        int maxX = grid.Keys.Max(p => p.x);
        int maxY = grid.Keys.Max(p => p.y);

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

            foreach (var direction in directionsFunc(path.direction, path.length))
            {
                Point next = (path.point.x + direction.dX, path.point.y + direction.dY);
                if (next.x < 0 || next.x > maxX || next.y < 0 || next.y > maxY)
                {
                    continue;
                }

                Path nextPath = (next, direction, direction == path.direction ? path.length + 1 : 1);
                int nextCost = distances[path] + grid[next];

                if (nextCost >= distances.GetValueOrDefault(nextPath, int.MaxValue))
                {
                    continue;
                }

                distances[nextPath] = nextCost;
                queue.Enqueue(nextPath, nextCost + ManhattanDistance(next, target));
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

        yield return RotateLeft(direction);
        yield return RotateRight(direction);
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

                yield return RotateLeft(direction);
                yield return RotateRight(direction);
                break;
        }
    }

    private static Direction RotateLeft(Direction direction) =>
        (-direction.dY, direction.dX);

    private static Direction RotateRight(Direction direction) =>
        (direction.dY, -direction.dX);

    private static T ManhattanDistance<T>((T x, T y) p1, (T x, T y) p2) where T : INumber<T>
    {
        return T.Abs(p1.x - p2.x) + T.Abs(p1.y - p2.y);
    }

    private static Dictionary<Point, int> ToGrid(string input)
    {
        var grid = new Dictionary<Point, int>();

        foreach ((string data, int y) in input.ToLines().Select((s, i) => (data: s, y: i )))
        {
            foreach ((char ch, int x) in data.Select((c, i) => (c, x: i)))
            {
                grid[(x, y)] = ch - '0';
            }
        }

        return grid;
    }
}