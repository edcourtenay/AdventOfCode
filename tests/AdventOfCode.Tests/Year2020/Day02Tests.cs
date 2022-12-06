using AdventOfCode.Year2020;

namespace AdventOfCode.Tests.Year2020;

public class Day02Tests : IClassFixture<Day02>
{
    private readonly Day02 _sut;

    private const string TestData = """
    1-3 a: abcde
    1-3 b: cdefg
    2-9 c: ccccccccc
    """;

    public Day02Tests(Day02 sut)
    {
        _sut = sut;
    }

    [Fact(DisplayName = "Part1 should return expected results from example data")]
    public void Part1Example()
    {
        _sut.Part1(TestData).As<int>().Should().Be(2);
    }

    [Fact(DisplayName = "Part2 should return expected results from example data")]
    public void Part2Example()
    {
        _sut.Part2(TestData).As<int>().Should().Be(1);
    }
}