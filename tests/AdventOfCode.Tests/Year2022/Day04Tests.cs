using AdventOfCode.Solutions.Year2022;

namespace AdventOfCode.Tests.Year2022;

public class Day04Tests : IClassFixture<Day04>
{
    private const string TestData = """
                                    2-4,6-8
                                    2-3,4-5
                                    5-7,7-9
                                    2-8,3-7
                                    6-6,4-6
                                    2-6,4-8
                                    """;

    private readonly Day04 _sut;

    public Day04Tests(Day04 sut)
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
        _sut.Part2(TestData).As<int>().Should().Be(4);
    }
}