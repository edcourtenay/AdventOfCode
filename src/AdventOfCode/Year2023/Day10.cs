using Point = (int X, int Y);

namespace AdventOfCode.Year2023;

[Description("Pipe Maze")]
public class Day10 : IPuzzle
{
    private const int North = 0b0001;
    private const int South = 0b0010;
    private const int East = 0b0100;
    private const int West = 0b1000;

    private const int NorthToEast = North | East;
    private const int EastToSouth = East | South;
    private const int SouthToWest = South | West;
    private const int WestToNorth = West | North;
    private const int NorthToSouth = North | South;
    private const int EastToWest = East | West;
    private const int NoConnection = 0b0000;

    private readonly (int SourceMask, int DestinationMask, Point Direction)[] _directions =
    [
        (South, North, (0, 1)),
        (East, West, (1, 0)),
        (North, South, (0, -1)),
        (West, East, (-1, 0))
    ];

    public object Part1(string input)
    {
        (Point Start, int[][] Map) map = Parse(input);

        var pipe = FindStartingPipe(map.Map, map.Start);
        map.Map[map.Start.X][map.Start.Y] = pipe;

        IList<Point> loop = GetLoop(map.Map, map.Start).ToList();

        return loop.Count / 2;
    }

    public object Part2(string input)
    {
        (Point Start, int[][] Map) map = Parse(input);

        var pipe = FindStartingPipe(map.Map, map.Start);
        map.Map[map.Start.X][map.Start.Y] = pipe;

        HashSet<Point> loop = GetLoop(map.Map, map.Start).ToHashSet();
        var points = InteriorPoints(map, loop).ToArray();

        return points.Length;
    }

    private static IEnumerable<Point> InteriorPoints((Point Start, int[][] Map) data, HashSet<Point> loop)
    {
        int[][] map = data.Map;
        for (int y = 0; y < map[0].Length; y++)
        {
            bool inside = false;
            bool top = false;

            for (int x = 0; x < map.Length; x++)
            {
                int item = loop.Contains((x, y))
                    ? map[x][y]
                    : NoConnection;

                switch (item)
                {
                    case NorthToSouth:
                        inside = !inside;
                        break;

                    case EastToWest:
                        break;

                    case EastToSouth:
                        top = true;
                        break;

                    case NorthToEast:
                        top = false;
                        break;

                    case WestToNorth:
                        inside = inside != top;
                        break;

                    case SouthToWest:
                        inside = inside == top;
                        break;

                    case NoConnection when inside:
                        yield return (x, y);
                        break;
                }
            }
        }
    }

    private IEnumerable<Point> GetLoop(int[][] mapMap, Point mapStart)
    {
        var visited = new HashSet<Point>();
        var current = mapStart;
        while (visited.Add(current))
        {
            yield return current;

            foreach ((int SourceMask, int DestinationMask, Point Direction) dir in _directions)
            {
                if ((mapMap[current.X][current.Y] & dir.SourceMask) != dir.SourceMask)
                {
                    continue;
                }

                Point proposed = (current.X + dir.Direction.X, current.Y + dir.Direction.Y);
                if (visited.Contains(proposed))
                {
                    continue;
                }

                current = proposed;
                break;
            }
        }
    }

    private static (Point, int[][]) Parse(string input)
    {
        Point startPosition = (0, 0);
        var map =input.ToLines((line, y) => line.Select((c, x) =>
        {
            if (c == 'S')
            {
                startPosition = (x, y);
                c = '.';
            }
            return c switch
            {
                '|' => NorthToSouth,
                '-' => EastToWest,
                'L' => NorthToEast,
                'J' => WestToNorth,
                '7' => SouthToWest,
                'F' => EastToSouth,
                '.' => NoConnection,
                _ => throw new Exception($"Unknown direction: {c}")
            };
        }));
        int[][] array = map.Pivot().Select(ints => ints.ToArray()).ToArray();
        return (startPosition, array);
    }

    private int FindStartingPipe(int[][] map, Point position)
    {
        var validDirections = _directions
            .Where(direction =>
            {
                int newY = position.Y + direction.Direction.Y;
                int newX = position.X + direction.Direction.X;
                return newY >= 0 && newY < map.Length && newX >= 0
                       && newX < map[position.Y].Length;
            });

        int start = 0;
        foreach ((int _, int targetMask, Point dir) in validDirections)
        {
            var target = map[position.X + dir.X][position.Y + dir.Y];
            start |= (target & targetMask);
        }

        return start ^ 0b1111;
    }
}