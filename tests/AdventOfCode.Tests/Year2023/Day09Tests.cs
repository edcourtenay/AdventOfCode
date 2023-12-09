using AdventOfCode.Year2023;

namespace AdventOfCode.Tests.Year2023;

public class Day09Tests : IClassFixture<Day09>
{
    private readonly Day09 _sut;

    private const string TestData =
        """
        0 3 6 9 12 15
        1 3 6 10 15 21
        10 13 16 21 30 45
        """;

    public Day09Tests(Day09 sut)
    {
        _sut = sut;
    }

    [Theory(DisplayName = "Part1 should return expected results from example data")]
    [InlineData(TestData, 114)]
    public void Part1Example(string input, long expected)
    {
        _sut.Part1(input).As<long>().Should().Be(expected);
    }

    [Theory(DisplayName = "Part2 should return expected results from example data")]
    [InlineData(TestData, 2)]
    public void Part2Example(string input, long expected)
    {
        _sut.Part2(input).As<long>().Should().Be(expected);
    }

    [Theory(DisplayName = "GenerateSequence should return expected results from puzzle input")]
    [InlineData(new long[] { 0, 3, 6, 9, 12, 15}, new long[] { 3, 3, 3, 3, 3})]
    [InlineData(new long[] { 3, 3, 3, 3, 3}, new long[] { 0, 0, 0, 0 })]
    public void GenerateExamples(long[] input, long[] expected)
    {
        _sut.GenerateSequence(input).Should().BeEquivalentTo(expected);
    }

    [Theory(DisplayName = "Extrapolate should return expected results from puzzle input")]
    [InlineData(new long[] { 0, 3, 6, 9, 12, 15 }, -3, 18)]
    [InlineData(new long[] { 1, 3, 6, 10, 15, 21 }, 0, 28)]
    [InlineData(new long[] { 10, 13, 16, 21, 30, 45 }, 5, 68)]
    public void ExtrapolateExamples(long[] input, long left, long right)
    {
        _sut.Extrapolate(input).Should().Be((left, right));
    }
}