namespace AdventOfCode.Solutions.Year2016;

[Description("Squares With Three Sides")]
public class Day03 : IPuzzle
{
    public object Part1(string input)
    {
        return input
            .ToLines()
            .Select<string, string[]>(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries))
            .Select(sides => sides.Select(int.Parse).ToArray())
            .Count(sides => IsValidTriangle(sides[0], sides[1], sides[2]));
    }

    public object Part2(string input)
    {
        var t = input
            .ToLines()
            .Select<string, string[]>(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries))
            .Select(sides => sides.Select(int.Parse).ToArray())
            .Pivot()
            .SelectMany(ints => ints)
            .Window(3);

        return t.Select<IEnumerable<int>, int[]>(ints => ints.ToArray())
            .Count(ints => IsValidTriangle(ints[0], ints[1], ints[2]));
    }

    public static bool IsValidTriangle(int a, int b, int c) => (a + b) > c && (a + c) > b && (b + c) > a;
}