using AdventOfCode.Solutions.Year2025;

namespace AdventOfCode.Tests.Year2025;

public class Day08Tests : IClassFixture<Day08>
{
    private readonly Day08 _sut;

    private const string TestData =
        """
        162,817,812
        57,618,57
        906,360,560
        592,479,940
        352,342,300
        466,668,158
        542,29,236
        431,825,988
        739,650,466
        52,470,668
        216,146,977
        819,987,18
        117,168,530
        805,96,715
        346,949,466
        970,615,88
        941,993,340
        862,61,35
        984,92,344
        425,690,689
        """;

    public Day08Tests(Day08 sut)
    {
        _sut = sut;
        _sut.Part1Iterations = 10;
    }

    [Fact]
    public void Part1Test()
    {
        _sut.Part1(TestData).As<int>().Should().Be(40);
    }

    [Fact]
    public void Part2Test()
    {
        _sut.Part2(TestData).As<long>().Should().Be(25272);
    }
}