using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode.Solutions.Year2015;

[SuppressMessage("ReSharper", "UnusedType.Global")]
[Description("Not Quite Lisp")]
public class Day01 : IPuzzle
{
    public object Part1(string input)
    {
        return input.Select(ParseChar).Sum();
    }

    public object Part2(string input)
    {
        using var enumerator = input.Select(ParseChar).GetEnumerator();
        int index = 0;
        int floor = 0;
        while (enumerator.MoveNext())
        {
            index++;
            floor += enumerator.Current;
            if (floor < 0)
            {
                break;
            }
        }

        return index;
    }

    private int ParseChar(char c) =>
        c switch
        {
            '(' => 1,
            ')' => -1,
            _ => 0
        };
}