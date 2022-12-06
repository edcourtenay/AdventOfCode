namespace AdventOfCode.Year2020;

[Description("Custom Customs")]
public class Day06 : IPuzzle
{
    public object Part1(string input)
    {
        return input.ToLines()
            .ToSequences(string.IsNullOrEmpty)
            .Select(e => new HashSet<char>(e.SelectMany(s => s.ToCharArray())))
            .Sum(set => set.Count);
    }

    public object Part2(string input)
    {
        return input.ToLines()
            .ToSequences(string.IsNullOrEmpty)
            .Select(first => first.Select(s => s.ToCharArray()).Aggregate((s1, s2) => s1.Intersect(s2).ToArray()))
            .Sum(chars => chars.Length);
    }
}