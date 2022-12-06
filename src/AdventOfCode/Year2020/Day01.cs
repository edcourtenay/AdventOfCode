namespace AdventOfCode.Year2020;

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
        var lines = input.ToLines(int.Parse).ToArray();

        return lines
            .Combinations(width)
            .First(ints => ints.Sum() == 2020)
            .Aggregate((i, j) => i * j);
    }
}