using Range = (ulong from, ulong to);

namespace AdventOfCode.Solutions.Year2025;

[Description("Cafeteria")]
public class Day05 : IPuzzle
{
    public object Part1(string input)
    {
        var (ranges, ingredients) = ParseInput(input);

        var fresh = ingredients.Count(i => ranges.Any(r => r.Contains(i)));

        return fresh;
    }

    public object Part2(string input)
    {
        var (ranges, _) = ParseInput(input);

        ulong sum = 0;
        foreach (var range in ranges)
        {
            sum += range.RangeLength();
        }

        return sum;
    }

    public (List<Range> ranges, List<ulong> ingredients) ParseInput(string input)
    {
        using IEnumerator<string> enumerator = input.ToLines().GetEnumerator();

        List<Range> ranges = [];
        List<ulong> ingredients = [];

        bool ingredientsSection = false;
        while (enumerator.MoveNext())
        {
            var current = enumerator.Current;

            if (string.IsNullOrWhiteSpace(current))
            {
                ingredientsSection = true;
                continue;
            }

            if (ingredientsSection)
            {
                ingredients.Add(ulong.Parse(current));
            }
            else
            {
                var parts = current.Split('-');
                ranges.Add((ulong.Parse(parts[0]), ulong.Parse(parts[1])));
            }
        }

        ranges = ranges.Merge().ToList();

        return (ranges, ingredients);
    }
}