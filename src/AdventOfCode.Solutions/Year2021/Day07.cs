namespace AdventOfCode.Solutions.Year2021;

[Description("The Treachery of Whales")]
public class Day07 : IPuzzle
{
    public object Part1(string input)
    {
        (int result1, _) = Execute(input);
        return result1;
    }

    public object Part2(string input)
    {
        (_, int result2) = Execute(input);
        return result2;
    }

    private (int result1, int result2) Execute(string input)
    {
        var crabs = input.Split(',').Select(s => Convert.ToInt32(s)).ToArray();

        var g = crabs.GroupBy(i => i).ToArray();
        var min = crabs.Min();
        var max = crabs.Max();

        (int result1, int result2) = (Int32.MaxValue, Int32.MaxValue);

        for (int i = min; i <= max; i++)
        {
            var (fuel1, fuel2) = SolveFor(g, i);

            result1 = fuel1 < result1 ? fuel1 : result1;
            result2 = fuel2 < result2 ? fuel2 : result2;
        }

        return (result1, result2);
    }

    private (int, int) SolveFor(IGrouping<int, int>[] crabs, int i) =>
        (SolveFor1(crabs, i), SolveFor2(crabs, i));

    private int SolveFor1(IGrouping<int,int>[] crabs, int i) =>
        crabs.Sum(grouping => Math.Abs(grouping.Key - i) * grouping.Count());

    private int SolveFor2(IGrouping<int, int>[] crabs, int i) =>
        crabs.Sum(grouping => Gauss(Math.Abs(grouping.Key - i)) * grouping.Count());

    private int Gauss(int i) => i * (i + 1) / 2;
}