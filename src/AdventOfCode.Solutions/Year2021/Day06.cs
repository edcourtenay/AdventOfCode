namespace AdventOfCode.Solutions.Year2021;

[Description("Lanternfish")]
public class Day06 : IPuzzle
{
    public object Part1(string input)
    {
        return Execute(input, 80);
    }

    public object Part2(string input)
    {
        return Execute(input, 256);
    }

    private object Execute(string input, int days)
    {
        var fish = input.Split(',').Select(s => Convert.ToInt64(s));
        var result = SimulateBucket(ToBuckets(fish), days);

        return result.Sum();
    }

    private long[] ToBuckets(IEnumerable<long> fish)
    {
        var buckets = new long[9];
        foreach (var group in fish.GroupBy(i => i))
        {
            buckets[group.Key] = group.Count();
        }

        return buckets;
    }

    private IEnumerable<long> SimulateBucket(IList<long> bucket, int days)
    {
        while (days-- > 0)
        {
            var newFish = bucket[0];
            for (int i = 0; i < bucket.Count - 1; i++)
            {
                bucket[i] = bucket[i + 1];
            }

            bucket[8] = newFish;
            bucket[6] += newFish;
        }

        return bucket;
    }
}