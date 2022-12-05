using AdventOfCode.Year2022;

namespace AdventOfCode.Tests.Year2022;

public class Day05Tests : IClassFixture<Day05>
{
    private readonly Day05 _sut;

    private const string TestData = """
    [D]
    [N] [C]
    [Z] [M] [P]
     1   2   3

    move 1 from 2 to 1
    move 3 from 1 to 3
    move 2 from 2 to 1
    move 1 from 1 to 2
    """;

    public Day05Tests(Day05 sut)
    {
        _sut = sut;
    }

    [Fact(DisplayName = "Part1 should return expected results from example data")]
    public void Part1Example()
    {
        _sut.Part1(TestData).As<string>().Should().Be("CMZ");
    }

    [Fact(DisplayName = "Part2 should return expected results from example data")]
    public void Part2Example()
    {
        _sut.Part2(TestData).As<string>().Should().Be("CMZ");
    }
}