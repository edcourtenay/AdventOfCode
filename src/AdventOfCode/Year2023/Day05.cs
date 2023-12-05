namespace AdventOfCode.Year2023;

[Description("If You Give A Seed A Fertilizer")]
public class Day05 : IPuzzle
{
    public object Part1(string input)
    {
        (SeedRange[] seeds, HashSet<CategoryMap> categoryMaps) = Parse(input,
            longs => longs.Select(l => new SeedRange(l, 1L)));

        return Solve(seeds, categoryMaps);
    }

    public object Part2(string input)
    {
        (SeedRange[] seeds, HashSet<CategoryMap> categoryMaps) = Parse(input,
            longs => longs.Window(2)
                .Select(w => w.ToArray())
                .Select(w => new SeedRange(w[0], w[1])));

        return Solve(seeds, categoryMaps);
    }

    private static long Solve(SeedRange[] ranges, HashSet<CategoryMap> categoryMaps)
    {
        return categoryMaps.Aggregate(ranges,
                (current, categoryMap) =>
                    current.SelectMany(seedRange => ProcessSeedRange(seedRange, categoryMap)).ToArray())
            .Min(range => range.Start);
    }

    private static IEnumerable<SeedRange> ProcessSeedRange(SeedRange seedRange, CategoryMap categoryMap)
    {
        var seedRangeStart = seedRange.Start;
        var seedRangeLength = seedRange.Length;
        while (seedRangeLength != 0)
        {
            bool found = false;
            long bestDistance = seedRangeLength;

            foreach (ValueRange valueRange in categoryMap.Ranges)
            {
                if (valueRange.Source <= seedRangeStart &&
                    seedRangeStart < (valueRange.Source + valueRange.Length))
                {
                    var offset = seedRangeStart - valueRange.Source;
                    var remainder = long.Min(valueRange.Length - offset, seedRangeLength);
                    yield return (valueRange.Destination + offset, remainder);

                    seedRangeStart += remainder;
                    seedRangeLength -= remainder;
                    found = true;
                    break;
                }

                if (valueRange.Source > seedRangeStart)
                {
                    bestDistance = long.Min(valueRange.Source - seedRangeStart, bestDistance);
                }
            }

            if (found)
            {
                continue;
            }

            var handle = long.Min(bestDistance, seedRangeLength);
            yield return (seedRangeStart, handle);
            seedRangeStart += handle;
            seedRangeLength -= handle;
        }
    }

    private static (SeedRange[] seeds, HashSet<CategoryMap> categoryMaps) Parse(string input, Func<IEnumerable<long>, IEnumerable<SeedRange>> func)
    {
        var categoryMaps = new HashSet<CategoryMap>();
        SeedRange[] seedValues = Array.Empty<SeedRange>();

        bool inMap = false;
        var ranges = new List<ValueRange>();

        foreach (var line in input.ToLines((l, i) => (l, i)).Append(("", -1)))
        {
            switch (line)
            {
                case ({ } s, 0):
                    IEnumerable<long> enumerable = s[7..].Split(' ').Select(long.Parse);
                    IEnumerable<SeedRange> valueTuples = func(enumerable);
                    seedValues = valueTuples.ToArray();
                    break;

                case ({ } s, _) when s.EndsWith("map:"):
                    inMap = true;
                    break;

                case ({ } s, _) when string.IsNullOrEmpty(s) && inMap:
                    categoryMaps.Add(new CategoryMap(ranges));
                    inMap = false;
                    ranges = new List<ValueRange>();
                    break;

                case ({ } s, _) when inMap:
                   var numbers = s.Split(' ').Select(long.Parse).ToArray();
                   ranges.Add(new ValueRange(numbers[0], numbers[1], numbers[2]));
                   break;
            }
        }

        return (seedValues, categoryMaps);
    }


    private record CategoryMap(IList<ValueRange> Ranges);

    private record ValueRange(long Destination, long Source, long Length);

    public record struct SeedRange(long Start, long Length)
    {
        public static implicit operator SeedRange((long start, long length) tuple) => new(tuple.start, tuple.length);
    }

}