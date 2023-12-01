using AdventOfCode.Year2023;

namespace AdventOfCode.Tests.Year2023;

public class Day11Tests : IClassFixture<Day11>
{
    private readonly Day11 _sut;

    private const string TestData =
        """

        """;

    public Day11Tests(Day11 sut)
    {
        _sut = sut;
    }

    [Theory(DisplayName = "Part1 should return expected results from example data")]
    [InlineData(TestData, -1)]
    public void Part1Example(string input, int expected)
    {
        _sut.Part1(input).As<int>().Should().Be(expected);
    }

    [Theory(DisplayName = "Part2 should return expected results from example data")]
    [InlineData(TestData, -1)]
    public void Part2Example(string input, int expected)
    {
        _sut.Part2(input).As<int>().Should().Be(expected);
    }
}