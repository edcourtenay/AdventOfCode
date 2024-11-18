namespace AdventOfCode.Solutions.Year2022;

[Description("Tuning Trouble")]
public class Day06 : IPuzzle
{
    public object Part1(string input) => FindMarker(input, 4);

    public object Part2(string input) => FindMarker(input, 14);

    private static object FindMarker(string input, int size)
    {
        var first = Enumerable.Select(input.SlidingWindow(size), (chars, index) => new { chars, index })
            .First(arg => Enumerable.Distinct<char>(arg.chars).Count() == size);

        return first.index + size;
    }
}