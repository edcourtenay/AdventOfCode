namespace AdventOfCode.Year2023;

[Description("If You Give A Seed A Fertilizer")]
public class Day05 : IPuzzle
{
    public object Part1(string input)
    {
        (IEnumerable<SeedRange> seeds, IEnumerable<IEnumerable<ValueRange>> categoryMaps) = Parse(input,
            longs => longs.Select(l => new SeedRange(l, 1L)));

        return Solve(seeds, categoryMaps);
    }

    public object Part2(string input)
    {
        (IEnumerable<SeedRange> seeds, IEnumerable<IEnumerable<ValueRange>> categoryMaps) = Parse(input,
            longs => longs.Window(2)
                .Select(w => w.ToArray())
                .Select(w => new SeedRange(w[0], w[1])));

        return Solve(seeds, categoryMaps);
    }

    private static long Solve(IEnumerable<SeedRange> ranges, IEnumerable<IEnumerable<ValueRange>> categoryMaps)
    {
        return categoryMaps.Aggregate(ranges,
                (current, categoryMap) =>
                    current.SelectMany(seedRange => ProcessSeedRange(seedRange, categoryMap)))
            .Min(range => range.Start);
    }

    private static IEnumerable<SeedRange> ProcessSeedRange(SeedRange seedRange, IEnumerable<ValueRange> categoryMap)
    {
        var seedRangeStart = seedRange.Start;
        var seedRangeLength = seedRange.Length;
        var valueRanges = categoryMap as ValueRange[] ?? categoryMap.ToArray();

        while (seedRangeLength != 0)
        {
            bool found = false;
            long bestDistance = seedRangeLength;

            foreach (ValueRange valueRange in valueRanges)
            {
                if (valueRange.Source <= seedRangeStart &&
                    seedRangeStart < (valueRange.Source + valueRange.Length))
                {
                    var offset = seedRangeStart - valueRange.Source;
                    var remainder = Math.Min(valueRange.Length - offset, seedRangeLength);
                    yield return new SeedRange(valueRange.Destination + offset, remainder);

                    seedRangeStart += remainder;
                    seedRangeLength -= remainder;
                    found = true;
                    break;
                }

                if (valueRange.Source > seedRangeStart)
                {
                    bestDistance = Math.Min(valueRange.Source - seedRangeStart, bestDistance);
                }
            }

            if (found)
            {
                continue;
            }

            var handle = Math.Min(bestDistance, seedRangeLength);
            yield return new SeedRange(seedRangeStart, handle);
            seedRangeStart += handle;
            seedRangeLength -= handle;
        }
    }

    private static (IEnumerable<SeedRange> seeds, IEnumerable<IEnumerable<ValueRange>> categoryMaps) Parse(string input, Func<IEnumerable<long>, IEnumerable<SeedRange>> func)
    {
        var categoryMaps = new List<IList<ValueRange>>();
        IEnumerable<SeedRange> seedValues = Array.Empty<SeedRange>();

        bool inMap = false;
        var ranges = new List<ValueRange>();

        foreach (var line in input.ToLines((l, i) => (l, i)).Append(("", -1)))
        {
            switch (line)
            {
                case ({ } s, 0):
                    IEnumerable<long> enumerable = s[7..].Split(' ').Select(long.Parse);
                    IEnumerable<SeedRange> valueTuples = func(enumerable);
                    seedValues = valueTuples;
                    break;

                case ({ } s, _) when s.EndsWith("map:"):
                    inMap = true;
                    break;

                case ({ } s, _) when string.IsNullOrEmpty(s) && inMap:
                    categoryMaps.Add(ranges);
                    inMap = false;
                    ranges = [];
                    break;

                case ({ } s, _) when inMap:
                   var numbers = s.Split(' ').Select(long.Parse).ToArray();
                   ranges.Add(new ValueRange(numbers[0], numbers[1], numbers[2]));
                   break;
            }
        }

        return (seedValues, categoryMaps);
    }

    private record ValueRange(long Destination, long Source, long Length);

    public record struct SeedRange(long Start, long Length)
    {
        public static implicit operator SeedRange((long start, long length) tuple) => new(tuple.start, tuple.length);
    }

}