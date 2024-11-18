namespace AdventOfCode.Solutions.Year2020;

[Description("Report Repair")]
public class Day01 : IPuzzle
{
    public object Part1(string input)
    {
        return Solve(input, 2);
    }

    public object Part2(string input)
    {
        return Solve(input, 3);
    }

    private static object Solve(string input, int width)
    {
        var lines = Enumerable.ToArray<int>(input.ToLines(int.Parse));

        return Enumerable
            .First<IEnumerable<int>>(lines
                .Combinations(width), ints => Enumerable.Sum((IEnumerable<int>)ints) == 2020)
            .Aggregate((i, j) => i * j);
    }
}