using AdventOfCode.Year2022;

namespace AdventOfCode.Tests.Year2022;

public class Day17Tests : IClassFixture<Day17>
{
    private readonly Day17 _sut;

    private const string TestData = ">>><<><>><<<>><>>><<<>>><<<><<<>><>><<>>";

    public Day17Tests(Day17 sut)
    {
        _sut = sut;
    }

    [Theory(DisplayName = "Part1 should return expected results from example data")]
    [InlineData(TestData, 3068)]
    public void Part1Example(string input, long expected)
    {
        _sut.Part1(input).As<long>().Should().Be(expected);
    }

    [Theory(DisplayName = "Part2 should return expected results from example data")]
    [InlineData(TestData, 1514285714288)]
    public void Part2Example(string input, long expected)
    {
        _sut.Part2(input).As<long>().Should().Be(expected);
    }
}