﻿namespace AdventOfCode.Year2017;

[Description("A Maze of Twisty Trampolines, All Alike")]
public class Day05 : IPuzzle
{
    public object Part1(string input)
    {
        return ExecuteSteps(input, _ => 1);
    }

    public object Part2(string input)
    {
        return ExecuteSteps(input, o => o >= 3 ? -1 : 1);
    }

    private static int ExecuteSteps(string input, Func<int, int> offsetFunc)
    {
        var array = input.ToLines(s => int.Parse(s)).ToArray();
        var index = 0;
        var steps = 0;

        while (index >= 0 && index < array.Length)
        {
            var offset = array[index];
            array[index] += offsetFunc(offset);
            index += offset;
            steps++;
        }

        return steps;
    }
}