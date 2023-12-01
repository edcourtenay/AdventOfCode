using AdventOfCode.Year2023;

namespace AdventOfCode.Tests.Year2023;

public class Day12Tests : IClassFixture<Day12>
{
    private readonly Day12 _sut;

    private const string TestData =
        """

        """;

    public Day12Tests(Day12 sut)
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