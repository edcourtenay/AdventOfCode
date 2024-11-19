namespace AdventOfCode.Solutions.Year2024;

[Description("Historian Hysteria")]
public class Day01 : IPuzzle
{
    public object Part1(string input)
    {
        var data = ParseInput(input).ToArray();

        var list1 = data.Select(i => i.Item1).Order();
        var list2 = data.Select(i => i.Item2).Order();

        var ordered = list1.Zip(list2, (a, b) => (a, b));

        return ordered.Sum(tuple => Math.Abs(tuple.a - tuple.b));
    }

    public object Part2(string input)
    {
        var data = ParseInput(input).ToArray();

        var list1 = data.Select(i => i.Item1);

        var group = data
            .Select(i => i.Item2)
            .GroupBy(i => i)
            .ToDictionary(g => g.Key, i => i.Count());

        return list1.Sum(k => group.GetValueOrDefault(k, 0) * k);
    }

    private static IEnumerable<(int, int)> ParseInput(string input)
    {
        foreach (var line in input.ToLines())
        {
            string[] strings = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            if (strings is [{ } a, { } b])
            {
                yield return ((int.Parse(a), int.Parse(b)));
            }
        }
    }
}
