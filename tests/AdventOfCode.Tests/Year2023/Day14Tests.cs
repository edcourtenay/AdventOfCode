using System.Text;

using static AdventOfCode.Year2023.Day14;

using AdventOfCode.Year2023;

namespace AdventOfCode.Tests.Year2023;

public class Day14Tests : IClassFixture<Day14>
{
    private readonly Day14 _sut;

    private const string TestData =
        """
        O....#....
        O.OO#....#
        .....##...
        OO.#O....O
        .O.....O#.
        O.#..O.#.#
        ..O..#O..O
        .......O..
        #....###..
        #OO..#....
        """;

    private const string AfterSlideNorth =
        """
        OOOO.#.O..
        OO..#....#
        OO..O##..O
        O..#.OO...
        ........#.
        ..#....#.#
        ..O..#.O.O
        ..O.......
        #....###..
        #....#....
        """;

    private const string AfterCycle1 =
        """
        .....#....
        ....#...O#
        ...OO##...
        .OO#......
        .....OOO#.
        .O#...O#.#
        ....O#....
        ......OOOO
        #...O###..
        #..OO#....
        """;

    private const string AfterCycle2 =
        """
        .....#....
        ....#...O#
        .....##...
        ..O#......
        .....OOO#.
        .O#...O#.#
        ....O#...O
        .......OOO
        #..OO###..
        #.OOO#...O
        """;

    private const string AfterCycle3 =
        """
        .....#....
        ....#...O#
        .....##...
        ..O#......
        .....OOO#.
        .O#...O#.#
        ....O#...O
        .......OOO
        #...O###.O
        #.OOO#...O
        """;

    public Day14Tests(Day14 sut)
    {
        _sut = sut;
    }

    [Theory(DisplayName = "Part1 should return expected results from example data")]
    [InlineData(TestData, 136)]
    public void Part1Example(string input, int expected)
    {
        _sut.Part1(input).As<int>().Should().Be(expected);
    }

    [Theory(DisplayName = "Part2 should return expected results from example data")]
    [InlineData(TestData, 64)]
    public void Part2Example(string input, int expected)
    {
        _sut.Part2(input).As<int>().Should().Be(expected);
    }

    [Fact]
    public void TiltNorthTest()
    {
        var grid = ParseInput(TestData);
        Tilt(grid, Direction.North);

        string trim = GridToString(grid);

        trim.Should().BeEquivalentTo(AfterSlideNorth);
    }

    [Theory(DisplayName = "Cycle should return expected results from example data")]
    [InlineData(TestData, AfterCycle1)]
    [InlineData(AfterCycle1, AfterCycle2)]
    [InlineData(AfterCycle2, AfterCycle3)]
    public void CycleTest(string input, string expected)
    {
        var grid = ParseInput(input);
        Cycle(grid);

        string trim = GridToString(grid);

        trim.Should().BeEquivalentTo(expected);
    }

    private static string GridToString((int xLength, int yLength, Dictionary<(int x, int y), char> positions) grid)
    {
        var sb = new StringBuilder();
        for (int y = 0; y < grid.yLength; y++)
        {
            for (int x = 0; x < grid.xLength; x++)
            {
                sb.Append(grid.positions.GetValueOrDefault((x, y), '.'));
            }
            sb.AppendLine();
        }

        return sb.ToString().Trim();
    }
}