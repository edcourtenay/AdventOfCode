namespace AdventOfCode.Solutions.Year2025;

using System.Linq;

[Description("Secret Entrance")]
public class Day01 : IPuzzle
{
    private const int Start = 50;
    private const int Size = 100;


    public object Part1(string input)
    {
        return Parse(input)
            .Aggregate((Pos: Start, Counter: 0), (acc, item) =>
            {
                var newPos = (acc.Pos + item + Size) % Size;
                return (Pos: newPos, Counter: acc.Counter + (newPos == 0 ? 1 : 0));
            }).Counter;
    }

    public object Part2(string input)
    {
        return Parse(input)
            .Aggregate((Pos: Start, Counter: 0), (acc, item) =>
            {
                var count = acc.Counter + (Math.Abs(item) / Size);
                var position = acc.Pos + (item % Size);

                if (position >= Size || (position <= 0 && acc.Pos != 0))
                {
                    count++;
                }

                return (Pos: (position + Size) % Size, Counter: count);
            }).Counter;
    }

    private static IEnumerable<int> Parse(string input)
    {
        return input.ToLines(f =>
        {
            return f switch
            {
                ['L', .. var xs] => -Convert.ToInt32(xs),
                ['R', .. var xs] => Convert.ToInt32(xs),
                _ => 0
            };
        });
    }
}