using AdventOfCode.Year2019;

namespace AdventOfCode.Tests.Year2019;

public class Day04Tests
{
    [Theory(DisplayName = "IsValid2 should produce expected results")]
    [InlineData("112233", true)]
    [InlineData("123444", false)]
    [InlineData("111122", true)]
    public void IsValid2Tests(string input, bool expected)
    {
        Day04.IsValid2(input).Should().Be(expected);
    }
}