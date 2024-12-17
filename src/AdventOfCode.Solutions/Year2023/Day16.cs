namespace AdventOfCode.Solutions.Year2023;

[Description("The Floor Will Be Lava")]
public class Day16 : IPuzzle
{
    private static readonly Direction[] EastWest = [Direction.East, Direction.West];
    private static readonly Direction[] NorthSouth = [Direction.South, Direction.North];

    private static readonly (char tile, Func<Direction, Direction>? reflectFunc, Func<Direction, bool>? splitTestFunc, Direction[]? splitDirections)[] Tiles =
    [
        ('/', dir => (-dir.Y, -dir.X), null, null),
        ('\\', dir => (dir.Y, dir.X), null, null),
        ('-', null, dir => dir.Y != 0, EastWest),
        ('|', null, dir => dir.X != 0, NorthSouth)
    ];

    public object Part1(string input)
    {
        return RunBeam(new Beam(new Point(0, 0), Direction.East), ParseInput(input));
    }

    public object Part2(string input)
    {
        GridData grid = ParseInput(input);

        return GetBeams(grid)
            .AsParallel()
            .Max(beam => RunBeam(beam, grid));
    }

    private static IEnumerable<Beam> GetBeams(GridData grid)
    {
        for (int y = 0; y < grid.Height; y++)
        {
            yield return new Beam(new Point(0, y), Direction.East);
            yield return new Beam(new Point(grid.Width - 1, y), Direction.West);
        }

        for (int x = 0; x < grid.Width; x++)
        {
            yield return new Beam(new Point(x, 0), Direction.South);
            yield return new Beam(new Point(x, grid.Height - 1), Direction.North);
        }
    }

    private static int RunBeam(Beam start, GridData grid)
    {
        var beams = new Queue<Beam>([start]);
        var pointsVisited = new HashSet<Point>();
        var beamsVisited = new HashSet<Beam> { start };

        while (beams.TryDequeue(out var beam))
        {
            if (!AttemptMove(grid, beam, pointsVisited, out var newBeams))
            {
                continue;
            }

            foreach (Beam newBeam in newBeams)
            {
                if (beamsVisited.Add(newBeam))
                {
                    beams.Enqueue(newBeam);
                }
            }
        }

        return pointsVisited.Count;
    }

    private static bool AttemptMove(GridData grid, Beam beam, HashSet<Point> visited, out Beam[] newBeams)
    {
        var position = beam.Position;

        while (IsInGrid(grid, position))
        {
            visited.Add(position);
            if (grid.Positions.TryGetValue(position, out var tile))
            {
                (char _, Func<Direction, Direction>? reflectFunc, Func<Direction, bool>? splitTest, Direction[]? splitDirections) = Tiles.First(t => t.tile == tile);
                if (reflectFunc != null)
                {
                    beam = beam with { Direction = reflectFunc(beam.Direction) };
                }
                else if (splitTest?.Invoke(beam.Direction) == true)
                {
                    newBeams = splitDirections!.Select(Beam (dir) =>
                    {
                        Point point = new(position.X + dir.X, position.Y + dir.Y);
                        return new Beam(point, dir);
                    }).ToArray();
                    return true;
                }
            }

            position = new Point(position.X + beam.Direction.X, position.Y + beam.Direction.Y);
        }

        newBeams = [];
        return false;
    }

    private static bool IsInGrid(GridData grid, Point position)
    {
        return position is { X: >= 0, Y: >= 0 } && position.X < grid.Width && position.Y < grid.Height;
    }

    private static GridData ParseInput(string input)
    {
        IEnumerable<string> lines = input.ToLines();
        HashSet<(char c, int x, int y)> positions = lines.SelectMany((s, y) => s.Select((c, x) => (c, x, y)))
            .Where(t => t.c != '.')
            .ToHashSet();

        Dictionary<Point, char> dictionary = positions
            .ToDictionary(t => new Point(t.x, t.y), t => t.c);

        return new GridData(positions.Max(t => t.x + 1), positions.Max(t => t.y + 1), dictionary);
    }

    private readonly record struct Beam(Point Position, Direction Direction);
    private readonly record struct GridData(int Width, int Height, Dictionary<Point, char> Positions);
}