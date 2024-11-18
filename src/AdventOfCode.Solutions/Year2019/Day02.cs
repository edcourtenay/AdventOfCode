namespace AdventOfCode.Solutions.Year2019;

[Description("1202 Program Alarm")]
public class Day02 : IPuzzle
{
    public object Part1(string input)
    {
        var program = input.Split(',').Select(int.Parse).ToArray();
        program[1] = 12;
        program[2] = 2;

        ExecuteProgram(0, program);

        return program[0];
    }

    public object Part2(string input)
    {
        var baseProgram = input.Split(',').Select(int.Parse).ToArray();
        var program = new int[baseProgram.Length];

        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                baseProgram.CopyTo(program, 0);
                program[1] = i;
                program[2] = j;

                ExecuteProgram(0, program);

                if (program[0] == 19690720)
                {
                    return (100 * i) + j;
                }
            }
        }

        return 0;
    }

    public static void ExecuteProgram(int programCounter, int[] program)
    {
        while (program[programCounter] != 99)
        {
            program[program[programCounter + 3]] = program[programCounter] switch
            {
                1 => program[program[programCounter + 1]] + program[program[programCounter + 2]],
                2 => program[program[programCounter + 1]] * program[program[programCounter + 2]],
                _ => program[program[programCounter + 3]]
            };

            programCounter += 4;
        }
    }
}