using AdventOfCode.Year2023;

namespace AdventOfCode.Tests.Year2023;

public class Day07Tests : IClassFixture<Day07>
{
    private readonly Day07 _sut;

    private const string TestData =
        """
        32T3K 765
        T55J5 684
        KK677 28
        KTJJT 220
        QQQJA 483
        """;

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
    [InlineData("J784T", Day07.HandTypes.HighCard, Day07.HandTypes.OnePair)]
    [InlineData("11JKQ", Day07.HandTypes.OnePair, Day07.HandTypes.ThreeOfAKind)]
    [InlineData("3232J", Day07.HandTypes.TwoPair, Day07.HandTypes.FullHouse)]
    [InlineData("33J3J", Day07.HandTypes.FullHouse, Day07.HandTypes.FiveOfAKind)]
    [InlineData("AAJAA", Day07.HandTypes.FourOfAKind, Day07.HandTypes.FiveOfAKind)]
    [InlineData("JJJJJ", Day07.HandTypes.FiveOfAKind, Day07.HandTypes.FiveOfAKind)]
    public void JokerTests(string input, Day07.HandTypes expectedWithoutJokers, Day07.HandTypes expectedWithJokers)
    {
        (new Day07.Hand(input, 0, false).HandType, new Day07.Hand(input, 0, true).HandType).Should().Be((expectedWithoutJokers, expectedWithJokers));
    }

}