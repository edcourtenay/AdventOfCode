using AdventOfCode.Solutions.Year2016;

namespace AdventOfCode.Tests.Year2016;

public class Day01Tests : IClassFixture<Day01>
{
    private readonly Day01 _sut;

    public Day01Tests(Day01 sut)
    {
        _sut = sut;
    }

    [Theory(DisplayName = "Part1 should return expected results from example data")]
    [InlineData("R2, L3", 5)]
    [InlineData("R2, R2, R2", 2)]
    [InlineData("R5, L5, R5, R3", 12)]
    public void Part1Examples(string input, int expectedResult)
    {
        _sut.Part1(input).As<int>().Should().Be(expectedResult);
    }

    [Theory(DisplayName = "Part2 should return expected results from example data")]
    [InlineData("R8, R4, R4, R8", 4)]
    public void Part2Examples(string input, int expectedResult)
    {
        _sut.Part2(input).As<int>().Should().Be(expectedResult);
    }
}