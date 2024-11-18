using AdventOfCode.Solutions.Year2022;

namespace AdventOfCode.Tests.Year2022;

public class Day09Tests : IClassFixture<Day09>
{
    private const string TestData = """
                                    R 4
                                    U 4
                                    L 3
                                    D 1
                                    R 4
                                    D 1
                                    L 5
                                    R 2
                                    """;

    private readonly Day09 _sut;

    public Day09Tests(Day09 sut)
    {
        _sut = sut;
    }

    [Fact(DisplayName = "Part1 should return expected results from example data")]
    public void Part1Example()
    {
        _sut.Part1(TestData).As<int>().Should().Be(13);
    }

    [Fact(DisplayName = "Part2 should return expected results from example data")]
    public void Part2Example()
    {
        _sut.Part2(TestData).As<int>().Should().Be(1);
    }
}