using AdventOfCode.Solutions.Year2025;

namespace AdventOfCode.Tests.Year2025;

public class Day10Tests : IClassFixture<Day10>
{
    private readonly Day10 _sut;

    private const string TestData =
        """
        [.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}
        [...#.] (0,2,3,4) (2,3) (0,4) (0,1,2) (1,2,3,4) {7,5,12,7,2}
        [.###.#] (0,1,2,3,4) (0,3,4) (0,1,2,4,5) (1,2) {10,11,11,5,10,5}
        """;
    
    public Day10Tests(Day10 sut)
    {
        _sut = sut;
    }

    [Fact]
    public void Part1Test()
    {
        _sut.Part1(TestData).As<int>().Should().Be(7);
    }
    
    [Fact]
    public void Part2Test()
    {
        _sut.Part2(TestData).As<int>().Should().Be(33);
    }
}