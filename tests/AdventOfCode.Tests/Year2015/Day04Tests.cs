using System.Text;

using AdventOfCode.Year2015;

namespace AdventOfCode2015CS.Tests;

public class Day04Tests
{
    [Theory(DisplayName = "FindSeed should return expected result")]
    [InlineData("abcdef", 609043)]
    [InlineData("pqrstuv", 1048970)]
    public void FindSeedTest(string key, int expected)
    {
        var sut = new Day04();

        var seed = sut.FindSeed(key, Day04.Part1Test);

        seed.Should().Be(expected);
    }

    [Theory(DisplayName = "CheckMD5 should be true")]
    [InlineData("abcdef609043")]
    [InlineData("pqrstuv1048970")]
    public void CheckMD5Test(string input)
    {
        var sut = new Day04();

        var result = sut.CheckMD5(Day04.Part1Test, Encoding.ASCII.GetBytes(input));

        result.Should().BeTrue();
    }
}