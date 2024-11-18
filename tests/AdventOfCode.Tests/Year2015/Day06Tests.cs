using AdventOfCode.Solutions.Year2015;

namespace AdventOfCode.Tests.Year2015;

public class Day06Tests
{
    [Theory(DisplayName = "ParseLine should produce expected results")]
    [InlineData("toggle 83,575 through 915,728", "toggle", 83, 575, 915, 728)]
    public void ParseLineTests(string line, string operation, int x1, int y1, int x2, int y2)
    {
        Day06 sut = new();

        Day06.Instruction instruction = sut.ParseLine(line);

        instruction.Should().Be(new Day06.Instruction(operation, x1, y1, x2, y2));
    }
}