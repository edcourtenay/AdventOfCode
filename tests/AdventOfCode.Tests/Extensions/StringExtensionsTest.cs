using AdventOfCode.Solutions.Extensions;

namespace AdventOfCode.Tests.Extensions;

public class StringExtensionsTest
{
    [Fact(DisplayName = "ToLines should split by newline")]
    public void ToLinesBasicTest()
    {
        string input = "line1\nline2\nline3";
        var lines = input.ToLines().ToList();
        
        lines.Should().Equal("line1", "line2", "line3");
    }

    [Fact(DisplayName = "ToLines should handle CRLF")]
    public void ToLinesCRLFTest()
    {
        string input = "line1\r\nline2\r\nline3";
        var lines = input.ToLines().ToList();
        
        lines.Should().Equal("line1", "line2", "line3");
    }

    [Fact(DisplayName = "ToLines should handle CR")]
    public void ToLinesCRTest()
    {
        string input = "line1\rline2\rline3";
        var lines = input.ToLines().ToList();
        
        lines.Should().Equal("line1", "line2", "line3");
    }

    [Fact(DisplayName = "ToLines should handle mixed line endings")]
    public void ToLinesMixedEndingsTest()
    {
        string input = "line1\nline2\r\nline3\rline4";
        var lines = input.ToLines().ToList();
        
        lines.Should().Equal("line1", "line2", "line3", "line4");
    }

    [Fact(DisplayName = "ToLines should handle empty lines")]
    public void ToLinesEmptyLinesTest()
    {
        string input = "line1\n\nline3";
        var lines = input.ToLines().ToList();
        
        lines.Should().Equal("line1", "", "line3");
    }

    [Fact(DisplayName = "ToLines should handle empty string")]
    public void ToLinesEmptyStringTest()
    {
        string input = "";
        var lines = input.ToLines().ToList();
        
        lines.Should().BeEmpty();
    }

    [Fact(DisplayName = "ToLines should handle string without line breaks")]
    public void ToLinesNoLineBreaksTest()
    {
        string input = "single line";
        var lines = input.ToLines().ToList();
        
        lines.Should().Equal("single line");
    }

    [Fact(DisplayName = "ToLines should handle trailing newline")]
    public void ToLinesTrailingNewlineTest()
    {
        string input = "line1\nline2\n";
        var lines = input.ToLines().ToList();
        
        // ToLines does not yield an empty line for trailing newline
        lines.Should().Equal("line1", "line2");
    }

    [Fact(DisplayName = "ToLines with selector should transform lines")]
    public void ToLinesWithSelectorTest()
    {
        string input = "1\n2\n3";
        var numbers = input.ToLines(int.Parse).ToList();
        
        numbers.Should().Equal(1, 2, 3);
    }

    [Fact(DisplayName = "ToLines with selector should throw if selector is null")]
    public void ToLinesWithNullSelectorTest()
    {
        string input = "line1\nline2";
        var act = () => input.ToLines((Func<string, int>)null!).ToList();
        
        act.Should().Throw<ArgumentNullException>()
            .WithParameterName("selector");
    }

    [Fact(DisplayName = "ToLines with index selector should provide line index")]
    public void ToLinesWithIndexSelectorTest()
    {
        string input = "a\nb\nc";
        var indexed = input.ToLines((line, idx) => $"{idx}:{line}").ToList();
        
        indexed.Should().Equal("0:a", "1:b", "2:c");
    }

    [Fact(DisplayName = "ToLines with index selector should throw if selector is null")]
    public void ToLinesWithNullIndexSelectorTest()
    {
        string input = "line1\nline2";
        var act = () => input.ToLines((Func<string, int, string>)null!).ToList();
        
        act.Should().Throw<ArgumentNullException>()
            .WithParameterName("selector");
    }

    [Fact(DisplayName = "ToLines should throw if input is null")]
    public void ToLinesNullInputTest()
    {
        string? input = null;
        var act = () => input!.ToLines().ToList();
        
        act.Should().Throw<ArgumentNullException>();
    }
}

