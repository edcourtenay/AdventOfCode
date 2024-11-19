namespace AdventOfCode.Solutions.Year2022;

[Description("Rope Bridge")]
public class Day09 : IPuzzle
{
    private static readonly (int x, int y) Up = new (0, 1);
    private static readonly (int x, int y) Down = new(0, -1);
    private static readonly (int x, int y) Left = new(-1, 0);
    private static readonly (int x, int y) Right = new(1, 0);

    public object Part1(string input) => Solve(input, 1);

    public object Part2(string input) => Solve(input, 9);

    private static object Solve(string input, int segments)
    {
        return Follow(Positions(input.ToLines()
            .SelectMany(Directions)), segments).ToHashSet().Count;
    }

    private static IEnumerable<(int x, int y)> Directions(string line)
    {
        var parts = line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        var d = parts[0] switch
        {
            "U" => Up,
            "D" => Down,
            "L" => Left,
            "R" => Right,
            _ => throw new ArgumentOutOfRangeException()
        };

        for (int i = 0; i < int.Parse(parts[1]); i++)
        {
            yield return d;
        }
    }

    private static IEnumerable<(int x, int y)> Positions(IEnumerable<(int x, int y)> directions)
    {
        var position = (x: 0, y: 0);
        foreach ((int x, int y) direction in directions)
        {
            yield return (position = (position.x + direction.x, position.y + direction.y));
        }
    }

    private static IEnumerable<(int x, int y)> Follow(IEnumerable<(int x, int y)> positions, int segments)
    {
        var tails = new(int x, int y)[segments];
        yield return tails[^1];

        foreach ((int x, int y) p in positions)
        {
            for (int i = 0; i < segments; i++)
            {
                var position = i == 0 ? p : tails[i - 1];
                if (IsTouching(position, tails[i]))
                {
                    continue;
                }

                tails[i] = (tails[i].x + (tails[i].x == position.x ? 0 : tails[i].x < position.x ? 1 : -1),
                    tails[i].y + (tails[i].y == position.y ? 0 : tails[i].y < position.y ? 1 : -1));
            }

            yield return tails[^1];
        }
    }

    private static bool IsTouching((int x, int y) head, (int x, int y) tail)
    {
        (int x, int y)[] neighbours =
        [
            (-1, 1), (0, 1), (1, 1),
            (-1, 0), (0, 0), (1, 0),
            (-1, -1), (0, -1), (1, -1)
        ];
        return neighbours.Any(tuple => (head.x + tuple.x, head.y + tuple.y) == tail);
    }
}