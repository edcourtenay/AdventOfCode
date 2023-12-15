namespace AdventOfCode.Year2023;

[Description("Cube Conundrum")]
public class Day02 : IPuzzle
{
    public object Part1(string input)
    {
        return input.ToLines()
            .Select(ParseGame)
            .Where(FilterImpossible)
            .Sum(game => game.Id);
    }

    public object Part2(string input)
    {
        return input.ToLines()
            .Select(ParseGame)
            .Sum(game => game.Sets.Max(set => set.Red) * game.Sets.Max(set => set.Green) *
                         game.Sets.Max(set => set.Blue));
    }

    private static bool FilterImpossible(Game game) =>
        game.Sets.All(set => set is { Red: <=12 , Green: <=13, Blue: <=14 });

    public static Game ParseGame(string input)
    {
        string[] strings = input.Split(':', StringSplitOptions.TrimEntries).ToArray();
        int id = int.Parse(strings[0].Split(' ')[1]);

        var sets = strings[1].Split(';', StringSplitOptions.TrimEntries).Select(ParseSet).ToArray();
        return new Game(id, sets);
    }

    private static Set ParseSet(string input)
    {
        var hands = input.Split(',', StringSplitOptions.TrimEntries);
        int red = 0;
        int green = 0;
        int blue = 0;

        foreach (string hand in hands)
        {
            (string color, int count) = ParseHand(hand);
            switch (color)
            {
                case "red":
                    red += count;
                    break;
                case "green":
                    green += count;
                    break;
                case "blue":
                    blue += count;
                    break;
                default:
                    throw new Exception($"Unknown color: {color}");
            }
        }

        return new Set(red, green, blue);
    }

    private static (string color, int count) ParseHand(string hand)
    {
        string[] strings = hand.Split(' ', StringSplitOptions.TrimEntries);
        return (color: strings[1], count: int.Parse(strings[0]));
    }

    public record Set(int Red, int Green, int Blue);

    public record Game(int Id, Set[] Sets);
}