using System.Text.RegularExpressions;

using Range = (long from, long to);

namespace AdventOfCode.Solutions.Year2025;

[Description("Gift Shop")]
public partial class Day02 : IPuzzle
{
    public object Part1(string input) =>
        Parse(input).SelectMany(SumInvalidIDsInRangeNoDuplicates).Sum();

    public object Part2(string input) =>
        Parse(input).SelectMany(SumInvalidIDsInRange).Sum();

    static IEnumerable<long> SumInvalidIDsInRangeNoDuplicates(Range r)
    {
        if (r.from > r.to)
        {
            yield break;
        }

        int maxDigits = (r.to == 0) ? 1 : (int)Math.Log10(r.to) + 1;

        for (int numDigits = 2; numDigits <= maxDigits; numDigits += 2)
        {
            int halfDigits = numDigits / 2;
            long multiplier = (long)Math.Pow(10, halfDigits) + 1;
            long minPattern = (long)Math.Pow(10, halfDigits - 1);
            long maxPattern = (long)Math.Pow(10, halfDigits) - 1;

            long patternStart = Math.Max(minPattern, (r.from + multiplier - 1) / multiplier);
            long patternEnd = Math.Min(maxPattern, r.to / multiplier);

            if (patternStart <= patternEnd)
            {
                long count = patternEnd - patternStart + 1;
                yield return (patternStart + patternEnd) * count / 2 * multiplier;
            }
        }
    }

    static IEnumerable<long> SumInvalidIDsInRange(Range r)
    {
        if (r.from > r.to)
        {
            yield break;
        }

        var patternsSeen = new Dictionary<(long, int), bool>();
        int maxDigits = (r.to == 0) ? 1 : (int)Math.Log10(r.to) + 1;

        for (int patternLen = 1; patternLen <= maxDigits; patternLen++)
        {
            for (int numReps = 2; patternLen * numReps <= maxDigits; numReps++)
            {
                long multiplier = Enumerable.Range(0, numReps)
                    .Aggregate(0L, (acc, _) => acc * (long)Math.Pow(10, patternLen) + 1);

                long minPattern = (patternLen == 1) ? 1 : (long)Math.Pow(10, patternLen - 1);
                long maxPattern = (long)Math.Pow(10, patternLen) - 1;

                long patternStart = Math.Max(minPattern, (r.from + multiplier - 1) / multiplier);
                long patternEnd = Math.Min(maxPattern, r.to / multiplier);

                if (patternStart > patternEnd)
                {
                    continue;
                }

                foreach (long pattern in Enumerable.Range((int)patternStart, (int)(patternEnd - patternStart + 1)))
                {
                    if (!patternsSeen.TryGetValue((pattern, patternLen), out bool isPrimitive))
                    {
                        isPrimitive = IsPrimitive(pattern, patternLen);
                        patternsSeen[(pattern, patternLen)] = isPrimitive;
                    }

                    if (isPrimitive)
                    {
                        yield return pattern * multiplier;
                    }
                }
            }
        }
    }

    static bool IsPrimitive(long pattern, int patternLen)
    {
        if (patternLen == 1)
        {
            return true;
        }

        for (int divisor = 1; divisor <= patternLen / 2; divisor++)
        {
            if (patternLen % divisor != 0)
            {
                continue;
            }

            long subPattern = pattern / (long)Math.Pow(10, patternLen - divisor);
            long expected = Enumerable.Range(0, patternLen / divisor)
                .Aggregate(0L, (acc, _) => acc * (long)Math.Pow(10, divisor) + subPattern);

            if (expected == pattern)
            {
                return false;
            }
        }

        return true;
    }

    private static IEnumerable<Range> Parse(string input) =>
        input.Split(",").Select(part =>
        {
            var match = RangeRegex().Match(part);
            return (long.Parse(match.Groups["from"].Value), long.Parse(match.Groups["to"].Value));
        });

    [GeneratedRegex(@"(?<from>\d+)\-(?<to>\d+)")]
    private static partial Regex RangeRegex();
}
