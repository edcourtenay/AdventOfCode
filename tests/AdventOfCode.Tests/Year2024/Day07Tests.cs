using AdventOfCode.Solutions.Year2024;

namespace AdventOfCode.Tests.Year2024;

public class Day07Tests : IClassFixture<Day07>
{
    private readonly Day07 _sut;

    private const string TestData =
        """
        190: 10 19
        3267: 81 40 27
        83: 17 5
        156: 15 6
        7290: 6 8 6 15
        161011: 16 10 13
        192: 17 8 14
        21037: 9 7 18 13
        292: 11 6 16 20
        """;

    public Day07Tests(Day07 sut)
    {
        _sut = sut;
    }

    [Fact]
    public void Part1Example()
    {
        _sut.Part1(TestData).As<int>().Should().Be(3749);
    }
    
    [Fact]
    public void Part2Example()
    {
        _sut.Part2(TestData).Should().Be("");
    }

    [Theory]
    [InlineData("190: 10 19", true)]
    [InlineData("3267: 81 40 27", true)]
    public void EvaluateLineTests(string lineData, bool expected)
    {
        (long target, long[] numbers) parsedLine = _sut.ParseLine(lineData);
        _sut.EvaluateLine(parsedLine).Should().Be(expected);
    }
}
