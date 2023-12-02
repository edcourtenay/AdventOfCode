using System.Diagnostics;
using System.Reflection.Metadata;
using System.Text;

namespace AdventOfCode.Year2022;

[Description("Pyroclastic Flow")]
public class Day17 : IPuzzle
{
    private const int LeftMask  = 0b1000000;
    private const int RightMask = 0b0000001;

    private static readonly Move LeftMove = new Move(LeftMask, r => r << 1);
    private static readonly Move RightMove = new Move(RightMask, r => r >> 1);


    private static readonly int[][] RockData = {
        new[] { 0b0011110 },
        new[] { 0b0001000, 0b0011100, 0b0001000 },
        new[] { 0b0000100, 0b0000100, 0b0011100 },
        new[] { 0b0010000, 0b0010000, 0b0010000, 0b0010000 },
        new[] { 0b0011000, 0b0011000 }
    };

    public object Part1(string input)
    {
        return Solve(input.ToCharArray(), 2022);
    }

    public object Part2(string input)
    {
        return Solve(input.ToCharArray(), 1000000000000);
    }

    private static long Solve(char[] wind, long iterations)
    {
        LinkedListNode<char> currentWind = new LinkedList<char>(wind).First!;
        Dictionary<int, int> chamber = new()
        {
            { 0, 0b1111111 }
        };

        var rockIndex = 0;
        var rock = GetRock(ref rockIndex);
        var rockRow = TopRow(chamber) + rock.Length + 3;

        for (long i = 0; i < iterations; )
        {
            rock = Blow(rock, rockRow, currentWind.Value, chamber);
            currentWind = currentWind.NextOrFirst();

            if (CanMove(rock, rockRow - 1, chamber))
            {
                rockRow--;
            }
            else
            {
                for (int j = 0; j < rock.Length; j++)
                {
                    int chamberRow = rockRow - j;
                    var row = chamber.TryGetValue(chamberRow, out var r) ? r : 0;
                    chamber[chamberRow] = row | rock[j];
                }

                i++;
                rock = GetRock(ref rockIndex);
                rockRow = TopRow(chamber) + rock.Length + 3;
            }
        }

        return TopRow(chamber);
    }

    private static void Visualise(int[] rock, int rockRow, Dictionary<int,int> chamber)
    {
        var sb = new StringBuilder();
        for (int i = rockRow; i > 0; i--)
        {
            sb.Clear();
            sb.Append("|       |");
            int mask = 0b1000000;
            int j = 1;
            int c = chamber.TryGetValue(i, out int value) ? value : 0;
            while (mask > 0)
            {
                if ((c & mask) != 0)
                {
                    sb[j] = '#';
                }
                j++;
                mask = mask >> 1;
            }

            int rr = rockRow - i;
            if (rr >= 0 && rr < rock.Length)
            {
                mask = 0b1000000;
                j = 1;
                while (mask > 0)
                {
                    if ((rock[rr] & mask) != 0)
                    {
                        sb[j] = '@';
                    }
                    j++;
                    mask = mask >> 1;
                }
            }

            Console.WriteLine(sb.ToString());
        }
        Console.WriteLine("+-------+");
        Console.WriteLine();
    }

    private static int TopRow(Dictionary<int, int> chamber)
    {
        int fullRow = chamber.Where(pair => pair.Value == 0b1111111).Select(pair => pair.Key).DefaultIfEmpty(0).Max();
        foreach (var key in chamber.Keys.Where(k => k < fullRow))
        {
            chamber.Remove(key);
        }

        return chamber.Where(pair => pair.Value != 0).Select(pair => pair.Key).DefaultIfEmpty(0).Max();
    }

    private static int[] GetRock(ref int rockIndex)
    {
        int index = rockIndex;
        rockIndex = (rockIndex + 1) % RockData.Length;
        return RockData[index].Select(r => r).ToArray();
    }

    private static bool CanRotate(IEnumerable<int> rock, int mask)
    {
        return rock.All(r => (r & mask) == 0);
    }

    private static int[] Blow(int[] rock, int rockRow, char direction, Dictionary<int, int> chamber)
    {
        (int mask, Func<int, int> func) = direction switch
        {
            '<' => LeftMove,
            '>' => RightMove,
            _ => throw new ArgumentOutOfRangeException(nameof(direction))
        };

        if (!CanRotate(rock, mask))
        {
            return rock;
        }

        var newRock = rock.Select(r => func(r)).ToArray();

        return CanMove(newRock, rockRow, chamber)
            ? newRock
            : rock;
    }

    private static bool CanMove(int[] newRock, int rockRow, Dictionary<int, int> chamber)
    {
        var canMove = true;
        for (int i = 0; i < newRock.Length; i++)
        {
            var row = chamber.TryGetValue(rockRow - i, out var r) ? r : 0;
            canMove &= (newRock[i] & row) == 0;
        }

        return canMove;
    }

    record Move(int Mask, Func<int, int> Func);
}