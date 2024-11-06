namespace AdventOfCode.Year2021;

[Description("Binary Diagnostic")]
public class Day03 : IPuzzle
{
    public object Part1(string input)
    {
        var lines = input
            .ToLines(d => d.ToArray())
            .Pivot()
            .Select(line => line.Aggregate((zeroes: 0, ones: 0), (current, c) => c switch
            {
                '0' => current with { zeroes = current.zeroes + 1 },
                '1' => current with { ones = current.ones + 1 },
                _ => throw new ArgumentOutOfRangeException(nameof(c), c, null)
            }))
            .ToArray();

        var epsilon = lines.Select(MostCommon).Aggregate(0, (i, j) => i * 2 + j);
        var gamma = lines.Select(LeastCommon).Aggregate(0, (i, j) => i * 2 + j);

        return gamma * epsilon;

        int MostCommon((int zeroes, int ones) tuple) => tuple.ones > tuple.zeroes ? 1 : 0;

        int LeastCommon((int zeroes, int ones) tuple) => tuple.ones < tuple.zeroes ? 1 : 0;
    }

    public object Part2(string input)
    {
        var bucket1 = input.ToLines().ToArray();
        var bucket2 = input.ToLines().ToArray();

        (int value1, int value2) = Process(0, bucket1, bucket2);
        
        
        return value1 * value2;
    }

    public (int, int) Process(int index, string[] bucket1, string[] bucket2)
    {
        while (true)
        {
            switch (bucket1.Length)
            {
                case 1 when bucket2.Length == 1:
                    return (Convert.ToInt32(bucket1[0], 2), Convert.ToInt32(bucket2[0], 2));
                case > 1:
                    bucket1 = ProcessBucket(index, bucket1, true);
                    break;
            }

            if (bucket2.Length > 1)
            {
                bucket2 = ProcessBucket(index, bucket2, false);
            }

            index++;
        }
    }

    private static string[] ProcessBucket(int index, string[] bucket, bool most)
    {
        var dict = bucket.GroupBy(s => s[index])
            .ToDictionary(g => g.Key, g => g.ToArray());

        int zeroCount = dict['0'].Length;
        int onesCount = dict['1'].Length;

        return most ? zeroCount == onesCount ? dict['1'] : zeroCount > onesCount ? dict['0'] : dict['1'] :
            zeroCount == onesCount ? dict['0'] :
            zeroCount < onesCount ? dict['0'] : dict['1'];
    }
}