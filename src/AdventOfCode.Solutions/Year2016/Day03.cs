namespace AdventOfCode.Solutions.Year2016;

[Description("Squares With Three Sides")]
public class Day03 : IPuzzle
{
    public object Part1(string input)
    {
        return Enumerable
            .Select<string, string[]>(input
                .ToLines(), line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries))
            .Select(sides => sides.Select(int.Parse).ToArray())
            .Count(sides => IsValidTriangle(sides[0], sides[1], sides[2]));
    }

    public object Part2(string input)
    {
        var t = Enumerable
            .SelectMany<IEnumerable<int>, int>(Enumerable
                .Select<string, string[]>(input
                    .ToLines(), line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                .Select(sides => sides.Select(int.Parse).ToArray())
                .Pivot(), ints => ints)
            .Window(3);

        return Enumerable.Select<IEnumerable<int>, int[]>(t, ints => Enumerable.ToArray<int>(ints))
            .Count(ints => IsValidTriangle(ints[0], ints[1], ints[2]));
    }

    public static bool IsValidTriangle(int a, int b, int c) => (a + b) > c && (a + c) > b && (b + c) > a;
}