using AdventOfCode.Solutions.Year2021;

namespace AdventOfCode.Tests.Year2021;

public class Day10Tests : IClassFixture<Day10>
{
    private const string TestData =
        """
        [({(<(())[]>[[{[]{<()<>>
        [(()[<>])]({[<{<<[]>>(
        {([(<{}[<>[]}>{[]{[(<()>
        (((({<>}<{<{<>}{[]{[]{}
        [[<[([]))<([[{}[[()]]]
        [{[{({}]{}}([{[{{{}}([]
        {<[[]]>}<{[{[{[]{()[[[]
        [<(<(<(<{}))><([]([]()
        <{([([[(<>()){}]>(<<{{
        <{([{{}}[<[[[<>{}]]]>[]]
        """;

    private readonly Day10 _sut;

    public Day10Tests(Day10 sut)
    {
        _sut = sut;
    }

    [Fact(DisplayName = "Part1 should return expected results from example data")]
    public void Part1Example()
    {
        _sut.Part1(TestData).As<long>().Should().Be(26397);
    }

    [Fact(DisplayName = "Part2 should return expected results from example data")]
    public void Part2Example()
    {
        _sut.Part2(TestData).As<long>().Should().Be(288957);
    }

    [Theory(DisplayName = "IsCorrupted should return expected results from example data")]
    [InlineData("[({(<(())[]>[[{[]{<()<>>", false, '\0', "}}]])})]")]
    [InlineData("[(()[<>])]({[<{<<[]>>(", false, '\0', ")}>]})")]
    [InlineData("{([(<{}[<>[]}>{[]{[(<()>", true, '}', "")]
    [InlineData("(((({<>}<{<{<>}{[]{[]{}", false, '\0', "}}>}>))))")]
    [InlineData("[[<[([]))<([[{}[[()]]]", true, ')', "")]
    [InlineData("[{[{({}]{}}([{[{{{}}([]", true, ']', "")]
    [InlineData("{<[[]]>}<{[{[{[]{()[[[]", false, '\0', "]]}}]}]}>")]
    [InlineData("[<(<(<(<{}))><([]([]()", true, ')', "")]
    [InlineData("<{([([[(<>()){}]>(<<{{", true, '>', "")]
    [InlineData("<{([{{}}[<[[[<>{}]]]>[]]", false, '\0', "])}>")]
    public void IsCorruptedExample(string input, bool isCorrupted, char corrupt, string remainder)
    {
        _sut.Validate(input).Should().Be((isCorrupted, corrupt, remainder));
    }
}