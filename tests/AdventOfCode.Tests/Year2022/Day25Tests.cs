using AdventOfCode.Year2022;

namespace AdventOfCode.Tests.Year2022;

public class Day25Tests : IClassFixture<Day25>
{
    private readonly Day25 _sut;

    private const string TestData =
        """
        1=-0-2
        12111
        2=0=
        21
        2=01
        111
        20012
        112
        1=-1=
        1-12
        12
        1=
        122
        """;

    public Day25Tests(Day25 sut)
    {
        _sut = sut;
    }

    [Theory(DisplayName = "Part1 should return expected results from example data")]
    [InlineData(TestData, "2=-1=0")]
    public void Part1Example(string input, string expected)
    {
        _sut.Part1(input).As<string>().Should().Be(expected);
    }

    // [Theory(DisplayName = "Part2 should return expected results from example data")]
    // [InlineData(TestData, -1)]
    // public void Part2Example(string input, int expected)
    // {
    //     _sut.Part2(input).As<int>().Should().Be(expected);
    // }

    [Theory(DisplayName = "SnafuToDecimal should return expected results from example data")]
    [InlineData("1=-0-2", 1747)]
    [InlineData("12111",906)]
    [InlineData("2=0=",198)]
    [InlineData("21",11)]
    [InlineData("2=01",201)]
    [InlineData("111",31)]
    [InlineData("20012",1257)]
    [InlineData("112",32)]
    [InlineData("1=-1=",353)]
    [InlineData("1-12",107)]
    [InlineData("12",7)]
    [InlineData("1=",3)]
    [InlineData("122",37)]
    public void SnafuToDecimalTests(string input, int expected)
    {
        _sut.SnafuToInt(input).Should().Be(expected);
    }

    [Theory(DisplayName = "IntToSnafu should return expected results from example data")]
    [InlineData(1747,"1=-0-2")]
    [InlineData(906,"12111")]
    [InlineData(198,"2=0=")]
    [InlineData(11,"21")]
    [InlineData(201,"2=01")]
    [InlineData(31,"111")]
    [InlineData(1257,"20012")]
    [InlineData(32,"112")]
    [InlineData(353,"1=-1=")]
    [InlineData(107,"1-12")]
    [InlineData(7,"12")]
    [InlineData(3,"1=")]
    [InlineData(37,"122")]
    public void IntToSnafuTests(int input, string expected)
    {
        _sut.IntToSnafu(input).Should().Be(expected);
    }
}