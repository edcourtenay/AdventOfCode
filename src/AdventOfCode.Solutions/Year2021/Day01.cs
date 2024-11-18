namespace AdventOfCode.Solutions.Year2021;

[Description("Sonar Sweep")]
public class Day01 : IPuzzle
{
    public object Part1(string input)
    {
        return Enumerable
            .Count<(int First, int Second)>(input
                .ToLines(int.Parse)
                .Pairwise(), tuple => tuple.Second > tuple.First);
    }

    public object Part2(string input)
    {
        return Enumerable
            .Count<(int First, int Second)>(Enumerable
                .Select<IEnumerable<int>, int>(input
                    .ToLines(int.Parse)
                    .SlidingWindow(3), ints => Enumerable.Sum((IEnumerable<int>)ints))
                .Pairwise(), tuple => tuple.Second > tuple.First);
    }
}