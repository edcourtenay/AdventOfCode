using LineData = (string input, int[] springLengths);

namespace AdventOfCode.Solutions.Year2023;

[Description("Hot Springs")]
public class Day12 : IPuzzle
{
    public object Part1(string input)
    {
        return input.ToLines(Parse)
            .AsParallel()
            .Sum(data => CalculateLegalPositions(data.input, data.springLengths));
    }

    public object Part2(string input)
    {
        return input.ToLines(Parse)
            .AsParallel()
            .Select(Unfold)
            .Sum(data => CalculateLegalPositions(data.input, data.springLengths));
    }

    private static LineData Unfold(LineData data) =>
        (string.Join('?', Enumerable.Repeat(data.input, 5)), Enumerable.Repeat(data.springLengths, 5).SelectMany(x => x).ToArray());

    private static LineData Parse(string line)
    {
        var strings = line.Split(' ').ToArray();
        return (input: strings[0], springLengths: strings[1].Split(',').Select(int.Parse).ToArray());
    }

    public static long CalculateLegalPositions(string input, IEnumerable<int> springLengths)
    {
        IReadOnlyList<char> chars = ("." + input + ".").ToCharArray();
        IReadOnlyList<char> springs = DamagedList(springLengths).ToArray();

        long[,] table = new long[chars.Count + 1, springs.Count + 1];
        table[table.GetUpperBound(0), table.GetUpperBound(1)] = 1;

        for (int c = chars.Count - 1; c >= 0; c--)
        {
            for (int s = springs.Count - 1; s >= 0; s--)
            {
                table[c, s] = (chars[c], springs[s]) switch
                {
                    (not '.', '#') => table[c + 1, s + 1],
                    (not '#', '.') => table[c + 1, s + 1] + table[c + 1, s],
                    _ => 0
                };
            }
        }

        return table[0, 0];
    }

    private static IEnumerable<char> DamagedList(IEnumerable<int> springLengths){
        yield return '.';

        foreach (var length in springLengths)
        {
            for (int i = 0; i < length; i++)
            {
                yield return '#';
            }

            yield return '.';
        }
    }
}