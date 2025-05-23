namespace AdventOfCode.Solutions.Year2023;

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
        grid.Positions
            .Where(kvp => kvp.Value == 'O')
            .Select(kvp => grid.Height - kvp.Key.Y)
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
                Direction.North => (0, -1, data => data.Positions.Keys.Where(p => p.Y > 0).OrderBy(p => p.Y)),
                Direction.South => (0, 1, data => data.Positions.Keys.Where(p => p.Y < grid.Height - 1).OrderByDescending(p => p.Y)),
                Direction.West => (-1, 0, data => data.Positions.Keys.Where(p => p.X > 0).OrderBy(p => p.X)),
                Direction.East => (1, 0, data => data.Positions.Keys.Where(p => p.X < grid.Width - 1).OrderByDescending(p => p.X)),
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
        Point testPosition = (position.X + dX, position.Y + dY);

        if (!grid.Positions.TryGetValue(position, out var c) || c != 'O' || grid.Positions.ContainsKey(testPosition))
        {
            return;
        }

        Point lastLegal;
        do
        {
            lastLegal = testPosition;
            testPosition = (testPosition.X + dX, testPosition.Y + dY);
        } while (testPosition is { X: >= 0, Y: >= 0 }
                 && testPosition.X < grid.Width
                 && testPosition.Y < grid.Height
                 && !grid.Positions.ContainsKey(testPosition));

        grid.Positions[lastLegal] = 'O';
        grid.Positions.Remove(position);
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
            .ToDictionary(t => new Point(t.x, t.y), t => t.c);

        return new GridData(positions.Max(t => t.x + 1), positions.Max(t => t.y + 1), dictionary);
    }

    private static long GenerateHashCode(GridData grid) =>
        grid.Positions
            .Where(pair => pair.Value == 'O')
            .Select(kvp => (long)HashCode.Combine(kvp.Key.X, kvp.Key.Y))
            .Sum();

    public enum Direction
    {
        North, South, East, West
    }

    public readonly record struct GridData(int Width, int Height, Dictionary<Point, char> Positions);
}