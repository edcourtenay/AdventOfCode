using System.Text;

namespace AdventOfCode.Solutions.Year2021;

[Description("Syntax Scoring")]
public class Day10 : IPuzzle
{
    public object Part1(string input)
    {
        return Enumerable
            .Where<(bool isCorrupted, char corrupt, string missing)>(input.ToLines(Validate), tuple => tuple.isCorrupted)
            .Sum(tuple => ScoreBracket(tuple.corrupt));
    }

    public object Part2(string input)
    {
        var scores = Enumerable
            .Where<(bool isCorrupted, char corrupt, string missing)>(input.ToLines(Validate), tuple => !tuple.isCorrupted)
            .Select(tuple => ScoreRemainder(tuple.missing))
            .Order()
            .ToArray();

        var middle = (scores.Length - 1) / 2;

        return scores[middle];
    }

    public (bool isCorrupted, char corrupt, string missing) Validate(string input)
    {
        var stack = new Stack<char>();
        foreach (var c in input)
        {
            if (IsOpen(c))
            {
                stack.Push(c);
            }
            else if (IsClose(c))
            {
                if (stack.Count == 0)
                {
                    return (true, c, "");
                }

                var open = stack.Pop();
                if (MapOpenToClose(open) != c)
                {
                    return (true, c, "");
                }
            }
        }

        var sb = new StringBuilder();
        while (stack.Count > 0)
        {
            sb.Append(MapOpenToClose(stack.Pop()));
        }
        return (false, default, sb.ToString());
    }

    public static char MapOpenToClose(char c) => c switch
    {
        '(' => ')',
        '[' => ']',
        '{' => '}',
        '<' => '>',
        _ => throw new ArgumentOutOfRangeException(nameof(c), c, "Not an open bracket")
    };

    public static char MapCloseToOpen(char c) => c switch
    {
        ')' => '(',
        ']' => '[',
        '}' => '{',
        '>' => '<',
        _ => throw new ArgumentOutOfRangeException(nameof(c), c, "Not a close bracket")
    };

    public static bool IsOpen(char c) => c switch
    {
        '(' or '[' or '{' or '<' => true,
        ')' or ']' or '}' or '>' => false,
        _ => throw new ArgumentOutOfRangeException(nameof(c), c, "Not a bracket")
    };

    public static bool IsClose(char c) => c switch
    {
        ')' or ']' or '}' or '>' => true,
        '(' or '[' or '{' or '<' => false,
        _ => throw new ArgumentOutOfRangeException(nameof(c), c, "Not a bracket")
    };

    public static long ScoreBracket(char bracket)
    {
        return bracket switch
        {
            ')' => 3,
            ']' => 57,
            '}' => 1197,
            '>' => 25137,
            _ => throw new ArgumentOutOfRangeException(nameof(bracket), bracket, "Not a bracket")
        };
    }

    public static long ScoreRemainder(string remainder)
    {
        return remainder.Aggregate(0L, (acc, c) => (acc * 5) + c switch
        {
            ')' => 1,
            ']' => 2,
            '}' => 3,
            '>' => 4,
            _ => throw new ArgumentOutOfRangeException(nameof(c), c, "Not a bracket")
        });
    }
}