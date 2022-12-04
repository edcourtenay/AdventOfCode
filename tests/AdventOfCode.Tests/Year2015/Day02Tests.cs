using AdventOfCode.Year2015;

namespace AdventOfCode2015CS.Tests;

public class Day02Tests
{
    [Theory(DisplayName = "CalculatePaper should return expected results")]
    [InlineData(2, 3, 4, 58)]
    [InlineData(1, 1, 10, 43)]
    public void Test1(int width, int length, int height, int expected)
    {
        var sut = new Day02();

        var result = sut.CalculatePaper(new Day02.Box(width, length, height));

        result.Should().Be(expected);
    }

    [Theory(DisplayName = "ParseLine should return expected Box")]
    [InlineData("2x3x4", 2, 3, 4)]
    public void TestParseLine(string input, int expectedWidth, int expectedHeight, int expectedLength)
    {
        var expectedBox = new Day02.Box(expectedWidth, expectedHeight, expectedLength);

        var sut = new Day02();

        Day02.Box result = sut.ParseLine(input);

        result.Should().Be(expectedBox);
    }

    [Theory(DisplayName = "CalculateRibbon should produce the expected result")]
    [InlineData(2, 3, 4, 34)]
    [InlineData(1, 1, 10, 14)]
    public void TestCalculateRibbon(int width, int height, int length, int expected)
    {
        var sut = new Day02();

        int result = sut.CalculateRibbon(new Day02.Box(width, height, length));

        result.Should().Be(expected);
    }
}