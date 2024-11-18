﻿using AdventOfCode.Solutions.Year2022;

namespace AdventOfCode.Tests.Year2022;

public class Day02Tests : IClassFixture<Day02>
{
    private const string TestData = """
                                    A Y
                                    B X
                                    C Z
                                    """;

    private readonly Day02 _sut;

    public Day02Tests(Day02 sut)
    {
        _sut = sut;
    }

    [Fact(DisplayName = "Part1 should return expected results from example data")]
    public void Part1Example()
    {
        _sut.Part1(TestData).As<int>().Should().Be(15);
    }

    [Fact(DisplayName = "Part2 should return expected results from example data")]
    public void Part2Example()
    {
        _sut.Part2(TestData).As<int>().Should().Be(12);
    }
}