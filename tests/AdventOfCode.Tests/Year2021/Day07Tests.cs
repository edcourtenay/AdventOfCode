﻿using AdventOfCode.Solutions.Year2021;

namespace AdventOfCode.Tests.Year2021;

public class Day07Tests : IClassFixture<Day07>
{
    private const string TestData = "16,1,2,0,4,2,7,1,2,14";
    private readonly Day07 _sut;

    public Day07Tests(Day07 sut)
    {
        _sut = sut;
    }

    [Fact(DisplayName = "Part1 should return expected results from example data")]
    public void Part1Example()
    {
        _sut.Part1(TestData).As<int>().Should().Be(37);
    }

    [Fact(DisplayName = "Part2 should return expected results from example data")]
    public void Part2Example()
    {
        _sut.Part2(TestData).As<int>().Should().Be(168);
    }
}