namespace AdventOfCode.Year2019;

[Description("Sunny with a Chance of Asteroids")]
public class Day05 : IPuzzle
{
    public object Part1(string input)
    {
        return Process(input, 1);
    }

    public object Part2(string input)
    {
        return Process(input, 5);
    }

    private static object Process(string input, int inputValue)
    {
        int[] data = input.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(int.Parse).ToArray();
        
        ProcessCodes(data, inputValue, out var code);
        return code;
    }

    private enum Mode
    {
        Position,
        Immediate
    }
    
    private static void ProcessCodes(int[] intCodes, int inputValue, out int output)
    {
        int x = 0;
        output = 0;

        while (x < intCodes.Length)
        {
            int opCode = 0;
            int param1 = 0;
            int param2 = 0;
            var param1Mode = Mode.Position;
            var param2Mode = Mode.Position;
            int instLength = 0;
            string currentCode = intCodes[x].ToString();

            if (currentCode == "99")
            {
                break;
            }

            switch (currentCode.Length)
            {
                case 1:
                    opCode = int.Parse(currentCode);
                    break;
                case 2:
                    opCode = int.Parse(currentCode[1].ToString());
                    param1Mode = currentCode[0] == '0' ? Mode.Position : Mode.Immediate;
                    break;
                case 3:
                    opCode = int.Parse(currentCode.Substring(1));
                    param1Mode = currentCode[0] == '0' ? Mode.Position : Mode.Immediate;
                    break;
                case 4:
                    opCode = int.Parse(currentCode.Substring(2));
                    param1Mode = currentCode[1] == '0' ? Mode.Position : Mode.Immediate;
                    param2Mode = currentCode[0] == '0' ? Mode.Position : Mode.Immediate;
                    break;
                case 5:
                    opCode = int.Parse(currentCode.Substring(3));
                    param1Mode = currentCode[2] == '0' ? Mode.Position : Mode.Immediate;
                    param2Mode = currentCode[1] == '0' ? Mode.Position : Mode.Immediate;
                    break;
            }

            if (opCode != 3)
            {
                param1 = param1Mode == Mode.Position ? intCodes[intCodes[x + 1]] : intCodes[x + 1];
            }

            if (opCode != 3 && opCode != 4)
            {
                param2 = param2Mode == Mode.Position ? intCodes[intCodes[x + 2]] : intCodes[x + 2];
            }

            switch (opCode)
            {
                case 1:
                    instLength = 4;
                    intCodes[intCodes[x + 3]] = param1 + param2;
                    break;
                case 2:
                    instLength = 4;
                    intCodes[intCodes[x + 3]] = param1 * param2;
                    break;
                case 3:
                    instLength = 2;
                    intCodes[intCodes[x + 1]] = inputValue;
                    break;
                case 4:
                    instLength = 2;
                    output = param1;
                    break;
                case 5:
                    if (param1 != 0)
                    {
                        x = param2;
                    }
                    else
                    {
                        instLength = 3;
                    }

                    break;
                case 6:
                    if (param1 == 0)
                    {
                        x = param2;
                    }
                    else
                    {
                        instLength = 3;
                    }

                    break;
                case 7:
                    instLength = 4;
                    intCodes[intCodes[x + 3]] = param1 < param2 ? 1 : 0;
                    break;
                case 8:
                    instLength = 4;
                    intCodes[intCodes[x + 3]] = param1 == param2 ? 1 : 0;
                    break;
            }

            x += instLength;
        }
    }
}