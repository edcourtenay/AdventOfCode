using System.Collections;

namespace AdventOfCode.Year2022;

[Description("Camp Cleanup")]
public class Day04 : IPuzzle
{
    public object Part1(string input)
        => Solve(input, r => IntersectAll(r).Count() == r.Min(range => range.Length));

    public object Part2(string input)
        => Solve(input, r => IntersectAll(r).Any());

    private static int Solve(string input, Func<Range[], bool> countFunc)
    {
        return input
            .ToLines(s => s.Split(',').Select(Range.Parse).ToArray())
            .Count(countFunc);
    }

    private static IEnumerable<int> IntersectAll(IEnumerable<Range> r)
    {
        return r.Aggregate((IEnumerable<int> a1, IEnumerable<int> a2) => a1.Intersect(a2));
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