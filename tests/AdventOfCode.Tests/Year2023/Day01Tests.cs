using AdventOfCode.Year2022;

namespace AdventOfCode.Tests.Year2023;

public class Day01Tests : IClassFixture<Day01>
{
    private readonly Day01 _sut;

    private const string TestData = "";

    public Day01Tests(Day01 sut)
    {
        _sut = sut;
    }

    [Theory(DisplayName = "Part1 should return expected results from example data")]
    [InlineData("", 0)]
    public void Part1Example(string input, int expected)
    {
        _sut.Part1(input).As<int>().Should().Be(expected);
    }


    [Theory(DisplayName = "Part2 should return expected results from example data")]
    [InlineData("", 0)]
    public void Part2Example(string input, int expected)
    {
        _sut.Part2(input).As<int>().Should().Be(expected);
    }
}