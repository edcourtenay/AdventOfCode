using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2022;

[Description("Beacon Exclusion Zone")]
public partial class Day15 : IPuzzle
{
    private static readonly RangeComparer RangeComparer = new();

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
                .Select(range => (range!.Value.from, range.Value.to))
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

    private static IEnumerable<(int start, int end)> MergeRanges(IEnumerable<(int start, int end)> ranges)
    {
        var sortedRanges = new SortedSet<(int start, int end)>(ranges, RangeComparer);

        (int start, int end)? currentMergedRange = null;

        foreach (var range in sortedRanges)
        {
            if (currentMergedRange == null)
            {
                currentMergedRange = range;
            }
            else
            {
                if (range.start <= currentMergedRange.Value.end + 1)
                {
                    currentMergedRange = (currentMergedRange.Value.start, Math.Max(currentMergedRange.Value.end, range.end));
                }
                else
                {
                    yield return currentMergedRange.Value;
                    currentMergedRange = range;
                }
            }
        }

        yield return currentMergedRange!.Value;
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

        [GeneratedRegex(@"Sensor at x=(?<sensorX>\-?\d+), y=(?<sensorY>\-?\d+): closest beacon is at x=(?<beaconX>\-?\d+), y=(?<beaconY>\-?\d+)", RegexOptions.Compiled)]
        private static partial Regex MyRegex();
    }
}

public class RangeComparer : IComparer<(int start, int end)>
{
    public int Compare((int start, int end) lhs, (int start, int end) rhs)
    {
        return lhs.start == rhs.start
            ? lhs.end.CompareTo(rhs.end)
            : lhs.start.CompareTo(rhs.start);
    }
}