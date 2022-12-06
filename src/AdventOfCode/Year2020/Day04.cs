using System.Text.RegularExpressions;

namespace AdventOfCode.Year2020;

[Description("Passport Processing")]
public partial class Day04 : IPuzzle
{
    public object Part1(string input)
    {
        IEnumerable<string> passports = input.ToLines()
            .ToSequences(string.IsNullOrEmpty)
            .Select(s => s.Aggregate("", (s1, s2) => $"{s1} {s2}"));

        return passports.Count(IsValid);
    }

    public object Part2(string input)
    {
        IEnumerable<string> passports = input.ToLines()
            .ToSequences(string.IsNullOrEmpty)
            .Select(s => s.Aggregate("", (s1, s2) => $"{s1} {s2}"));

        return passports.Count(IsValid2);
    }

    private bool IsValid(string s)
    {
        string[] keys = {"byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid"};
        bool isValid = KeyValueRegex()
            .Matches(s)
            .ToDictionary(match => match.Groups["key"].Value, match => match.Groups["value"].Value)
            .Keys
            .Intersect(keys)
            .Count() == keys.Length;
        return isValid;
    }

    public static bool IsValid2(string s)
    {
        var ops = new (string Key, string Pattern, Func<Match, bool> Predicate)[]
        {
            ("byr", @"(\d{4})", m => int.Parse(m.Groups[0].Value) is >= 1920 and <= 2002),
            ("iyr", @"(\d{4})", m => int.Parse(m.Groups[0].Value) is >= 2010 and <= 2020),
            ("eyr", @"(\d{4})", m => int.Parse(m.Groups[0].Value) is >= 2020 and <= 2030),
            ("hgt", @"(\d+)(cm|in)", m => (m.Groups[2].Value == "cm" && (int.Parse(m.Groups[1].Value) is >= 150 and <= 193)) || (m.Groups[2].Value == "in" && (int.Parse(m.Groups[1].Value) is >= 59 and <= 76))),
            ("hcl", @"#([0-9a-f]{6})", _ => true),
            ("ecl", @"(amb|blu|brn|gry|grn|hzl|oth)", _ => true),
            ("pid", @"(\d{9})", _ => true),
        };
        string[] keys = ops.Select(tuple => tuple.Key).Distinct().ToArray();

        Dictionary<string,string> dictionary = KeyValueRegex()
            .Matches(s)
            .ToDictionary(match => match.Groups["key"].Value, match => match.Groups["value"].Value);

        bool hasRequiredKeys = dictionary
            .Keys
            .Intersect(keys)
            .Count() == keys.Length;

        return hasRequiredKeys && dictionary
            .Keys
            .Aggregate(true, (current1, key) => ops
                .Where(tuple => tuple.Key == key)
                .Aggregate(current1, (current, tuple) => Regex
                        .Match(dictionary[key], $"^{tuple.Pattern}$") switch {
                            { Success: true } match => current && tuple.Predicate(match),
                            _ => false
            }));
    }

    [GeneratedRegex(@"(?<key>\w{3}):(?<value>[A-Za-z0-9#]+)", RegexOptions.Compiled)]
    private static partial Regex KeyValueRegex();
}