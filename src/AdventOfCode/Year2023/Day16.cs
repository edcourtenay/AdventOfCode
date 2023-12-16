using Point = (int x, int y);
using Dir = (int dX, int dY);
using Beam = ((int x, int y) position, (int dX, int dY) direction);
using GridData = (int xLength, int yLength, System.Collections.Generic.Dictionary<(int x, int y), char> positions);

namespace AdventOfCode.Year2023;

[Description("The Floor Will Be Lava")]
public class Day16 : IPuzzle
{
    private static readonly Dir North = (0, -1);
    private static readonly Dir South = (0, 1);
    private static readonly Dir West = (-1, 0);
    private static readonly Dir East = (1, 0);

    private static readonly Dir[] EastWest = {(1, 0), (-1, 0)};
    private static readonly Dir[] NorthSouth = {(0, 1), (0, -1)};

    private static readonly (char tile, Func<Dir, Dir>? reflectFunc, Func<Dir, bool>? splitTestFunc, Dir[]? splitDirections)[] Tiles =
    {
        ('/', dir => (-dir.dY, -dir.dX), null, null),
        ('\\', dir => (dir.dY, dir.dX), null, null),
        ('-', null, dir => dir.dY != 0, EastWest),
        ('|', null, dir => dir.dX != 0, NorthSouth)
    };

    public object Part1(string input)
    {
        return RunBeam(((0, 0), East), ParseInput(input));
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
        for (int y = 0; y < grid.yLength; y++)
        {
            yield return ((0, y), East);
            yield return ((grid.xLength - 1, y), West);
        }

        for (int x = 0; x < grid.xLength; x++)
        {
            yield return ((x, 0), South);
            yield return ((x, grid.yLength - 1), North);
        }
    }

    private static int RunBeam(Beam start, GridData grid)
    {
        var beams = new Queue<Beam>(new [] { start });
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
        var position = beam.position;

        while (IsInGrid(grid, position))
        {
            visited.Add(position);
            if (grid.positions.TryGetValue(position, out var tile))
            {
                (char _, Func<Dir, Dir>? reflectFunc, Func<Dir, bool>? splitTest, Dir[]? splitDirections) = Tiles.First(t => t.tile == tile);
                if (reflectFunc != null)
                {
                    beam.direction = reflectFunc(beam.direction);
                }
                else if (splitTest?.Invoke(beam.direction) == true)
                {
                    newBeams = splitDirections!.Select(dir =>
                    {
                        Point point = (position.x + dir.dX, position.y + dir.dY);
                        return (Beam)(point, dir);
                    }).ToArray();
                    return true;
                }
            }

            position = (position.x + beam.direction.dX, position.y + beam.direction.dY);
        }

        newBeams = Array.Empty<Beam>();
        return false;
    }

    private static bool IsInGrid(GridData grid, Point position)
    {
        return position is { x: >= 0, y: >= 0 } && position.x < grid.xLength && position.y < grid.yLength;
    }

    private static GridData ParseInput(string input)
    {
        IEnumerable<string> lines = input.ToLines();
        HashSet<(char c, int x, int y)> positions = lines.SelectMany((s, y) => s.Select((c, x) => (c, x, y)))
            .Where(t => t.c != '.')
            .ToHashSet();

        Dictionary<Point, char> dictionary = positions
            .ToDictionary(t => (t.x, t.y), t => t.c);

        return (positions.Max(t => t.x + 1), positions.Max(t => t.y + 1), dictionary);
    }
}