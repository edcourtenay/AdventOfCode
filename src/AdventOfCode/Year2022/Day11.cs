using System.Linq.Expressions;
using System.Numerics;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2022;

[Description("Monkey in the Middle")]
public partial class Day11 : IPuzzle
{
    public object Part1(string input)
    {
        var monkeys = ParseMonkeys(input);

        return Solve(monkeys, 20, l => l / 3);
    }

    public object Part2(string input)
    {
        var monkeys = ParseMonkeys(input);

        var lcd = LeastCommonMultiple(monkeys.Values.Select(monkey => monkey.DivisibleBy));

        return Solve(monkeys, 10000, l => l % lcd);
    }

    private Dictionary<int, Monkey> ParseMonkeys(string input)
    {
        return input.ToLines()
            .ToSequences(string.IsNullOrEmpty)
            .Select(ParseMonkey)
            .ToDictionary(m => m.Id, m => m);
    }

    private static long Solve(Dictionary<int, Monkey> monkeys, int iterations, Func<long, long> func)
    {
        for (int i = 0; i < iterations; i++)
        {
            DoRound(monkeys, func);
        }

        return monkeys.Values.Select(m => m.InspectionCount)
            .OrderDescending()
            .Take(2)
            .Aggregate((x, y) => x * y);
    }

    private static T GreatestCommonDivisor<T>(T n1, T n2) where T : INumber<T>
    {
        while (true)
        {
            if (n2 == T.Zero) return n1;
            var n3 = n1;
            n1 = n2;
            n2 = n3 % n2;
        }
    }

    private static T LeastCommonMultiple<T>(IEnumerable<T> numbers) where T : INumber<T>
    {
        return numbers.Aggregate((n1, n2) => n1 * n2 / GreatestCommonDivisor(n1, n2));
    }

    private static void DoRound(Dictionary<int, Monkey> monkeys, Func<long, long> func)
    {
        foreach (int key in monkeys.Keys)
        {
            var monkey = monkeys[key];

            var items = new List<long>(monkey.Items);
            monkey.Items.Clear();

            foreach (int item in items)
            {
                var level = monkey.Operation(item);
                level = func(level);

                int destination = level % monkey.DivisibleBy == 0
                    ? monkey.IfTrue
                    : monkey.IfFalse;

                monkeys[destination].Items.Add(level);
                monkey.InspectionCount++;
            }
        }
    }

    private static Monkey ParseMonkey(IEnumerable<string> block)
    {
        var lines = block.ToArray();

        return new Monkey
        {
            Id = int.Parse(IdRegex().Match(lines[0]).Groups["id"].Value),
            Items = ItemsRegex().Matches(lines[1])[0].Groups["item"].Captures.Select(s => long.Parse(s.Value)).ToList(),
            Operation = ParseOperation(lines[2]),
            DivisibleBy = int.Parse(DivisibleRegex().Match(lines[3]).Groups["div"].Value),
            IfTrue = int.Parse(ThrowRegex().Match(lines[4]).Groups["id"].Value),
            IfFalse = int.Parse(ThrowRegex().Match(lines[5]).Groups["id"].Value)
        };
    }

    private static Func<long, long> ParseOperation(string line)
    {
        var match = OperationRegex().Match(line);

        ParameterExpression oldParameter = Expression.Parameter(typeof(long), "old");

        var lValue = ParseVal(match.Groups["lvalue"].Value) ?? oldParameter;
        var rValue = ParseVal(match.Groups["rvalue"].Value) ?? oldParameter;

        var expression = match.Groups["op"].Value switch
        {
            "+" => Expression.Add(lValue, rValue),
            "*" => Expression.Multiply(lValue, rValue),
            _ => throw new ArgumentOutOfRangeException()
        };

        var lambda = Expression.Lambda(expression, oldParameter);

        return (Func<long, long>)lambda.Compile();
    }

    private static Expression? ParseVal(string val)
    {
        return long.TryParse(val, out long result) ? Expression.Constant(result) : null;
    }

    public class Monkey
    {
        public required int Id { get; init; }
        public required Func<long, long> Operation { get; init; }
        public required int DivisibleBy { get; init; }
        public required int IfTrue { get; init; }
        public required int IfFalse { get; init; }
        public required List<long> Items { get; init; }

        public long InspectionCount { get; set; } = 0;
    }

    [GeneratedRegex(@"^Monkey (?<id>\d+):$", RegexOptions.Compiled)]
    private static partial Regex IdRegex();
    [GeneratedRegex(@"^\s+Starting items: ((?<item>\d+)(,\s)?)+$", RegexOptions.Compiled)]
    private static partial Regex ItemsRegex();
    [GeneratedRegex(@"^\s+Operation: new = (?<lvalue>old|\d+)\s(?<op>\*|\+)\s(?<rvalue>old|\d+)$", RegexOptions.Compiled)]
    private static partial Regex OperationRegex();
    [GeneratedRegex(@"^\s+Test: divisible by (?<div>\d+)$", RegexOptions.Compiled)]
    private static partial Regex DivisibleRegex();
    [GeneratedRegex(@"^\s+If (true|false): throw to monkey (?<id>\d+)$", RegexOptions.Compiled)]
    private static partial Regex ThrowRegex();
}