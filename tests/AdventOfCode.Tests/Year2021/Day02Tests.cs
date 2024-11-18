using AdventOfCode.Solutions.Year2021;

namespace AdventOfCode.Tests.Year2021;

public class Day02Tests : IClassFixture<Day02>
{
    private const string TestData = """
                                    forward 5
                                    down 5
                                    forward 8
                                    up 3
                                    down 8
                                    forward 2
                                    """;

    private readonly Day02 _sut;

    public Day02Tests(Day02 sut)
    {
        _sut = sut;
    }

    [Fact(DisplayName = "Part1 should return expected results from example data")]
    public void Part1Example()
    {
        _sut.Part1(TestData).As<int>().Should().Be(150);
    }

    [Fact(DisplayName = "Part2 should return expected results from example data")]
    public void Part2Example()
    {
        _sut.Part2(TestData).As<int>().Should().Be(900);
    }
}