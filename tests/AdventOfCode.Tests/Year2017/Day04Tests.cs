using AdventOfCode.Solutions.Year2017;

namespace AdventOfCode.Tests.Year2017;

public class Day04Tests : IClassFixture<Day04>
{
    private readonly Day04 _sut;

    public Day04Tests(Day04 sut)
    {
        _sut = sut;
    }

    [Theory(DisplayName = "Validate line should return expected results from example data")]
    [InlineData("aa bb cc dd ee", true)]
    [InlineData("aa bb cc dd aa", false)]
    [InlineData("aa bb cc dd aaa", true)]
    public void ValidateLineExample(string input, bool expected)
    {
        _sut.ValidateLine(input, word => new string(Enumerable.OrderBy(word.ToCharArray(), c => c).ToArray())).Should()
            .Be(expected);
    }

    [Fact]
    public void Temp()
    {
        new string("cba".ToCharArray().OrderBy(c => c).ToArray()).Should().Be("abc");
    }
}