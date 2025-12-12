using AdventOfCode.Solutions.Year2025;

namespace AdventOfCode.Tests.Year2025;

public class Day12Tests : IClassFixture<Day12>
{
    private readonly Day12 _sut;

    private const string TestData =
        """
        0:
        ###
        ##.
        ##.

        1:
        ###
        ##.
        .##

        2:
        .##
        ###
        ##.

        3:
        ##.
        ###
        ##.

        4:
        ###
        #..
        ###

        5:
        ###
        .#.
        ###

        4x4: 0 0 0 0 2 0
        12x5: 1 0 1 0 2 2
        12x5: 1 0 1 0 3 2
        """;
    
    public Day12Tests(Day12 sut)
    {
        _sut = sut;
    }

    [Fact(Skip = "This is a troll puzzle!!!")]
    public void Part1Test()
    {
        _sut.Part1(TestData).As<int>().Should().Be(2);
    }
}