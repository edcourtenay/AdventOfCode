using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2015;

[Description("Knights of the Dinner Table")]
[SuppressMessage("ReSharper", "UnusedType.Global")]
public class Day13 : IPuzzle
{
    private readonly Regex _regex =
        new(@"^(?<Name>\w+)\swould\s(?<LoseOrGain>lose|gain)\s(?<Units>\d+).*\s(?<Neighbour>\w+)\.$",
            RegexOptions.Compiled);
    
    public object Part1(string input)
    {
        var table = Table(input);

        return CalculateHappiness(table);
    }

    public object Part2(string input)
    {
        var table = Table(input);
        
        var participants = table.Keys.Select(tuple => tuple.Item1).Distinct().ToArray();
        foreach (string participant in participants)
        {
            table.Add(("You", participant), 0);
            table.Add((participant, "You"), 0);
        }
        
        return CalculateHappiness(table);
    }

    private int CalculateHappiness(IDictionary<(string, string), int> table)
    {
        var participants = table.Keys.Select(tuple => tuple.Item1).Distinct().ToArray();
        IEnumerable<string[]> stringsEnumerable = participants.Permutations();
        var results = stringsEnumerable
            .Select(ss => CalculateHappiness(table, ss))
            .OrderByDescending(i => i);

        return results.First();
    }

    public int CalculateHappiness(IDictionary<(string, string), int> table, string[] permutation)
    {
        int Loop(int x)
        {
            x += 1;
            return x > permutation.Length - 1 ? 0 : x;
        }

        int previous = permutation.Length - 1;
        int current = 0;
        int next = 1;
        int sum = 0;
        
        do
        {
            sum += table[(permutation[current], permutation[previous])];
            sum += table[(permutation[current], permutation[next])];

            previous = Loop(previous);
            current = Loop(current);
            next = Loop(next);
        } while (current != 0);

        return sum;
    }
    
    public IDictionary<(string, string), int> Table(string input)
    {
        var dictionary = Data(input).Select(d => _regex.Match(d))
            .Where(match => match.Success)
            .Select(match => new KeyValuePair<(string, string),int>((match.Groups["Name"].Value, match.Groups["Neighbour"].Value),
            int.Parse(match.Groups["Units"].Value) * match.Groups["LoseOrGain"].Value switch
            {
                "lose" => -1,
                "gain" => 1,
                _ => 0
            }))
            .ToDictionary(pair => pair.Key, pair => pair.Value);

        return dictionary;
    }

    private IEnumerable<string> Data(string input)
    {
        var reader = new StringReader(input);
        while (reader.ReadLine() is { } line)
        {
            yield return line;
        }
    }
}