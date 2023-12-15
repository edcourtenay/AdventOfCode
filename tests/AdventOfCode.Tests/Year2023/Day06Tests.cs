using AdventOfCode.Year2023;

namespace AdventOfCode.Tests.Year2023;

public class Day06Tests : IClassFixture<Day06>
{
    private readonly Day06 _sut;

    private const string TestData =
        """
        Time:      7  15   30
        Distance:  9  40  200
        """;

    public Day06Tests(Day06 sut)
    {
        _sut = sut;
    }

    [Theory(DisplayName = "Part1 should return expected results from example data")]
    [InlineData(TestData, 288)]
    public void Part1Example(string input, int expected)
    {
        _sut.Part1(input).As<int>().Should().Be(expected);
    }

    [Theory(DisplayName = "Part2 should return expected results from example data")]
    [InlineData(TestData, 71503)]
    public void Part2Example(string input, int expected)
    {
        _sut.Part2(input).As<int>().Should().Be(expected);
    }

    [Fact(DisplayName = "Parse should return expected results from example data")]
    public void ParseInputExample()
    {
        Day06.Parse(TestData).Should().BeEquivalentTo(new Day06.Race[]
        {
            new (9, 7), new (40, 15), new (200, 30)
        });
    }
}