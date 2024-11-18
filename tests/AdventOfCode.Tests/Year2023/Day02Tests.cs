using AdventOfCode.Solutions.Year2023;

namespace AdventOfCode.Tests.Year2023;

public class Day02Tests : IClassFixture<Day02>
{
    private const string TestData =
        """
        Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
        Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
        Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
        Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
        Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green
        """;

    private readonly Day02 _sut;

    public Day02Tests(Day02 sut)
    {
        _sut = sut;
    }

    [Theory(DisplayName = "Part1 should return expected results from example data")]
    [InlineData(TestData, 8)]
    public void Part1Example(string input, int expected)
    {
        _sut.Part1(input).As<int>().Should().Be(expected);
    }

    [Theory(DisplayName = "Part2 should return expected results from example data")]
    [InlineData(TestData, 2286)]
    public void Part2Example(string input, int expected)
    {
        _sut.Part2(input).As<int>().Should().Be(expected);
    }

    [Fact(DisplayName = "ParseGame should return expected results from example data")]
    public void ParseGameTest()
    {
        Day02.Game? result =
            Day02.ParseGame("Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red");
        result.Should().BeEquivalentTo(new Day02.Game(3,
        [
            new Day02.Set(20, 8, 6),
            new Day02.Set(4, 13, 5),
            new Day02.Set(1, 5, 0)
        ]));
    }
}