using AdventOfCode.Solutions.Year2017;

namespace AdventOfCode.Tests.Year2017;

public class Day02Tests : IClassFixture<Day02>
{
    private readonly Day02 _sut;

    public Day02Tests(Day02 sut)
    {
        _sut = sut;
    }

    [Fact(DisplayName = "Part1 should return expected results from example data")]
    public void Part1Example()
    {
        const string input = """
                             5	1	9	5
                             7	5	3
                             2	4	6	8
                             """;
        _sut.Part1(input).As<int>().Should().Be(18);
    }

    [Fact(DisplayName = "Part2 should return expected results from example data")]
    public void Part2Example()
    {
        const string input = """
                             5	9	2	8
                             9	4	7	3
                             3	8	6	5
                             """;
        _sut.Part2(input).As<int>().Should().Be(9);
    }
}