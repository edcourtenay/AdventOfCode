using System.Text.RegularExpressions;

namespace AdventOfCode.Year2023;

[Description("Gear Ratios")]
public partial class Day03 : IPuzzle
{
    [GeneratedRegex(@"(?'digit')\d+|(?'symbol'[^\.\n\r])", RegexOptions.Compiled)]
    private static partial Regex Regex();

    public object Part1(string input)
    {
        return Solve(input, FindPartNumbers);
    }

    public object Part2(string input)
    {
        return Solve(input, FindGearRatios);
    }

    private static object Solve(string input, Func<IList<ValueCapture>, IList<SymbolCapture>, IEnumerable<int>> func)
    {
        var lines = input.ToLines()
            .Select((s, i) => (s, i));

        List<ValueCapture> values = [];
        List<SymbolCapture> symbols = [];

        foreach ((string? line, int y) in lines)
        {
            foreach (Match match in Regex().Matches(line))
            {
                if (match.Groups["digit"].Success)
                {
                    values.Add(new ValueCapture(match.Index, y, match.Length, int.Parse(match.Value)));
                }
                else
                {
                    symbols.Add(new SymbolCapture(match.Index, y, match.Value[0]));
                }
            }
        }

        IEnumerable<int> allowedValues = func(values, symbols);

        return allowedValues.Sum();
    }

    private static IEnumerable<int> FindPartNumbers(IList<ValueCapture> values, IList<SymbolCapture> symbols)
    {
        return values.Where(v =>
            symbols.FirstOrDefault(SymbolAdjacentToValuePredicate(v)) is not null
        ).Select(v => v.Value);
    }

    private static IEnumerable<int> FindGearRatios(IList<ValueCapture> values, IList<SymbolCapture> symbols)
    {
        return symbols.Where(s => s.Symbol == '*')
            .Select(s => values.Where(ValueAdjacentToSymbolPredicate(s)).ToArray())
            .Where(values1 => values1.Length == 2)
            .Select(values2 => values2[0].Value * values2[1].Value);
    }

    private static Func<SymbolCapture, bool> SymbolAdjacentToValuePredicate(ValueCapture v) =>
        s => s.X >= v.X -1 && s.X <= v.X + v.Length && s.Y >= v.Y - 1 && s.Y <= v.Y + 1;

    private static Func<ValueCapture, bool> ValueAdjacentToSymbolPredicate(SymbolCapture s) =>
        v => s.X >= v.X -1 && s.X <= v.X + v.Length && s.Y >= v.Y - 1 && s.Y <= v.Y + 1;


    public record ValueCapture(int X, int Y, int Length, int Value);
    public record SymbolCapture(int X, int Y, char Symbol);
}