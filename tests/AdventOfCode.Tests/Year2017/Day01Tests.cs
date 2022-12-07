using AdventOfCode.Year2017;

namespace AdventOfCode.Tests.Year2017;

public class Day01Tests : IClassFixture<Day01>
{
    private readonly Day01 _sut;

    public Day01Tests(Day01 sut)
    {
        _sut = sut;
    }

    [Theory(DisplayName = "Part1 should return expected results from example data")]
    [InlineData("1122", 3)]
    [InlineData("1111", 4)]
    [InlineData("1234", 0)]
    [InlineData("91212129", 9)]
    public void Part1Example(string input, int expectedValue)
    {
        _sut.Part1(input).As<int>().Should().Be(expectedValue);
    }

    [Theory(DisplayName = "Part2 should return expected results from example data")]
    [InlineData("1212", 6)]
    [InlineData("1221", 0)]
    [InlineData("123425", 4)]
    [InlineData("123123", 12)]
    [InlineData("12131415", 4)]
    public void Part2Example(string input, int expectedValue)
    {
        _sut.Part2(input).As<int>().Should().Be(expectedValue);
    }
}