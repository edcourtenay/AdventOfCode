using AdventOfCode.Solutions.Year2021;

namespace AdventOfCode.Tests.Year2021;

public class Day05Tests : IClassFixture<Day05>
{
    private const string TestData = """
                                    0,9 -> 5,9
                                    8,0 -> 0,8
                                    9,4 -> 3,4
                                    2,2 -> 2,1
                                    7,0 -> 7,4
                                    6,4 -> 2,0
                                    0,9 -> 2,9
                                    3,4 -> 1,4
                                    0,0 -> 8,8
                                    5,5 -> 8,2
                                    """;

    private readonly Day05 _sut;

    public Day05Tests(Day05 sut)
    {
        _sut = sut;
    }

    [Fact(DisplayName = "Part1 should return expected results from example data")]
    public void Part1Example()
    {
        _sut.Part1(TestData).As<int>().Should().Be(5);
    }

    [Fact(DisplayName = "Part2 should return expected results from example data")]
    public void Part2Example()
    {
        _sut.Part2(TestData).As<int>().Should().Be(12);
    }
}