using AdventOfCode.Year2015;

namespace AdventOfCode2015CS.Tests;

public class Day10Tests
{
    [Theory(DisplayName="LookAndSay should produce expected results")]
    [InlineData("1", "11")]
    [InlineData("11", "21")]
    [InlineData("21", "1211")]
    [InlineData("1211", "111221")]
    [InlineData("111221", "312211")]
    public void LookAndSayTests(string input, string expected)
    {
        var sut = new Day10();

        string result = sut.LookAndSay(input);

        result.Should().Be(expected);
    }

    [Fact(DisplayName = "Iterate should produce expected result")]
    public void IterateTest()
    {
        var sut = new Day10();

        string result = sut.Iterate(5, "1");

        result.Should().Be("312211");
    }
}