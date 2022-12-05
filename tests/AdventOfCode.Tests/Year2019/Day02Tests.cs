using AdventOfCode.Year2019;

namespace AdventOfCode.Tests.Year2019;

public class Day02Tests
{
    [Fact(DisplayName = "Part1 should return expected results from example data")]
    public void Part1Example()
    {
        var program = new[] { 1, 9, 10, 3, 2, 3, 11, 0, 99, 30, 40, 50 };
        Day02.ExecuteProgram(0, program);

        program[0].Should().Be(3500);
    }
}