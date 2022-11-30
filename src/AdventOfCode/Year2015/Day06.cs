using System.Text.RegularExpressions;

namespace AdventOfCode.Year2015;

[Description("Probably a Fire Hazard")]
public class Day06 : IPuzzle
{
    private static readonly Regex Regex = new(@"(turn off|turn on|toggle)\s(\d+),(\d+)\sthrough\s(\d+),(\d+)",
        RegexOptions.Compiled);

    public object Part1(string input)
    {
        return Execute(input, false);
    }

    public object Part2(string input)
    {
        return Execute(input, true);
    }

    private string Execute(string input, bool useVersion2)
    {
        var arr = new int[1000 * 1000];
        foreach (var instruction in Data(input))
        {
            Action<int[],int> operation = SelectOperation(instruction, useVersion2);
            foreach (int index in instruction.Range())
            {
                operation(arr, index);
            }
        }

        return arr.Sum().ToString();
    }

    public IEnumerable<Instruction> Data(string input)
    {
        using var r = new StringReader(input);

        while (r.ReadLine() is { } line)
        {
            yield return ParseLine(line);
        }
    }

    public Instruction ParseLine(string line)
    {
        var match = Regex.Match(line);
        var op = match.Groups[1].Value;
        var x1 = int.Parse(match.Groups[2].Value);
        var y1 = int.Parse(match.Groups[3].Value);
        var x2 = int.Parse(match.Groups[4].Value);
        var y2 = int.Parse(match.Groups[5].Value);
        return new Instruction(op, x1, y1, x2, y2);
    }

    public Action<int[], int> SelectOperation(Instruction instruction, bool useVersion2 = false)
    {
        return instruction.Operation switch
        {
            "turn on" when useVersion2 => _turnOnV2,
            "turn on" => _turnOnV1,
            "turn off" when useVersion2 => _turnOffV2,
            "turn off" => _turnOffV1,
            "toggle" when useVersion2 => _toggleV2,
            "toggle" => _toggleV1,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    readonly Action<int[], int> _turnOnV1 = (arr, index) => arr[index] = 1;

    readonly Action<int[], int> _turnOffV1 = (arr, index) => arr[index] = 0;

    readonly Action<int[], int> _toggleV1 = (arr, index) => arr[index] = arr[index] == 1 ? 0 : 1;

    readonly Action<int[], int> _turnOnV2 = (arr, index) => arr[index] += 1;

    private readonly Action<int[], int> _turnOffV2 = (arr, index) => arr[index] = arr[index] >= 1 ? arr[index] - 1 : 0; 
    
    readonly Action<int[], int> _toggleV2 = (arr, index) => arr[index] += 2;

    public readonly record struct Instruction(string Operation, int X1, int Y1, int X2, int Y2)
    {
        public IEnumerable<int> Range()
        {
            for (int y = Y1; y <= Y2; y++)
            {
                for (int x = X1; x <= X2; x++)
                {
                    yield return x * 1000 + y;
                }
            }
        }
    };
}