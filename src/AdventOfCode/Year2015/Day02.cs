using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode.Year2015;

[SuppressMessage("ReSharper", "UnusedType.Global")]
[Description("I Was Told There Would Be No Math")]
public class Day02 : IPuzzle
{
    public object Part1(string input)
    {
        return Data(input).Select(CalculatePaper).Sum();
    }

    public object Part2(string input)
    {
        return Data(input).Select(CalculateRibbon).Sum();
    }

    public IEnumerable<Box> Data(string input)
    {
        var r = new StringReader(input);

        while (r.ReadLine() is { } line)
        {
            yield return ParseLine(line);
        }
    }
    
    public int CalculatePaper(Box box)
    {
        var sides = new[]
        {
            box.Length * box.Width,
            box.Width * box.Height,
            box.Height * box.Length
        }.Order().ToArray();

        return sides.Select(s => s * 2).Sum() + sides[0];
    }

    public Box ParseLine(string input)
    {
        var split = input.Split('x').Select(int.Parse).ToArray();
        return new Box(split[0], split[1], split[2]);
    }
    
    public record Box(int Width, int Length, int Height);

    public int CalculateRibbon(Box box)
    {
        var (w, l, h) = box;
        var a = new[] { w, l, h }.Order().ToArray();
        return a[0] * 2 + a[1] * 2 + a[0] * a[1] * a[2];
    }
}