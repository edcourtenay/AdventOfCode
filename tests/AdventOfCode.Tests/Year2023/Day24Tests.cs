using AdventOfCode.Year2023;

namespace AdventOfCode.Tests.Year2023;

public class Day24Tests : IClassFixture<Day24>
{
    private readonly Day24 _sut;

    private const string TestData =
        """
        19, 13, 30 @ -2,  1, -2
        18, 19, 22 @ -1, -1, -2
        20, 25, 34 @ -2, -2, -4
        12, 31, 28 @ -1, -2, -1
        20, 19, 15 @  1, -5, -3
        """;

    public Day24Tests(Day24 sut)
    {
        _sut = sut;
    }

    [Theory(DisplayName = "Part1 should return expected results from example data")]
    [InlineData(TestData, 2)]
    public void Part1Example(string input, int expected)
    {
        var test = (new List<int> { 1, 2, 3, 4 }).CartesianPairs();

        Day24.SolvePart1(input, (7, 27)).As<int>().Should().Be(expected);
    }

    [Theory(DisplayName = "Part2 should return expected results from example data")]
    [InlineData(TestData, -1)]
    public void Part2Example(string input, int expected)
    {
        _sut.Part2(input).As<int>().Should().Be(expected);
    }
}