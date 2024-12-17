using System.Text;

using AdventOfCode.Solutions.Year2015;

namespace AdventOfCode.Tests.Year2015;

public class Day04Tests : IClassFixture<Day04>
{
    private readonly Day04 _sut;

    public Day04Tests(Day04 sut)
    {
        _sut = sut;
    }

    [Theory(DisplayName = "FindSeed should return expected result")]
    [InlineData("abcdef", 609043)]
    [InlineData("pqrstuv", 1048970)]
    public void FindSeedTest(string key, int expected)
    {
        _sut.FindSeed(key, Day04.Part1Test).As<int>().Should().Be(expected);
    }

    [Theory(DisplayName = "CheckMD5 should be true")]
    [InlineData("abcdef609043")]
    [InlineData("pqrstuv1048970")]
    public void CheckMD5Test(string input)
    {
        _sut.CheckMD5(Day04.Part1Test, Encoding.ASCII.GetBytes(input)).Should().BeTrue();
    }
}