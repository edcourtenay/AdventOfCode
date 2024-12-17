using System.Text;

namespace AdventOfCode.Solutions.Year2016;

[Description("Bathroom Security")]
public class Day02 : IPuzzle
{
    public object Part1(string input)
    {
        return FindCombination(input, (1, 1), IsKeyValid1, SquarePad);
    }

    public object Part2(string input)
    {
        return FindCombination(input, (0, 2), IsKeyValid2, DiamondPad);
    }

    private static string FindCombination(string input, Point startKey, Func<Point, bool> validFunc,
        Func<Point, char> charFunc)
    {
        StringBuilder sb = new();
        Point key = startKey;
        foreach (var line in input.ToLines())
        {
            key = IterateKeys(line, key, validFunc).Last();
            sb.Append(charFunc(key));
        }

        return sb.ToString();
    }

    private static IEnumerable<Point> IterateKeys(string instructions, Point position,
        Func<Point, bool> isKeyValid)
    {
        yield return position;

        foreach (var newPos in instructions.Select(instruction => position + instruction switch
                 {
                     'U' => Direction.North,
                     'D' => Direction.South,
                     'L' => Direction.West,
                     'R' => Direction.East,
                     _ => throw new ArgumentOutOfRangeException()
                 }).Where(isKeyValid))
        {
            yield return newPos;
            position = newPos;
        }
    }

    private static bool IsKeyValid1(Point position) 
        => !(position.X is < 0 or > 2 || position.Y is < 0 or > 2);

    private static bool IsKeyValid2(Point position) 
        => !(position.X is < 0 or > 4 || position.Y is < 0 or > 4) && DiamondPad(position) != '.';

    private static char SquarePad(Point position) 
        => (char)('1' + position.X + position.Y * 3);

    private static char DiamondPad(Point position)
    {
        const string pad = "..1...234.56789.ABC...D..";
        return pad[position.Y * 5 + position.X];
    }
}