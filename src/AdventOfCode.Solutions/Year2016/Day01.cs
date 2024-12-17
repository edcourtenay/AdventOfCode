using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2016;

[Description("No Time for a Taxicab")]
public partial class Day01 : IPuzzle
{
    [GeneratedRegex(@"(?<turn>L|R)(?<distance>\d+)")]
    private partial Regex InstructionRegex();
    
    public object Part1(string input)
    {
        Point position = Walk(input).Last();

        return Math.Abs(position.X) + Math.Abs(position.Y);
    }

    public object Part2(string input)
    {
        Point position = Find();

        return Math.Abs(position.X) + Math.Abs(position.Y);

        Point Find()
        {
            HashSet<Point> set = [];
            IEnumerable<Point> valueTuples = Walk(input);
            foreach (var pos in valueTuples)
            {
                if (!set.Add(pos))
                {
                    return pos;
                }
            }

            throw new InvalidOperationException();
        }
    }

    private IEnumerable<Point> Walk(string input)
    {
        Direction direction = new(0, 1);
        Point position = new(0, 0);

        yield return position;
        foreach (Match match in InstructionRegex().Matches(input))
        {
            direction = match.Groups["turn"].Value switch
            {
                "L" => direction.RotateLeft,
                "R" => direction.RotateRight,
                _ => throw new ArgumentOutOfRangeException()
            };

            int distance = int.Parse(match.Groups["distance"].Value);
            for (int i = 0; i < distance; i++)
            {
                position += direction;
                yield return position;
            }
        }
    }
}