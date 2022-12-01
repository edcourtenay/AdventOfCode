namespace AdventOfCode.Year2021;

[Description("Sonar Sweep")]
public class Day01 : IPuzzle
{
    public object Part1(string input)
    {
        return input
            .ToLines(int.Parse)
            .Pairwise()
            .Count(tuple => tuple.Second > tuple.First);
    }

    public object Part2(string input)
    {
        return input
            .ToLines(int.Parse)
            .SlidingWindow(3)
            .Select(ints => ints.Sum())
            .Pairwise()
            .Count(tuple => tuple.Second > tuple.First);
    }
}