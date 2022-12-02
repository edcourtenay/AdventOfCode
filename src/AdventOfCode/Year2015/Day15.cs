using System.Text.RegularExpressions;

namespace AdventOfCode.Year2015;

[Description("Science for Hungry People")]
public partial class Day15 : IPuzzle
{
    private readonly Regex _regex = LineRegex();

    public object Part1(string input)
    {
        Ingredient[] ingredients = Data(input).ToArray();
        var partitions = Partition(100, ingredients.Length);

        return partitions.Select(partition => Calculate(ingredients, partition)).Max();
    }

    public object Part2(string input)
    {
        return string.Empty;
    }

    private IEnumerable<Ingredient> Data(string input)
    {
        var reader = new StringReader(input);
        while (reader.ReadLine() is { } line)
        {
            if (_regex.Match(line) is { Success: true } match)
            {
                yield return new Ingredient()
                {
                    Name = match.Groups["ingredient"].Value,
                    Capacity = int.Parse(match.Groups["capacity"].Value),
                    Durability = int.Parse(match.Groups["durability"].Value),
                    Flavour = int.Parse(match.Groups["flavour"].Value),
                    Texture = int.Parse(match.Groups["texture"].Value),
                    Calories = int.Parse(match.Groups["calories"].Value)
                };
            }
        }
    }

    public int Calculate(Ingredient[] ingredients, int[] teaspoons)
    {
        return teaspoons.Select((t, i) => ingredients[i].TotalScore(t)).Aggregate(1, (x, y) => x * y);
    }

    IEnumerable<int[]> Partition(int n, int k) {
        if (k == 1) {
            yield return new[] { n };
        } else {
            for (var i = 0; i <= n; i++) {
                foreach (var rest in Partition(n - i, k - 1)) {
                    yield return rest.Select(x => x).Append(i).ToArray();
                }
            }
        }
    }

    public record Ingredient
    {
        public required string Name { get; init; }
        public required int Capacity { get; init; }
        public required int Durability { get; init; }
        public required int Flavour { get; init; }
        public required int Texture { get; init; }
        public required int Calories { get; init; }

        public int TotalScore(int teaspoons) =>
            Capacity * teaspoons + Durability * teaspoons + Flavour * teaspoons + Texture * teaspoons +
            Calories * teaspoons;

        public int[] Properties => new[] { Capacity, Durability, Flavour, Texture, Calories };
    }

    [GeneratedRegex(@"^(?<ingredient>\w+):\scapacity\s(?<capacity>-?\d+),\sdurability\s(?<durability>-?\d+),\sflavor\s(?<flavour>-?\d+),\stexture\s(?<texture>-?\d+),\scalories\s(?<calories>-?\d+)$", RegexOptions.Compiled)]
    private static partial Regex LineRegex();
}