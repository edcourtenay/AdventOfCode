namespace AdventOfCode.Solutions.Year2025;

[Description("Lobby")]
public class Day03: IPuzzle
{
    public object Part1(string input) =>
        input.ToLines(s => CalculateJoltage(s, 2))
            .Aggregate(0UL, (sum, val) => sum + val);

    public object Part2(string input) =>
        input.ToLines(s => CalculateJoltage(s, 12))
            .Aggregate(0UL, (sum, val) => sum + val);

    public static ulong CalculateJoltage(string bank, int length)
    {
        var result = new char[length];
        int from = 0;

        for (int index = 0; index < length; index++)
        {
            int to = bank.Length - length + index;
            var current = (Char: '0', Index: to);

            for (; to >= from; to--)
            {
                if (current.Char <= bank[to])
                {
                    current = (bank[to], to);
                }
            }

            from = current.Index + 1;
            result[index] = current.Char;
        }

        return ulong.Parse(result);
    }
}