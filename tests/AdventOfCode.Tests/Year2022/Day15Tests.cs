using AdventOfCode.Solutions.Year2022;

namespace AdventOfCode.Tests.Year2022;

public class Day15Tests : IClassFixture<Day15>
{
    private const string TestData = """
                                    Sensor at x=2, y=18: closest beacon is at x=-2, y=15
                                    Sensor at x=9, y=16: closest beacon is at x=10, y=16
                                    Sensor at x=13, y=2: closest beacon is at x=15, y=3
                                    Sensor at x=12, y=14: closest beacon is at x=10, y=16
                                    Sensor at x=10, y=20: closest beacon is at x=10, y=16
                                    Sensor at x=14, y=17: closest beacon is at x=10, y=16
                                    Sensor at x=8, y=7: closest beacon is at x=2, y=10
                                    Sensor at x=2, y=0: closest beacon is at x=2, y=10
                                    Sensor at x=0, y=11: closest beacon is at x=2, y=10
                                    Sensor at x=20, y=14: closest beacon is at x=25, y=17
                                    Sensor at x=17, y=20: closest beacon is at x=21, y=22
                                    Sensor at x=16, y=7: closest beacon is at x=15, y=3
                                    Sensor at x=14, y=3: closest beacon is at x=15, y=3
                                    Sensor at x=20, y=1: closest beacon is at x=15, y=3
                                    """;

    private readonly Day15 _sut;

    public Day15Tests(Day15 sut)
    {
        _sut = sut;
    }

    [Fact(DisplayName = "Part1 should return expected results from example data")]
    public void Part1Example()
    {
        _sut.SolvePart1(TestData, 10).As<int>().Should().Be(26);
    }

    [Fact(DisplayName = "Part2 should return expected results from example data")]
    public void Part2Example()
    {
        _sut.SolvePart2(TestData, (20, 20)).As<long>().Should().Be(56000011);
    }
}