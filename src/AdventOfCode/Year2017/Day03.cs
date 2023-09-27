namespace AdventOfCode.Year2017;

[Description("Spiral Memory")]
public class Day03 : IPuzzle
{
    public object Part1(string input)
    {
        var sideLength = LengthOfSideWith(int.Parse(input));
        var midpoints = MidpointsForSideLength(sideLength);
        var stepsToRingFromCenter = (sideLength - 1) / 2;
        return stepsToRingFromCenter + midpoints.Select(midpoint => Math.Abs(int.Parse(input) - midpoint)).Min();
    }

    public object Part2(string input)
    {
        return string.Empty;
    }

    private static int LengthOfSideWith(int n)
    {
        int i = (int)Math.Round(Math.Sqrt(n));
        return (i % 2 == 0) switch
        {
            true => i + 1,
            false => i
        };
    }

    private int[] MidpointsForSideLength(int sideLength)
    {
        var highest = sideLength * sideLength;
        var offset = (int)((sideLength - 1) / 2.0);
        return Enumerable.Range(0, 4)
            .Select(i => highest - (offset + (i * (sideLength - 1))))
            .ToArray();
    }
}