using AdventOfCode.Solutions.Year2023;

using Hand = AdventOfCode.Solutions.Year2023.Day07.Hand;
using HandTypes = AdventOfCode.Solutions.Year2023.Day07.HandTypes;

namespace AdventOfCode.Tests.Year2023;

public class Day07Tests : IClassFixture<Day07>
{
    private const string TestData =
        """
        32T3K 765
        T55J5 684
        KK677 28
        KTJJT 220
        QQQJA 483
        """;

    private readonly Day07 _sut;

    public Day07Tests(Day07 sut)
    {
        _sut = sut;
    }

    [Theory(DisplayName = "Part1 should return expected results from example data")]
    [InlineData(TestData, 6440)]
    public void Part1Example(string input, int expected)
    {
        _sut.Part1(input).As<int>().Should().Be(expected);
    }

    [Theory(DisplayName = "Part2 should return expected results from example data")]
    [InlineData(TestData, 5905)]
    public void Part2Example(string input, int expected)
    {
        _sut.Part2(input).As<int>().Should().Be(expected);
    }

    [Theory(DisplayName = "Jokers should parse correctly")]
    [InlineData("J784T", HandTypes.HighCard, HandTypes.OnePair)]
    [InlineData("11JKQ", HandTypes.OnePair, HandTypes.ThreeOfAKind)]
    [InlineData("3232J", HandTypes.TwoPair, HandTypes.FullHouse)]
    [InlineData("33J3J", HandTypes.FullHouse, HandTypes.FiveOfAKind)]
    [InlineData("AAJAA", HandTypes.FourOfAKind, HandTypes.FiveOfAKind)]
    [InlineData("JJJJJ", HandTypes.FiveOfAKind, HandTypes.FiveOfAKind)]
    public void JokerTests(string input, HandTypes expectedWithoutJokers, HandTypes expectedWithJokers)
    {
        (new Hand(input, 0).HandType, new Hand(input, 0, true).HandType).Should()
            .Be((expectedWithoutJokers, expectedWithJokers));
    }
}