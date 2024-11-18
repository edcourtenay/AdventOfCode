using AdventOfCode.Solutions.Year2021;

namespace AdventOfCode.Tests.Year2021;

public class Day01Tests : IClassFixture<Day01>
{
    private const string TestData = """
                                    199
                                    200
                                    208
                                    210
                                    200
                                    207
                                    240
                                    269
                                    260
                                    263
                                    """;

    private readonly Day01 _sut;

    public Day01Tests(Day01 sut)
    {
        _sut = sut;
    }

    [Fact(DisplayName = "Part1 should return expected results from example data")]
    public void Part1Example()
    {
        _sut.Part1(TestData).As<int>().Should().Be(7);
    }

    [Fact(DisplayName = "Part2 should return expected results from example data")]
    public void Part2Example()
    {
        _sut.Part2(TestData).As<int>().Should().Be(5);
    }
}