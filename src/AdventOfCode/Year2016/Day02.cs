using System.Text;

namespace AdventOfCode.Year2016;

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

    private static string FindCombination(string input, (int, int) startKey, Func<(int col, int row), bool> validFunc,
        Func<(int col, int row), char> charFunc)
    {
        StringBuilder sb = new();
        (int col, int row) key = startKey;
        foreach (var line in input.ToLines())
        {
            key = IterateKeys(line, key, validFunc).Last();
            sb.Append(charFunc(key));
        }

        return sb.ToString();
    }

    private static IEnumerable<(int, int)> IterateKeys(string instructions, (int, int) position,
        Func<(int col, int row), bool> isKeyValid)
    {
        yield return position;
        
        (int col, int row) = position;
        foreach (char instruction in instructions)
        {
            (int newCol, int newRow) = instruction switch
            {
                'U' => (col, row - 1),
                'D' => (col, row + 1),
                'L' => (col - 1, row),
                'R' => (col + 1, row),
                _ => throw new ArgumentOutOfRangeException()
            };

            if (isKeyValid((newCol, newRow)))
            {
                yield return (newCol, newRow);
                (col, row) = (newCol, newRow);
            }
        }
    }

    private static bool IsKeyValid1((int col, int row) position) 
        => !(position.col is < 0 or > 2 || position.row is < 0 or > 2);

    private static bool IsKeyValid2((int col, int row) position) 
        => !(position.col is < 0 or > 4 || position.row is < 0 or > 4) && DiamondPad(position) != '.';

    private static char SquarePad((int col, int row) position) 
        => (char)('1' + position.col + position.row * 3);

    private static char DiamondPad((int col, int row) position)
    {
        const string pad = "..1...234.56789.ABC...D..";
        return pad[position.row * 5 + position.col];
    }
}