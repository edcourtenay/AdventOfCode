namespace AdventOfCode.Solutions.Year2022;

[Description("Rock Paper Scissors")]
public class Day02 : IPuzzle
{
    private static readonly LinkedList<Choice> Choices = new([Choice.Rock, Choice.Paper, Choice.Scissors]);

    public object Part1(string input)
    {
        return Execute(input, line => (Player: Translate(line[2]), Opponent: Translate(line[0])));
    }

    public object Part2(string input)
    {
        return Execute(input, line => (Player: GetStrategy(line[2], Translate(line[0])), Opponent: Translate(line[0])));
    }

    private static int Execute(string input, Func<string, (Choice Player, Choice Opponent)> convert)
    {
        return Enumerable
            .Select<(Choice Player, Choice Opponent), int>(input
                .ToLines(convert), r => Score(r.Player, r.Opponent))
            .Sum();
    }

    private static Choice Translate(char c) =>
        c switch
        {
            'A' or 'X' => Choice.Rock,
            'B' or 'Y' => Choice.Paper,
            'C' or 'Z' => Choice.Scissors,
            _ => throw new ArgumentOutOfRangeException(nameof(c))
        };

    private static Choice GetStrategy(char c, Choice opponent) =>
        (c, Choices.Find(opponent)) switch
        {
            ('Z', { } n) => n.NextOrFirst().Value,
            ('X', { } n) => n.PreviousOrLast().Value,
            ('Y', { } n) => n.Value,
            _ => throw new ArgumentOutOfRangeException()
        };

    private static int Score(Choice player, Choice opponent)
    {
        LinkedListNode<Choice> linkedListNode = Choices.Find(opponent)!;
        return (player, linkedListNode.PreviousOrLast().Value, linkedListNode.NextOrFirst().Value) switch
        {
            var (p, _, w) when p == w => 6,
            var (p, l, _) when p == l => 0,
            _ => 3
        } + (int)player;
    }

    private enum Choice
    {
        Rock = 1,
        Paper = 2,
        Scissors = 3
    }
}