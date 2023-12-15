using Point = (int x, int y);
using GridData = (int xLength, int yLength, System.Collections.Generic.Dictionary<(int x, int y), char> positions);

namespace AdventOfCode.Year2023;

[Description("Parabolic Reflector Dish")]
public class Day14 : IPuzzle
{
    public object Part1(string input)
    {
        GridData grid = ParseInput(input);

        Tilt(grid, Direction.North);
        return CalculateNorthLoad(grid);
    }

    public object Part2(string input)
    {
        GridData grid = ParseInput(input);

        TiltCycles(1000000000, grid);
        return CalculateNorthLoad(grid);
    }

    private static int CalculateNorthLoad(GridData grid) =>
        grid.positions
            .Where(kvp => kvp.Value == 'O')
            .Select(kvp => grid.yLength - kvp.Key.y)
            .Sum();

    public static void Cycle(GridData grid)
    {
        Tilt(grid, Direction.North);
        Tilt(grid, Direction.West);
        Tilt(grid, Direction.South);
        Tilt(grid, Direction.East);
    }

    public static void Tilt(GridData grid, Direction direction)
    {
        (int dx, int dy, Func<GridData, IEnumerable<Point>> f) tuple = direction switch
            {
                Direction.North => (0, -1, data => data.positions.Keys.Where(p => p.y > 0).OrderBy(p => p.y)),
                Direction.South => (0, 1, data => data.positions.Keys.Where(p => p.y < grid.yLength - 1).OrderByDescending(p => p.y)),
                Direction.West => (-1, 0, data => data.positions.Keys.Where(p => p.x > 0).OrderBy(p => p.x)),
                Direction.East => (1, 0, data => data.positions.Keys.Where(p => p.x < grid.xLength - 1).OrderByDescending(p => p.x)),
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };

        Tilt(grid, tuple.dx, tuple.dy, tuple.f);
    }

    private static void Tilt(GridData grid, int dX, int dY, Func<GridData, IEnumerable<Point>> f)
    {
        foreach (Point position in f(grid))
        {
            AttemptMove(grid, dX, dY, position);
        }
    }

    private static void AttemptMove(GridData grid, int dX, int dY, Point position)
    {
        Point testPosition = (position.x + dX, position.y + dY);

        if (!grid.positions.TryGetValue(position, out var c) || c != 'O' || grid.positions.ContainsKey(testPosition))
        {
            return;
        }

        Point lastLegal;
        do
        {
            lastLegal = testPosition;
            testPosition = (testPosition.x + dX, testPosition.y + dY);
        } while (testPosition is { x: >= 0, y: >= 0 }
                 && testPosition.x < grid.xLength
                 && testPosition.y < grid.yLength
                 && !grid.positions.ContainsKey(testPosition));

        grid.positions[lastLegal] = 'O';
        grid.positions.Remove(position);
    }

    static void TiltCycles(int totalCycles, GridData platform)
    {
        var seen = new Dictionary<long, int>();

        for (var cycle = 1; cycle <= totalCycles; cycle++)
        {
            Cycle(platform);

            var key = GenerateHashCode(platform);
            if (seen.TryGetValue(key, out int cycleStart))
            {
                var cycleLength = cycle - cycleStart;
                var remainingCycles = (totalCycles - cycleStart) % cycleLength;
                for (var i = 0; i < remainingCycles; i++)
                {
                    Cycle(platform);
                }

                return;
            }

            seen.Add(key, cycle);
        }
    }

    public static GridData ParseInput(string input)
    {
        var lines = input.ToLines();
        var positions = lines.SelectMany((s, y) => s.Select((c, x) => (c, x, y)))
            .Where(t => t.c != '.')
            .ToHashSet();

        var dictionary = positions
            .ToDictionary(t => (t.x, t.y), t => t.c);

        return (positions.Max(t => t.x + 1), positions.Max(t => t.y + 1), dictionary);
    }

    private static long GenerateHashCode(GridData grid) =>
        grid.positions
            .Where(pair => pair.Value == 'O')
            .Select(kvp => (long)HashCode.Combine(kvp.Key.x, kvp.Key.y))
            .Sum();

    public enum  Direction
    {
        North, South, East, West
    }
}