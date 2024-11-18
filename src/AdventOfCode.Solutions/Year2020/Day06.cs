namespace AdventOfCode.Solutions.Year2020;

[Description("Custom Customs")]
public class Day06 : IPuzzle
{
    public object Part1(string input)
    {
        return Enumerable
            .Select<IEnumerable<string>, HashSet<char>>(input.ToLines()
                .ToSequences(string.IsNullOrEmpty), e => new HashSet<char>(Enumerable.SelectMany<string, char>(e, s => s.ToCharArray())))
            .Sum(set => set.Count);
    }

    public object Part2(string input)
    {
        return Enumerable
            .Select<IEnumerable<string>, char[]>(input.ToLines()
                .ToSequences(string.IsNullOrEmpty), first => Enumerable.Select<string, char[]>(first, s => s.ToCharArray()).Aggregate((s1, s2) => s1.Intersect(s2).ToArray()))
            .Sum(chars => chars.Length);
    }
}