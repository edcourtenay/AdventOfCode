using AdventOfCode.Solutions.Year2015;

namespace AdventOfCode.Tests.Year2015;

public class Day10Tests : IClassFixture<Day10>
{
    private readonly Day10 _sut;

    public Day10Tests(Day10 sut)
    {
        _sut = sut;
    }

    [Theory(DisplayName = "LookAndSay should produce expected results")]
    [InlineData("1", "11")]
    [InlineData("11", "21")]
    [InlineData("21", "1211")]
    [InlineData("1211", "111221")]
    [InlineData("111221", "312211")]
    public void LookAndSayTests(string input, string expected)
    {
        _sut.LookAndSay(input).Should().Be(expected);
    }

    [Fact(DisplayName = "Iterate should produce expected result")]
    public void IterateTest()
    {
        _sut.Iterate(5, "1").Should().Be("312211");
    }
}