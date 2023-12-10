using System.Runtime.InteropServices;

using AdventOfCode.Year2023;

namespace AdventOfCode.Tests.Year2023;

public class Day10Tests : IClassFixture<Day10>
{
    private readonly Day10 _sut;

    private const string Part1TestData =
        """
        7-F7-
        .FJ|7
        SJLL7
        |F--J
        LJ.LJ
        """;

    private const string Part2TestData =
        """
        FF7FSF7F7F7F7F7F---7
        L|LJ||||||||||||F--J
        FL-7LJLJ||||||LJL-77
        F--JF--7||LJLJ7F7FJ-
        L---JF-JLJ.||-FJLJJ7
        |F|F-JF---7F7-L7L|7|
        |FFJF7L7F-JF7|JL---7
        7-L-JL7||F7|L7F-7F7|
        L.L7LFJ|||||FJL7||LJ
        L7JLJL-JLJLJL--JLJ.L
        """;

    public Day10Tests(Day10 sut)
    {
        _sut = sut;
    }

    [Theory(DisplayName = "Part1 should return expected results from example data")]
    [InlineData(Part1TestData, 8)]
    public void Part1Example(string input, int expected)
    {
        _sut.Part1(input).As<int>().Should().Be(expected);
    }

    [Theory(DisplayName = "Part2 should return expected results from example data")]
    [InlineData(Part2TestData, 10)]
    public void Part2Example(string input, int expected)
    {
        _sut.Part2(input).As<int>().Should().Be(expected);
    }
}