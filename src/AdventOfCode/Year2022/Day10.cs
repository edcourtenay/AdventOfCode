using System.Text;

namespace AdventOfCode.Year2022;

[Description("Cathode-Ray Tube")]
public class Day10 : IPuzzle
{
    public object Part1(string input)
    {
        return ProcessInstructions(input.ToLines())
            .Select((x, i) => (x, i: i + 1))
            .Where(t => (t.i - 20) % 40 == 0)
            .Sum(tuple => tuple.x * tuple.i);
    }

    public object Part2(string input)
    {
        var registers = ProcessInstructions(input.ToLines());

        var sb = new StringBuilder();
        sb.AppendLine();
        var x = 0;
        using IEnumerator<int> enumerator = registers.GetEnumerator();

        while (enumerator.MoveNext())
        {
            sb.Append(x >= enumerator.Current - 1
                      && x <= enumerator.Current + 1 ? '#' : '.');

            if (++x == 40)
            {
                x = 0;
                sb.AppendLine();
            }
        }

        return sb.ToString();
    }

    private static IEnumerable<int> ProcessInstructions(IEnumerable<string> instructions)
    {
        var x = 1;

        foreach (string instruction in instructions)
        {
            var segments = instruction.Split(' ');

            (int cycles, int op1, Func<int, int, int> func) t = segments switch
            {
                ["noop"] => (1, 0, (r, _) => r),
                ["addx", { } p1] => (2, int.Parse(p1), (r, op1) => r + op1),
                _ => throw new ArgumentException()
            };

            for (int i = 0; i < t.cycles; i++)
            {
                yield return x;
            }

            x = t.func(x, t.op1);
        }
    }
}