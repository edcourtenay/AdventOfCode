using System.Numerics;

namespace AdventOfCode.Solutions.Year2023;

[Description("Mirage Maintenance")]
public class Day09 : IPuzzle
{
    public object Part1(string input)
    {
        return Solve(input, tuple => tuple.right);
    }

    public object Part2(string input)
    {
        return Solve(input, tuple => tuple.left);
    }

    private static long Solve(string input, Func<(long left, long right), long> selector)
    {
        return Enumerable
            .Select<long[], (long left, long right)>(input.ToLines(line => Enumerable.Select<string, long>(line.Split(' '), long.Parse).ToArray()), Extrapolate<long>)
            .Sum(selector);
    }

    public static T[] GenerateSequence<T>(IEnumerable<T> input) where T : INumber<T> =>
        Enumerable.Select<IEnumerable<T>, T[]>(input.SlidingWindow(2), window => Enumerable.ToArray<T>(window)).Select(w => w[1] - w[0]).ToArray();

    public static (T left, T right) Extrapolate<T>(T[] input) where T : INumber<T>
    {
        var leftStack = new Stack<T>();
        var rightStack = new Stack<T>();

        while (input.Any(t => !T.IsZero(t)))
        {
            leftStack.Push(input[0]);
            rightStack.Push(input[^1]);
            input = GenerateSequence(input);
        }
        var left = T.Zero;
        var right = T.Zero;
        while (rightStack.TryPop(out T? rightCurrent) && leftStack.TryPop(out T? leftCurrent))
        {
            left = leftCurrent - left;
            right += rightCurrent;
        }

        return (left, right);
    }
}