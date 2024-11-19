namespace AdventOfCode.Solutions.Year2020;

[Description("Custom Customs")]
public class Day06 : IPuzzle
{
    public object Part1(string input)
    {
        return input.ToLines()
            .ToSequences(string.IsNullOrEmpty)
            .Select<IEnumerable<string>, HashSet<char>>(e => [..e.SelectMany(s => s.ToCharArray())])
            .Sum(set => set.Count);
    }

    public object Part2(string input)
    {
        return input.ToLines()
            .ToSequences(string.IsNullOrEmpty)
            .Select<IEnumerable<string>, char[]>(first => first.Select<string, char[]>(s => s.ToCharArray()).Aggregate((s1, s2) => s1.Intersect(s2).ToArray()))
            .Sum(chars => chars.Length);
    }
}