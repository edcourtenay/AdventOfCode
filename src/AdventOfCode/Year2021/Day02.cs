namespace AdventOfCode.Year2021;

[Description("Dive!")]
public class Day02 : IPuzzle
{
    public object Part1(string input)
    {
        var parsed = input
            .ToLines(line => line.Split(' ').ToArray())
            .Select(seg => new { Command = seg[0], Unit = Convert.ToInt32(seg[1]) });

        var pos = (horizontal: 0, depth: 0);
        pos = parsed.Aggregate(pos, (current, command) => command.Command switch
        {
            "forward" => (current.horizontal + command.Unit, current.depth),
            "down" => (current.horizontal, current.depth + command.Unit),
            "up" => (current.horizontal, current.depth - command.Unit),
            _ => throw new ArgumentOutOfRangeException()
        });

        return pos.horizontal * pos.depth;
    }

    public object Part2(string input)
    {
        var parsed = input
            .ToLines(line => line.Split(' ').ToArray())
            .Select(seg => new { Command = seg[0], Unit = Convert.ToInt32(seg[1]) });

        var pos = (horizontal: 0, depth: 0, aim: 0);
        pos = parsed.Aggregate(pos, (current, command) => command.Command switch
        {
            "forward" => current with { horizontal = current.horizontal + command.Unit, depth = current.depth + current.aim * command.Unit },
            "down" => current with { aim = current.aim + command.Unit },
            "up" => current with { aim = current.aim - command.Unit },
            _ => throw new ArgumentOutOfRangeException()
        });

        return pos.horizontal * pos.depth;
    }
}