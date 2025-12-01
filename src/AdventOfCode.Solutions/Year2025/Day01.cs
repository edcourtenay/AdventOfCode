namespace AdventOfCode.Solutions.Year2025;

public class Day01 : IPuzzle
{
    public object Part1(string input)
    {
        return Execute(Parse(input), 50, 100);
    }

    public object Part2(string input)
    {
        return Execute(Parse(input), 50, 100, true);
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
    
    private static IEnumerable<int> Normalize(IEnumerable<int> data, int max)
    {
        foreach (var turn in data)
        {
            if (turn >= 0)
            {
                yield return turn;
            }
            else
            {
                var i = Math.Abs(turn);
                var j = (((i / max) + 1) * max) + (max - i % max);

                yield return j;
            }
        }
    }

    private static int Execute(IEnumerable<int> data, int start, int max, bool includeTransitions = false)
    {
        int zeroCount = 0;
        
        foreach (var turn in data)
        {
            int i = (start + turn);
            start = i % max;
            if (start < 0)
            {
                start += max;
            }

            if (includeTransitions)
            {
                if (i <= 0)
                {
                    zeroCount += ((Math.Abs(i) + max) / max);
                }
                else if (i >= max)
                {
                    zeroCount += ((i) / max);
                }
            }
            else if (start == 0)
            {
                zeroCount++;
            }
        }

        return zeroCount;
    }
}