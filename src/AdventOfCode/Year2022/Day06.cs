namespace AdventOfCode.Year2022;

[Description("Tuning Trouble")]
public class Day06 : IPuzzle
{
    public object Part1(string input) => FindMarker(input, 4);

    public object Part2(string input) => FindMarker(input, 14);

    private static object FindMarker(string input, int size)
    {
        var first = input.SlidingWindow(size).Select((chars, index) => new { chars, index })
            .First(arg => arg.chars.Distinct().Count() == size);

        return first.index + size;
    }
}