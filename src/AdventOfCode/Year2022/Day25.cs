using System.Text;

namespace AdventOfCode.Year2022;

[Description("Full of Hot Air")]
public class Day25 : IPuzzle
{
    public object Part1(string input)
    {
        long sum = input.ToLines(s => SnafuToInt(s))
            .Sum();
        return IntToSnafu(sum);
    }

    public object Part2(string input)
    {
        throw new NotImplementedException();
    }

    public long SnafuToInt(ReadOnlySpan<char> input)
    {
        long result = 0;

        foreach (char c in input)
        {
            var t = c switch
            {
                '0' or '1' or '2' => c - '0',
                '-' => -1,
                '=' => -2
            };
            result = result * 5 + t;
        }

        return result;
    }

    public string IntToSnafu(long input)
    {
        var queue = new Queue<long>();

        int c = 0;
        while (input + c > 0)
        {
            var r = (input % 5) + c;
            switch (r)
            {
                case > 2:
                    queue.Enqueue(r - 5);
                    c = 1;
                    break;
                default:
                    queue.Enqueue(r);
                    c = 0;
                    break;
            }

            input /= 5;
        }

        return StackToString(queue);
    }

    private string StackToString(Queue<long> queue)
    {
        var sb = new StringBuilder();
        foreach (int i in queue)
        {
            sb.Insert(0, i switch
            {
                -1 => '-',
                -2 => '=',
                _ => i.ToString()
            });
        }

        return sb.ToString();
    }
}