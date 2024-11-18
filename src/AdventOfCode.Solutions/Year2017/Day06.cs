namespace AdventOfCode.Solutions.Year2017;

[Description("Memory Reallocation")]
public class Day06 : IPuzzle
{
    public object Part1(string input)
    {
        var banks = input.Split(' ', '\t')
            .Select(int.Parse)
            .ToArray();

        return Process(banks).Count;
    }

    public object Part2(string input)
    {
        var banks = input.Split(' ', '\t')
            .Select(int.Parse)
            .ToArray();

        var found = Process(banks).Banks;
        return Process(found).Count;
    }

    private (int Count, int[] Banks) Process(int[] banks)
    {
        var seen = new HashSet<string>();
        do
        {
            seen.Add(ToKey(banks));

            int index = FindBank(banks);
            var units = banks[index];
            banks[index] = 0;

            while (units > 0)
            {
                index = (index + 1) % banks.Length;
                banks[index]++;
                units--;
            }
        } while (!seen.Contains(ToKey(banks)));

        return (seen.Count, banks);
    }

    private static string ToKey(int[] ints)
    {
        return ints
            .Select(i => i.ToString())
            .Aggregate((s0, s1) => $"{s0}-{s1}");
    }

    private int FindBank(int[] banks)
    {
        return banks
            .Select((a, i) => new { Item = a, Index = i })
            .OrderByDescending(i => i.Item)
            .ThenBy(i => i.Index)
            .First()
            .Index;
    }
}