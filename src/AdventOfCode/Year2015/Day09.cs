using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2015;

[SuppressMessage("ReSharper", "UnusedType.Global")]
[Description("All in a Single Night")]
public partial class Day09 : IPuzzle
{
    [GeneratedRegex(@"^(?<start>\w+)\sto\s(?<finish>\w+)\s=\s(?<distance>\d+)$", RegexOptions.Compiled)]
    private static partial Regex MyRegex();

    public object Part1(string input)
    {
        return Routes(input).Min();
    }

    public object Part2(string input)
    {
        return Routes(input).Max();
    }

    private IEnumerable<int> Routes(string input)
    {
        Dictionary<(string s, string f), int> enumerable = Data(input)
            .Select(s => MyRegex().Match(s))
            .Where(m => m.Success)
            .SelectMany(m =>
            {
                var (s, f) = (m.Groups["start"].Value, m.Groups["finish"].Value);
                var d = int.Parse(m.Groups["distance"].Value);
                return new[] { (k: (s, f), d), (k: (f, s), d) };
            })
            .ToDictionary(p => p.k, p => p.d);

        var places = enumerable.Keys.Select(k => k.s).Distinct().ToArray();

        return places.Permutations().Select(route =>
            route.Zip(route.Skip(1), (a, b) => enumerable[(a, b)]).Sum());
    }
    
    public IEnumerable<string> Data(string input)
    {
        using var r = new StringReader(input);

        while (r.ReadLine() is { } line)
        {
            yield return line;
        }
    }
}