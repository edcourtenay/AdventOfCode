using AdventOfCode.Solutions;
using AdventOfCode.Solutions.Extensions;

namespace AdventOfCode.Tests.Extensions;

public class ChunkingExtensionsTest
{
    [Fact(DisplayName = "ChunkBy should split by predicate and drop separator")]
    public void ChunkByDropSeparatorTest()
    {
        int[] numbers = [1, 2, 0, 3, 4, 0, 5];
        var chunks = numbers.ChunkBy(x => x == 0, dropChunkSeparator: true)
            .ToList();
        
        chunks.Should().HaveCount(3);
        chunks[0].Should().Equal(1, 2);
        chunks[1].Should().Equal(3, 4);
        chunks[2].Should().Equal(5);
    }

    [Fact(DisplayName = "ChunkBy should split by predicate and keep separator")]
    public void ChunkByKeepSeparatorTest()
    {
        int[] numbers = [1, 2, 0, 3, 4, 0, 5];
        var chunks = numbers.ChunkBy(x => x == 0, dropChunkSeparator: false).ToList();
        
        chunks.Should().HaveCount(3);
        chunks[0].Should().Equal(1, 2, 0);
        chunks[1].Should().Equal(3, 4, 0);
        chunks[2].Should().Equal(5);
    }

    [Fact(DisplayName = "ChunkBy should handle empty sequence")]
    public void ChunkByEmptySequenceTest()
    {
        int[] numbers = [];
        var chunks = numbers.ChunkBy(x => x == 0).ToList();
        
        chunks.Should().BeEmpty();
    }

    [Fact(DisplayName = "ChunkBy should handle no separators")]
    public void ChunkByNoSeparatorsTest()
    {
        int[] numbers = [1, 2, 3, 4];
        var chunks = numbers.ChunkBy(x => x == 0).Select(c => c.ToList()).ToList();
        
        chunks.Should().HaveCount(1);
        chunks[0].Should().Equal(1, 2, 3, 4);
    }

    [Fact(DisplayName = "ChunkBy should handle consecutive separators")]
    public void ChunkByConsecutiveSeparatorsTest()
    {
        int[] numbers = [1, 0, 0, 2];
        var chunks = numbers.ChunkBy(x => x == 0, dropChunkSeparator: true).Select(c => c.ToList()).ToList();
        
        chunks.Should().HaveCount(3);
        chunks[0].Should().Equal(1);
        chunks[1].Should().BeEmpty();
        chunks[2].Should().Equal(2);
    }

    [Fact(DisplayName = "ToSequences should split by predicate")]
    public void ToSequencesTest()
    {
        string input = "line1\nline2\nline3";
        var sequences = input.ToSequences(c => c == '\n').Select(s => new string(s.ToArray())).ToList();
        
        sequences.Should().HaveCount(3);
        sequences[0].Should().Be("line1");
        sequences[1].Should().Be("line2");
        sequences[2].Should().Be("line3");
    }

    [Fact(DisplayName = "ToSequences should handle empty lines")]
    public void ToSequencesEmptyLinesTest()
    {
        string input = "a\n\nb";
        var sequences = input.ToSequences(c => c == '\n').Select(s => new string(s.ToArray())).ToList();
        
        sequences.Should().HaveCount(3);
        sequences[0].Should().Be("a");
        sequences[1].Should().Be("");
        sequences[2].Should().Be("b");
    }
}

