using AdventOfCode.Year2023;

namespace AdventOfCode.Tests.Year2023;

public class Day15Tests : IClassFixture<Day15>
{
    private readonly Day15 _sut;

    private const string TestData =
        """
        rn=1,cm-,qp=3,cm=2,qp-,pc=4,ot=9,ab=5,pc-,pc=6,ot=7
        """;

    public Day15Tests(Day15 sut)
    {
        _sut = sut;
    }

    [Theory(DisplayName = "Part1 should return expected results from example data")]
    [InlineData(TestData, 1320)]
    public void Part1Example(string input, long expected)
    {
        _sut.Part1(input).As<long>().Should().Be(expected);
    }

    [Theory(DisplayName = "Part2 should return expected results from example data")]
    [InlineData(TestData, 145)]
    public void Part2Example(string input, long expected)
    {
        _sut.Part2(input).As<long>().Should().Be(expected);
    }
}