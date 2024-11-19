using System.Text.RegularExpressions;

using Instruction = (char direction, int count);
using Edge = ((long x, long y) from, (long x, long y) to);

namespace AdventOfCode.Solutions.Year2023;

[Description("Lavaduct Lagoon")]
public sealed partial class Day18 : IPuzzle
{
    [GeneratedRegex(@"(?'direction'[UDLR])\s(?'count'\d+)\s\(#(?'colour'[0-9a-f]{6})\)", RegexOptions.Compiled)]
    private static partial Regex LineRegex();

    public object Part1(string input)
    {
        return Solve(input, match =>
        {
            char direction = match.Groups["direction"].Value[0];
            int count = int.Parse(match.Groups["count"].Value);
            return (direction, count);
        });
    }

    public object Part2(string input)
    {
        return Solve(input, match =>
        {
            int colour = Convert.ToInt32(match.Groups["colour"].Value, 16);
            char direction = (colour & 0xF) switch
            {
                0x0 => 'R',
                0x1 => 'U',
                0x2 => 'L',
                0x3 => 'D',
                _ => throw new InvalidOperationException()
            };
            int count = (colour >> 4);

            return (direction, count);
        });
    }

    private static long Solve(string input, Func<Match, Instruction> matchToInstruction)
    {
        (long perimeter, long area) = GetEdges(input.ToLines(line => LineRegex().Match(line))
                .Where(match => match.Success)
                .Select(matchToInstruction))
            .Aggregate((p: 0L, a: 0L),
                (acc, edge) => (acc.p + Math.Abs(edge.to.x - edge.from.x) + Math.Abs(edge.to.y - edge.from.y),
                    acc.a + ((edge.from.x + edge.to.x) * (edge.from.y - edge.to.y))),
                acc => ((acc.p / 2) + 1, Math.Abs(acc.a / 2)));

        return perimeter + area;
    }

    private static IEnumerable<Edge> GetEdges(IEnumerable<Instruction> data)
    {
        var from = (x: 0L, y: 0L);
        foreach (var instruction in data)
        {
            var to = (instruction.direction) switch
            {
                'U' => (from.x, from.y - instruction.count),
                'D' => (from.x, from.y + instruction.count),
                'L' => (from.x - instruction.count, from.y),
                'R' => (from.x + instruction.count, from.y),
                _ => throw new InvalidOperationException()
            };

            yield return (from, to);
            from = to;
        }
    }
}