using AdventOfCode.Year2017;

namespace AdventOfCode.Tests.Year2017;

public class Day09Tests : IClassFixture<Day09>
{
    private readonly Day09 _sut;

    public Day09Tests(Day09 sut)
    {
        _sut = sut;
    }

    [Theory(DisplayName = "Groups should return expected results from example data")]
    [InlineData("<>")]
    [InlineData("<random characters>")]
    [InlineData("<<<<>")]
    [InlineData("<{!>}>")]
    [InlineData("<!!>")]
    [InlineData("<!!!>>")]
    [InlineData("<{o\"i!a,<{i<a>")]
    public void SelfContainedGarbageExample(string input)
    {
        Day09.Groups(input).Count(s => !string.IsNullOrEmpty(s)).Should().Be(0);
    }

    [Theory(DisplayName = "Parse should return expected results from example data")]
    [InlineData(@"{}", 1)]
    [InlineData(@"{{{}}}", 6)]
    [InlineData(@"{{},{}}", 5)]
    [InlineData(@"{{{},{},{{}}}}", 16)]
    [InlineData(@"{<a>,<a>,<a>,<a>}", 1)]
    [InlineData(@"{{<ab>},{<ab>},{<ab>},{<ab>}}}", 5)]
    [InlineData(@"{{<!!>},{<!!>},{<!!>},{<!!>}}", 5)]
    [InlineData(@"{{<a!>},{<a!>},{<a!>},{<ab>}}", 3)]
    public void ParseExample(string input, int expected)
    {
        Day09.Parse(input).Score.Should().Be(expected);
    }
}