using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2015;

[Description("Science for Hungry People")]
public partial class Day15 : IPuzzle
{
    public object Part1(string input)
    {
        return Solve(input, null);
    }

    public object Part2(string input)
    {
        return Solve(input, 500);
    }

    long Solve(string input, int? calories) {
        var ingredients = Parse(input);
        var propsCount = ingredients[0].Length;

        var maxValue = 0L;
        foreach (var amounts in Partition(100, ingredients.Length)) {
            var props = new int[propsCount];
            for (int ingredient = 0; ingredient < ingredients.Length; ingredient++) {
                for (int prop = 0; prop < 5; prop++) {
                    props[prop] += ingredients[ingredient][prop] * amounts[ingredient];
                }
            }
            if (!calories.HasValue || calories.Value == props.Last()) {
                var value = props.Take(propsCount - 1).Aggregate(1L, (acc, p) => acc * Math.Max(0, p));
                maxValue = Math.Max(maxValue, value);
            }
        }
        return maxValue;
    }

    static int[][] Parse(string input) =>
        (input.ToLines()
            .Select(line => new { line, m = ParseRegex().Match(line) })
            .Select(t =>
                new { t, nums = t.m.Groups
                    .Cast<Group>()
                    .Skip(1)
                    .Select(g => int.Parse(g.Value))
                    .ToArray()
                })
            .Select(t => t.nums))
        .ToArray();


    static IEnumerable<int[]> Partition(int n, int k)
    {
        if (k == 1) {
            yield return [n];
            yield break;
        }

        for (var i = 0; i <= n; i++) {
            foreach (var rest in Partition(n - i, k - 1)) {
                yield return rest.Select(x => x).Append(i).ToArray();
            }
        }
    }

    [GeneratedRegex(".*: capacity (.*), durability (.*), flavor (.*), texture (.*), calories (.*)")]
    private static partial Regex ParseRegex();
}