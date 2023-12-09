using System.Numerics;

namespace AdventOfCode.Year2023;

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

    private long Solve(string input, Func<(long left, long right), long> selector)
    {
        return input.ToLines(line => line.Split(' ').Select(long.Parse).ToArray())
            .Select(Extrapolate)
            .Sum(selector);
    }

    public T[] GenerateSequence<T>(IEnumerable<T> input) where T : INumber<T> =>
        input.SlidingWindow(2).Select(window => window.ToArray()).Select(w => w[1] - w[0]).ToArray();

    public (T left, T right) Extrapolate<T>(T[] input) where T : INumber<T>
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