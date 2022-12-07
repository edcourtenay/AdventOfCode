namespace AdventOfCode.Year2017;

[Description("Inverse Captcha")]
public class Day01 : IPuzzle
{
    public object Part1(string input)
    {
        return Solve(input, 1);
    }

    public object Part2(string input)
    {
        return Solve(input, input.Length / 2);
    }

    private static object Solve(string input, int offset)
    {
        var sum = 0;
        var chars = input.ToCharArray();

        for (int i = 0; i < chars.Length; i++)
        {
            var j = (i + offset) % chars.Length;
            if (chars[i] == chars[j])
            {
                sum += chars[i] - '0';
            }
        }

        return sum;
    }
}