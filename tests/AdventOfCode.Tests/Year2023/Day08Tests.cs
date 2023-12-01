using AdventOfCode.Year2023;

namespace AdventOfCode.Tests.Year2023;

public class Day08Tests : IClassFixture<Day08>
{
    private readonly Day08 _sut;

    private const string TestData =
        """

        """;

    public Day08Tests(Day08 sut)
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