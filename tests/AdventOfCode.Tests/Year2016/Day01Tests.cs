using AdventOfCode.Solutions.Year2016;

namespace AdventOfCode.Tests.Year2016;

public class Day01Tests
{
    [Theory(DisplayName = "Part1 should return expected results from example data")]
    [InlineData("R2, L3", 5)]
    [InlineData("R2, R2, R2", 2)]
    [InlineData("R5, L5, R5, R3", 12)]
    public void Part1Examples(string input, int expectedResult)
    {
        Day01? sut = new();

        int result = (int)sut.Part1(input);

        result.Should().Be(expectedResult);
    }

    [Theory(DisplayName = "Part2 should return expected results from example data")]
    [InlineData("R8, R4, R4, R8", 4)]
    public void Part2Examples(string input, int expectedResult)
    {
        Day01? sut = new();

        int result = (int)sut.Part2(input);

        result.Should().Be(expectedResult);
    }
}