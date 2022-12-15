using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2022;

[Description("Beacon Exclusion Zone")]
public partial class Day15 : IPuzzle
{
    public object Part1(string input)
    {
        return SolvePart1(input, 2_000_000);
    }

    public int SolvePart1(string input, int row)
    {
        var inputs = input.ToLines()
            .Select(SensorBeaconPair.ParseLine).ToArray();

        var list = inputs
            .Select(pair => pair.RangeForRow(row))
            .Where(range => range != null)
            .Select(range => (range!.Value.from, range.Value.to));

        var merged = MergeRanges(list)
            .SelectMany(range => Enumerable.Range(range.start, range.end - range.start + 1))
            .Except(inputs.Where(pair => pair.Beacon.Y == row).Select(pair => pair.Beacon.X))
            .Count();

        return merged;
    }

    public object Part2(string input)
    {
        return SolvePart2(input, (4_000_000, 4_000_000));
    }

    public long SolvePart2(string input, (int x, int y) max)
    {
        var inputs = input.ToLines()
            .Select(SensorBeaconPair.ParseLine).ToArray();

        for (int row = 0; row <= max.y; row++)
        {
            var list = inputs
                .Select(pair => pair.RangeForRow(row))
                .Where(range => range != null)
                .Select(range => (range.Value.from, range.Value.to))
                .Where(range => 0 <= range.from && range.from <= max.x || 0 <= range.to && range.to <= max.y)
                .Select(range => (Math.Max(0, range.from), Math.Min(max.y, range.to)));

            var merged = MergeRanges(list);

            foreach (((int start, int end) First, (int start, int end) Second) pair in merged.Pairwise())
            {
                if (pair.First.end + 1 == pair.Second.start - 1)
                {
                    return ((pair.First.end + 1) * 4_000_000L) + row;
                }
            }
        }

        return 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static IEnumerable<(int start, int end)> MergeRanges(IEnumerable<(int start, int end)> ranges)
    {
        var ordered = ranges.OrderBy(tuple => tuple.start);
        var mergedRanges = new List<(int start, int end)>();

        foreach (var currentRange in ordered)
        {
            if (mergedRanges.Count == 0)
            {
                mergedRanges.Add(currentRange);
                continue;
            }

            var lastMergedRange = mergedRanges[^1];

            if (currentRange.start - 1 <= lastMergedRange.end)
            {
                lastMergedRange.end = Math.Max(currentRange.end, lastMergedRange.end);
                mergedRanges[^1] = lastMergedRange;
            }
            else
            {
                mergedRanges.Add(currentRange);
            }
        }

        return mergedRanges;
    }


    public partial record SensorBeaconPair
    {
        private readonly Lazy<int> _manhattanDistance;

        private SensorBeaconPair()
        {
            _manhattanDistance = new Lazy<int>(() => Math.Abs(Beacon.X - Sensor.X) + Math.Abs(Beacon.Y - Sensor.Y));
        }

        public required (int X, int Y) Sensor { get; init; }
        public required (int X, int Y) Beacon { get; init; }

        public int ManhattanDistance => _manhattanDistance.Value;

        public (int from, int to)? RangeForRow(int y)
        {
            if (y < Sensor.Y - ManhattanDistance || y > Sensor.Y + ManhattanDistance)
            {
                return null;
            }

            int h = Math.Abs(Sensor.Y - y);
            return (Sensor.X - ManhattanDistance + h, Sensor.X + ManhattanDistance - h);
        }

        public static SensorBeaconPair ParseLine(string line)
        {
            if (MyRegex().Match(line) is { Success: true } match)
            {
                return new SensorBeaconPair
                {
                    Sensor = (int.Parse(match.Groups["sensorX"].Value), int.Parse(match.Groups["sensorY"].Value)),
                    Beacon = (int.Parse(match.Groups["beaconX"].Value), int.Parse(match.Groups["beaconY"].Value))
                };
            }

            throw new ApplicationException($"Could not parse line: {line}");
        }

        [GeneratedRegex("Sensor at x=(?<sensorX>\\-?\\d+), y=(?<sensorY>\\-?\\d+): closest beacon is at x=(?<beaconX>\\-?\\d+), y=(?<beaconY>\\-?\\d+)", RegexOptions.Compiled)]
        private static partial Regex MyRegex();
    }
}