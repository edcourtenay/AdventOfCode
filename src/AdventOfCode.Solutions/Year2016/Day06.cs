namespace AdventOfCode.Solutions.Year2016;

[Description("Signals and Noise")]
public class Day06 : IPuzzle
{
    public object Part1(string input)
    {
        return Solve(input, l => l.GroupBy(c => c).OrderByDescending(chars => chars.Count())
            .Select(g => g.Key)
            .First());
    }

    public object Part2(string input)
    {
        return Solve(input, l => l.GroupBy(c => c).OrderBy(chars => chars.Count())
            .Select(g => g.Key)
            .First());
    }

    private static string Solve(string input, Func<IEnumerable<char>, char> selector)
    {
        var t = Enumerable.Select(input.ToLines()
                .Pivot(), selector);
        return new string(t.ToArray());
    }
}