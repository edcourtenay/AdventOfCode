using AdventOfCode.Year2023;

namespace AdventOfCode.Tests.Year2023;

public class Day22Tests : IClassFixture<Day22>
{
    private readonly Day22 _sut;

    private const string TestData =
        """

        """;

    public Day22Tests(Day22 sut)
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