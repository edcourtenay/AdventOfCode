namespace AdventOfCode.Year2022;

[Description("Rock Paper Scissors")]
public class Day02 : IPuzzle
{
    public object Part1(string input)
    {
        return input
            .ToLines(line => (Player: Translate(line[2]), Opponent: Translate(line[0])))
            .Select(r => Score(r.Player, r.Opponent))
            .Sum();
    }

    public object Part2(string input)
    {
        return input
            .ToLines(line => (Player: GetStrategy(line[2], Translate(line[0])), Opponent: Translate(line[0])))
            .Select(r => Score(r.Player, r.Opponent))
            .Sum();
    }

    public (Choice Player, Choice Opponent) Parse2(string line)
    {
        var opponent = Translate(line[0]);
        var player = GetStrategy(line[2], opponent);
        return (player, opponent);
    }

    private static Choice Translate(char c) =>
        c switch
        {
            'A' or 'X' => Choice.Rock,
            'B' or 'Y' => Choice.Paper,
            'C' or 'Z' => Choice.Scissors,
            _ => throw new ArgumentOutOfRangeException()
        };

    public Choice GetStrategy(char c, Choice opponent)
    {
        return (c, opponent) switch
        {
            ('Z', Choice.Rock) => Choice.Paper,
            ('Z', Choice.Paper) => Choice.Scissors,
            ('Z', Choice.Scissors) => Choice.Rock,
            ('X', Choice.Rock) => Choice.Scissors,
            ('X', Choice.Paper) => Choice.Rock,
            ('X', Choice.Scissors) => Choice.Paper,
            var (_, a) => a,
        };
    }

    public int Score(Choice player, Choice opponent)
    {
        return (player, opponent) switch
        {
            (Choice.Rock, Choice.Scissors) => 6,
            (Choice.Paper, Choice.Rock) => 6,
            (Choice.Scissors, Choice.Paper) => 6,
            var (l, r) when l == r => 3,
            _ => 0
        } + (int)player;
    }

    public enum Choice
    {
        Rock = 1,
        Paper = 2,
        Scissors = 3
    }
}