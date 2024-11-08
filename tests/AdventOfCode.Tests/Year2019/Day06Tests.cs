using AdventOfCode.Year2019;

namespace AdventOfCode.Tests.Year2019;

public class Day06Tests : IClassFixture<Day06>
{
    private readonly Day06 _sut;

    private const string Part1TestData = 
        """
        COM)B
        B)C
        C)D
        D)E
        E)F
        B)G
        G)H
        D)I
        E)J
        J)K
        K)L
        """;

    private const string Part2TestData =
        """
        COM)B
        B)C
        C)D
        D)E
        E)F
        B)G
        G)H
        D)I
        E)J
        J)K
        K)L
        K)YOU
        I)SAN
        """;
    
    public Day06Tests(Day06 sut)
    {
        _sut = sut;
    }

    [Fact]
    public void Part1Tests()
    {
        _sut.Part1(Part1TestData).Should().Be(42);
    }
    
    [Fact]
    public void Part2Tests()
    {
        _sut.Part2(Part2TestData).Should().Be(4);
    }
}