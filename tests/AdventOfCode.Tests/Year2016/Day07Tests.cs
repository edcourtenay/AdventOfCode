using AdventOfCode.Solutions.Year2016;

namespace AdventOfCode.Tests.Year2016;

public class Day07Tests
{
    [Theory(DisplayName = "SupportsTls should return expected values")]
    [InlineData("abba[mnop]qrst", true)]
    [InlineData("abcd[bddb]xyyx", false)]
    [InlineData("aaaa[qwer]tyui", false)]
    [InlineData("ioxxoj[asdfgh]zxcvbn", true)]
    [InlineData("ioxxoj[asdfghbccb]zxcvbn", false)]
    public void SupportsTlsTests(string input, bool expected)
    {
        Day07.SupportsTls(input).Should().Be(expected);
    }

    [Theory(DisplayName = "SupportsSsl should return expected values")]
    [InlineData("aba[bab]xyz", true)]
    [InlineData("xyx[xyx]xyx", false)]
    [InlineData("aaa[kek]eke", true)]
    [InlineData("zazbz[bzb]cdb", true)]
    public void SupportsSslTests(string input, bool expected)
    {
        Day07.SupportsSsl(input).Should().Be(expected);
    }
}