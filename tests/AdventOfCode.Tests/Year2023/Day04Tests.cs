using AdventOfCode.Year2023;

namespace AdventOfCode.Tests.Year2023;

public class Day04Tests : IClassFixture<Day04>
{
    private readonly Day04 _sut;

    private const string TestData =
        """
        Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
        Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
        Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
        Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
        Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
        Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11
        """;

    public Day04Tests(Day04 sut)
    {
        _sut = sut;
    }

    [Theory(DisplayName = "Part1 should return expected results from example data")]
    [InlineData(TestData, 13)]
    public void Part1Example(string input, int expected)
    {
        _sut.Part1(input).As<int>().Should().Be(expected);
    }

    [Theory(DisplayName = "Part2 should return expected results from example data")]
    [InlineData(TestData, 30)]
    public void Part2Example(string input, int expected)
    {
        _sut.Part2(input).As<int>().Should().Be(expected);
    }

    [Fact(DisplayName = "ParseLine should return expected results from example data")]
    public void ParseLineExample()
    {
        Day04.ParseLine("Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53").Should()
            .BeEquivalentTo(new Day04.Card(1,
                new[] { 41, 48, 83, 86, 17 },
                new[] { 83, 86, 6, 31, 17, 9, 48, 53 }));
    }

    [Theory(DisplayName = "Card Points")]
    [InlineData("Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53", 8)]
    [InlineData("Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19", 2)]
    [InlineData("Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1", 2)]
    [InlineData("Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83", 1)]
    [InlineData("Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36", 0)]
    [InlineData("Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11", 0)]
    public void CardPointsExample(string line, int expected)
    {
        Day04.ParseLine(line).Points.Should().Be(expected);
    }
}