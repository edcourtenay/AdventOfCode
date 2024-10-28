using System.Text.RegularExpressions;

namespace AdventOfCode.Year2016;

[Description("No Time for a Taxicab")]
public partial class Day01 : IPuzzle
{
    [GeneratedRegex(@"(?<turn>L|R)(?<distance>\d+)")]
    private partial Regex InstructionRegex();
    
    public object Part1(string input)
    {
        (int x, int y) position = Walk(input).Last();

        return Math.Abs(position.x) + Math.Abs(position.y);
    }

    public object Part2(string input)
    {
        (int, int) Find()
        {
            HashSet<(int, int)> set = [];
            IEnumerable<(int, int)> valueTuples = Walk(input);
            foreach (var pos in valueTuples)
            {
                if (!set.Add(pos))
                {
                    return pos;
                }
            }

            throw new InvalidOperationException();
        }
        
        (int x, int y) position = Find();

        return Math.Abs(position.x) + Math.Abs(position.y);
    }

    private IEnumerable<(int, int)> Walk(string input)
    {
        var direction = (x: 0, y: 1);
        var position = (x: 0, y: 0);

        yield return position;
        foreach (Match match in InstructionRegex().Matches(input))
        {
            direction = match.Groups["turn"].Value switch
            {
                "L" => (-direction.y, direction.x), 
                "R" => (direction.y, -direction.x),
                _ => throw new ArgumentOutOfRangeException()
            };

            int distance = int.Parse(match.Groups["distance"].Value);
            for (int i = 0; i < distance; i++)
            {
                position = (position.x + direction.x, position.y + direction.y);
                yield return position;
            }
        }
    }
}