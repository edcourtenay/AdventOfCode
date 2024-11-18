﻿using System.Text;

using AdventOfCode.Solutions.Year2015;

namespace AdventOfCode.Tests.Year2015;

public class Day04Tests
{
    [Theory(DisplayName = "FindSeed should return expected result")]
    [InlineData("abcdef", 609043)]
    [InlineData("pqrstuv", 1048970)]
    public void FindSeedTest(string key, int expected)
    {
        Day04 sut = new();

        int seed = sut.FindSeed(key, Day04.Part1Test);

        seed.Should().Be(expected);
    }

    [Theory(DisplayName = "CheckMD5 should be true")]
    [InlineData("abcdef609043")]
    [InlineData("pqrstuv1048970")]
    public void CheckMD5Test(string input)
    {
        Day04 sut = new();

        bool result = sut.CheckMD5(Day04.Part1Test, Encoding.ASCII.GetBytes(input));

        result.Should().BeTrue();
    }
}