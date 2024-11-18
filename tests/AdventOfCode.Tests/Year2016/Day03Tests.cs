using AdventOfCode.Solutions.Year2016;

namespace AdventOfCode.Tests.Year2016;

public class Day03Tests
{
    [Fact(DisplayName = "Part1 should return expected results from example data")]
    public void Part1Example()
    {
        Day03? sut = new();

        int result = (int)sut.Part1("5 10 25");

        result.Should().Be(0);
    }
}