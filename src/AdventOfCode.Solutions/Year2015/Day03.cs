using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode.Solutions.Year2015;

[SuppressMessage("ReSharper", "UnusedType.Global")]
[Description("Perfectly Spherical Houses in a Vacuum")]
public class Day03 : IPuzzle
{
    public object Part1(string input)
    {
        var santa = new Position(0, 0);
        var positions = new HashSet<Position> { santa };
        foreach (var direction in input.Select(ParseChar))
        {
            santa += direction;
            positions.Add(santa);
        }

        return positions.Count;
    }

    public object Part2(string input)
    {
        var santa = new Position(0, 0);
        var robot = new Position(0, 0);
        var positions = new HashSet<Position> { santa, robot };
        
        using var enumerator = input.Select(ParseChar).GetEnumerator();
        while (enumerator.MoveNext())
        {
            santa += enumerator.Current;
            enumerator.MoveNext();
            robot += enumerator.Current;
            positions.Add(santa);
            positions.Add(robot);
        }

        return positions.Count;
    }

    public Direction ParseChar(char c)
    {
        return c switch
        {
            '^' => Direction.Up,
            'v' => Direction.Down,
            '<' => Direction.Left,
            '>' => Direction.Right,
            _ => throw new ArgumentOutOfRangeException(nameof(c), c, null)
        };
    }

    public record struct Position(int X, int Y)
    {
        public static Position operator +(Position position, Direction direction)
        {
            return new Position(position.X + direction.X, position.Y + direction.Y);
        }
    }

    public record struct Direction(int X, int Y)
    {
        public static Direction Up => new(1, 0);
        public static Direction Down => new(-1, 0);
        public static Direction Left => new(0, -1);
        public static Direction Right => new(0, 1);
    }
}