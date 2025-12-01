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
            .Aggregate(new {Pos = Start, Counter = 0}, (acc, item) =>
            {
                var newPos = (acc.Pos + item + Size) % Size;
                return new { Pos = newPos, Counter = acc.Counter + (newPos == 0 ? 1 : 0) };
            }).Counter;
    }

    public object Part2(string input)
    {
        return Parse(input)
            .Aggregate(new { Pos = Start, Counter = 0 }, (acc, item) =>
            {
                var clicks = Math.Abs(item);

                var count = acc.Counter + (clicks / Size);
                var position = acc.Pos + (item % Size);

                if (position >= Size || (position <= 0 && acc.Pos != 0))
                {
                    count++;
                }

                position = (position + Size) % Size;
                
                return new { Pos = position, Counter = count };
            }).Counter;
    }

    private static IEnumerable<int> Parse(string input)
    {
        return input.ToLines(f =>
        {
            var i = Convert.ToInt32(f[1..]);
            return f switch
            {
                ['L', ..] => -i,
                ['R', ..] => i,
                _ => 0
            };
        });
    }
}