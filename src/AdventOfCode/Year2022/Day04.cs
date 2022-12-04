using System.Collections;

namespace AdventOfCode.Year2022;

[Description("Camp Cleanup")]
public class Day04 : IPuzzle
{
    public object Part1(string input)
        => Solve(input, ranges => ranges[0].Intersect(ranges[1]).Count() == ranges.Min(range => range.Length));

    public object Part2(string input)
        => Solve(input, ranges => ranges[0].Intersect(ranges[1]).Any());

    private static int Solve(string input, Func<Range[], bool> countFunc)
    {
        return input
            .ToLines(s => s.Split(',').Select(Range.Parse).ToArray())
            .Count(countFunc);
    }

    public record Range(int Start, int End) : IEnumerable<int>
    {
        public int Length => End - Start + 1;

        public IEnumerator<int> GetEnumerator()
        {
            return Enumerable.Range(Start, Length).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static Range Parse(string s)
        {
            int[] ints = s.Split('-').Select(int.Parse).ToArray();
            return new Range(ints[0], ints[1]);
        }
    }
}