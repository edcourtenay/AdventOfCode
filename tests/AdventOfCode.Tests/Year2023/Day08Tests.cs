using AdventOfCode.Solutions.Year2023;

namespace AdventOfCode.Tests.Year2023;

public class Day08Tests : IClassFixture<Day08>
{
    private const string TestData1 =
        """
        RL

        AAA = (BBB, CCC)
        BBB = (DDD, EEE)
        CCC = (ZZZ, GGG)
        DDD = (DDD, DDD)
        EEE = (EEE, EEE)
        GGG = (GGG, GGG)
        ZZZ = (ZZZ, ZZZ)
        """;

    private const string TestData2 =
        """
        LLR

        AAA = (BBB, BBB)
        BBB = (AAA, ZZZ)
        ZZZ = (ZZZ, ZZZ)
        """;

    private const string TestData3 =
        """
        LR

        11A = (11B, XXX)
        11B = (XXX, 11Z)
        11Z = (11B, XXX)
        22A = (22B, XXX)
        22B = (22C, 22C)
        22C = (22Z, 22Z)
        22Z = (22B, 22B)
        XXX = (XXX, XXX)
        """;

    private readonly Day08 _sut;

    public Day08Tests(Day08 sut)
    {
        _sut = sut;
    }

    [Theory(DisplayName = "Part1 should return expected results from example data")]
    [InlineData(TestData1, 2)]
    [InlineData(TestData2, 6)]
    public void Part1Example(string input, int expected)
    {
        _sut.Part1(input).As<int>().Should().Be(expected);
    }

    [Theory(DisplayName = "Part2 should return expected results from example data")]
    [InlineData(TestData3, 6)]
    public void Part2Example(string input, int expected)
    {
        _sut.Part2(input).As<long>().Should().Be(expected);
    }
}