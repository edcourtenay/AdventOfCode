using AdventOfCode.Solutions.Year2024;

namespace AdventOfCode.Tests.Year2024;

public class Day03Tests : IClassFixture<Day03>
{
    private readonly Day03 _sut;

    private const string Part1TestData = "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))";
    private const string Part2TestData = "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))";

    public Day03Tests(Day03 sut)
    {
        _sut = sut;
    }

    [Fact]
    public void Part1Example()
    {
        _sut.Part1(Part1TestData).As<int>().Should().Be(161);
    }
    
    [Fact]
    public void Part2Example()
    {
        _sut.Part2(Part2TestData).As<int>().Should().Be(48);
    }
}
