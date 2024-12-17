using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode.Solutions.Year2015;

[SuppressMessage("ReSharper", "UnusedType.Global")]
[Description("Perfectly Spherical Houses in a Vacuum")]
public class Day03 : IPuzzle
{
    public object Part1(string input)
    {
        var santa = Point.Zero;
        HashSet<Point> positions = [santa];
        foreach (var direction in input.Select(ParseChar))
        {
            santa += direction;
            positions.Add(santa);
        }

        return positions.Count;
    }

    public object Part2(string input)
    {
        var santa = Point.Zero;
        var robot = Point.Zero;
        var positions = new HashSet<Point> { santa, robot };
        
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
            '^' => Direction.North,
            'v' => Direction.South,
            '<' => Direction.West,
            '>' => Direction.East,
            _ => throw new ArgumentOutOfRangeException(nameof(c), c, null)
        };
    }
}