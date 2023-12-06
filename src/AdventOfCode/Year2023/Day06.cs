namespace AdventOfCode.Year2023;

[Description("Wait For It")]
public class Day06 : IPuzzle
{
    public object Part1(string input)
    {
        return Parse(input)
            .Select(ProcessRace)
            .Aggregate((a, b) => a * b);
    }

    public object Part2(string input)
    {
        return Parse(input, true)
            .Select(ProcessRace)
            .Aggregate((a, b) => a * b);
    }

    public Race[] Parse(string input, bool badKerning = false)
    {
        var lines =input.ToLines().ToArray();
        var times = FixKerning(lines[0][6..]).Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(double.Parse).ToArray();
        var distances = FixKerning(lines[1][10..]).Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(double.Parse).ToArray();

        return times.Zip(distances).Select(tuple => new Race(tuple.Second, tuple.First)).ToArray();

        string FixKerning(string s) => badKerning ? s.Replace(" ", "") : s;
    }

    private int ProcessRace(Race race)
    {
        var range = SolveQuadratic(1, race.Time, race.Distance)
            .Select(Math.Abs)
            .Order()
            .Select((d, i) => double.IsInteger(d) ? d + (i == 0 ? 1 : -1) : d)
            .Select((d, i) => (int) Math.Round(d, i ==0 ? MidpointRounding.ToPositiveInfinity : MidpointRounding.ToNegativeInfinity))
            .ToArray();

        return range[1] - range[0] + 1;
    }

    public record Race(double Distance, double Time)
    {
        public double Speed => Distance / (double)Time;
    }

    public double[] SolveQuadratic(double a, double b, double c)
    {
        double discriminant = (b * b) - (4 * a * c);

        switch (discriminant)
        {
            case < 0:
                return Array.Empty<double>();
            case 0:
                {
                    double root = -b / (2 * a);
                    return new[] { root, root };
                }
            default:
                {
                    double root1 = (-b + Math.Sqrt(discriminant)) / (2 * a);
                    double root2 = (-b - Math.Sqrt(discriminant)) / (2 * a);
                    return new[] { root1, root2 };
                }
        }
    }
}