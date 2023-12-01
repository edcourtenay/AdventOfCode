using AdventOfCode.Year2023;

namespace AdventOfCode.Tests.Year2023;

public class Day01Tests : IClassFixture<Day01>
{
    private readonly Day01 _sut;

    private const string Day01TestData = """
                                    1abc2
                                    pqr3stu8vwx
                                    a1b2c3d4e5f
                                    treb7uchet
                                    """;

    private const string Day02TestData = """
                                         two1nine
                                         eightwothree
                                         abcone2threexyz
                                         xtwone3four
                                         4nineeightseven2
                                         zoneight234
                                         7pqrstsixteen
                                         """;

    public Day01Tests(Day01 sut)
    {
        _sut = sut;
    }

    [Theory(DisplayName = "Part1 should return expected results from example data")]
    [InlineData(Day01TestData, 142)]
    public void Part1Example(string input, int expected)
    {
        _sut.Part1(input).As<int>().Should().Be(expected);
    }

    [Theory(DisplayName = "Part2 should return expected results from example data")]
    [InlineData(Day02TestData, 281)]
    public void Part2Example(string input, int expected)
    {
        _sut.Part2(input).As<int>().Should().Be(expected);
    }
}