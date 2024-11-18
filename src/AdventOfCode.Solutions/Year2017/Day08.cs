using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2017;

[Description("I Heard You Like Registers")]
public partial class Day08 : IPuzzle
{
    public object Part1(string input)
    {
        var registers = new Dictionary<string, int>();
        Process(input, registers);

        return registers.Values.Max();
    }

    public object Part2(string input)
    {
        var registers = new Dictionary<string, int>();
        int maxValue = 0;
        Process(input, registers, max => maxValue = max);

        return maxValue;
    }

    private static void Process(string input, Dictionary<string, int> registers, Action<int>? onMax = null)
    {
        var max = int.MinValue;

        foreach (string line in input.ToLines())
        {
            var match = Regex().Match(line);
            if (!match.Success)
            {
                throw new Exception($"Failed to parse line: {line}");
            }

            var register = match.Groups["register"].Value;
            var operation = match.Groups["operation"].Value;
            var amount = int.Parse(match.Groups["amount"].Value);
            var conditionRegister = match.Groups["conditionRegister"].Value;
            var condition = match.Groups["condition"].Value;
            var conditionAmount = int.Parse(match.Groups["conditionAmount"].Value);

            var registerValue = registers.GetValueOrDefault(register, 0);
            var conditionRegisterValue = registers.GetValueOrDefault(conditionRegister, 0);

            var conditionMet = condition switch
            {
                "==" => conditionRegisterValue == conditionAmount,
                "!=" => conditionRegisterValue != conditionAmount,
                "<" => conditionRegisterValue < conditionAmount,
                ">" => conditionRegisterValue > conditionAmount,
                "<=" => conditionRegisterValue <= conditionAmount,
                ">=" => conditionRegisterValue >= conditionAmount,
                _ => throw new Exception($"Unknown condition: {condition}")
            };

            if (conditionMet)
            {
                registers[register] = operation switch
                {
                    "inc" => registerValue + amount,
                    "dec" => registerValue - amount,
                    _ => throw new Exception($"Unknown operation: {operation}")
                };

                if (onMax != null)
                {
                    max = Math.Max(max, registers[register]);
                    onMax(max);
                }
            }
        }
    }

    [GeneratedRegex("""(?<register>\w+)\s(?<operation>inc|dec)\s(?<amount>-?\d+)\sif\s(?<conditionRegister>\w+)\s(?<condition>==|!=|<|>|<=|>=)\s(?<conditionAmount>-?\d+)""")]
    private static partial Regex Regex();
}
