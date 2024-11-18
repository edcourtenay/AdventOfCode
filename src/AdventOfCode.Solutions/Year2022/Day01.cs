namespace AdventOfCode.Solutions.Year2022;

[Description("Calorie Counting")]
public class Day01 : IPuzzle
{
    public object Part1(string input)
    {
        return Solve(input, 1);
    }

    public object Part2(string input)
    {
        return Solve(input, 3);
    }

    private static int Solve(string input, int topCount)
    {
        return Enumerable
            .Select<IEnumerable<string>, int>(input.ToLines().ToSequences(string.IsNullOrEmpty), set => Enumerable.Select<string, int>(set, int.Parse).Sum())
            .OrderDescending().Take(topCount).Sum();
    }
}