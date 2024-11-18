using AdventOfCode.Solutions.Year2020;

namespace AdventOfCode.Tests.Year2020;

public class Day01Tests : IClassFixture<Day01>
{
    private const string TestData = """
                                    1721
                                    979
                                    366
                                    299
                                    675
                                    1456
                                    """;

    private readonly Day01 _sut;

    public Day01Tests(Day01 sut)
    {
        _sut = sut;
    }

    [Fact(DisplayName = "Part1 should return expected results from example data")]
    public void Part1Example()
    {
        _sut.Part1(TestData).As<int>().Should().Be(514579);
    }

    [Fact(DisplayName = "Part2 should return expected results from example data")]
    public void Part2Example()
    {
        _sut.Part2(TestData).As<int>().Should().Be(241861950);
    }
}