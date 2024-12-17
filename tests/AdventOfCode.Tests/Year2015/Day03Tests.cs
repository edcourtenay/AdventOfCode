using AdventOfCode.Solutions.Year2015;

namespace AdventOfCode.Tests.Year2015;

public class Day03Tests : IClassFixture<Day03>
{
    private readonly Day03 _sut;

    public Day03Tests(Day03 sut)
    {
        _sut = sut;
    }

    [Theory(DisplayName = "Part1 should return expected results from example data")]
    [InlineData(">", 2)]
    [InlineData("^>v<", 4)]
    [InlineData("^v^v^v^v^v", 2)]
    public void Part1Example(string input, int expected)
    {
        _sut.Part1(input).As<int>().Should().Be(expected);
    }

    [Theory(DisplayName = "Part2 should return expected results from example data")]
    [InlineData("^v", 3)]
    [InlineData("^>v<", 3)]
    [InlineData("^v^v^v^v^v", 11)]
    public void Part2Example(string input, int expected)
    {
        _sut.Part2(input).As<int>().Should().Be(expected);
    }
}