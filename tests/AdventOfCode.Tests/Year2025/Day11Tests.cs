using AdventOfCode.Solutions.Year2025;

namespace AdventOfCode.Tests.Year2025;

public class Day11Tests : IClassFixture<Day11>
{
    private readonly Day11 _sut;

    private const string Part1TestData =
        """
        aaa: you hhh 
        you: bbb ccc 
        bbb: ddd eee 
        ccc: ddd eee fff 
        ddd: ggg 
        eee: out 
        fff: out 
        ggg: out 
        hhh: ccc fff iii 
        iii: out
        """;
    
    private const string Part2TestData =
        """
        svr: aaa bbb
        aaa: fft
        fft: ccc
        bbb: tty
        tty: ccc
        ccc: ddd eee
        ddd: hub
        hub: fff
        eee: dac
        dac: fff
        fff: ggg hhh
        ggg: out
        hhh: out
        """;

    public Day11Tests(Day11 sut)
    {
        _sut = sut;
    }

    [Fact]
    public void ParseInput_CreatesCorrectGraphStructure()
    {
        var graph = Day11.ParseInput(Part1TestData);
        
        // Verify all nodes were created (11 unique nodes in the test data)
        graph.Nodes.Should().HaveCount(11);
        graph.Nodes.Should().ContainKeys("aaa", "you", "bbb", "ccc", "ddd", "eee", "fff", "ggg", "hhh", "iii", "out");
        
        // Verify connections for "aaa"
        var aaaNode = graph.GetNode("aaa");
        aaaNode.Should().NotBeNull();
        aaaNode.Children.Should().HaveCount(2);
        aaaNode.Children.Select(c => c.Name).Should().Contain(["you", "hhh"]);
        
        // Verify connections for "you"
        var youNode = graph.GetNode("you");
        youNode.Should().NotBeNull();
        youNode.Children.Should().HaveCount(2);
        youNode.Children.Select(c => c.Name).Should().Contain(["bbb", "ccc"]);
        
        // Verify connections for "ccc"
        var cccNode = graph.GetNode("ccc");
        cccNode.Should().NotBeNull();
        cccNode.Children.Should().HaveCount(3);
        cccNode.Children.Select(c => c.Name).Should().Contain(["ddd", "eee", "fff"]);
        
        // Verify leaf nodes have no children
        var outNode = graph.GetNode("out");
        outNode.Should().NotBeNull();
        outNode!.Children.Should().BeEmpty();
    }

    [Fact]
    public void CountPaths_CalculatesCorrectPathCount()
    {
        var graph = Day11.ParseInput(Part1TestData);
        
        // Test path from "aaa" to "out"
        // Expected paths: aaa -> you -> bbb -> ddd -> ggg -> out
        //                 aaa -> you -> bbb -> eee -> out
        //                 aaa -> you -> ccc -> ddd -> ggg -> out
        //                 aaa -> you -> ccc -> eee -> out
        //                 aaa -> you -> ccc -> fff -> out
        //                 aaa -> hhh -> ccc -> ddd -> ggg -> out
        //                 aaa -> hhh -> ccc -> eee -> out
        //                 aaa -> hhh -> ccc -> fff -> out
        //                 aaa -> hhh -> fff -> out
        //                 aaa -> hhh -> iii -> out
        var pathCount = Day11.CountPaths(graph, "aaa", "out");
        pathCount.Should().Be(10);
        
        // Test path from "you" to "out"
        var youToOutCount = Day11.CountPaths(graph, "you", "out");
        youToOutCount.Should().Be(5);
        
        // Test path from "bbb" to "out"
        var bbbToOutCount = Day11.CountPaths(graph, "bbb", "out");
        bbbToOutCount.Should().Be(2);
        
        // Test path from node to itself
        var selfPath = Day11.CountPaths(graph, "out", "out");
        selfPath.Should().Be(1);
        
        // Test path that doesn't exist
        var noPath = Day11.CountPaths(graph, "out", "aaa");
        noPath.Should().Be(0);
    }

    [Fact]
    public void Part1Test()
    {
        _sut.Part1(Part1TestData).As<int>().Should().Be(5);
    }

    [Fact]
    public void Part2Test()
    {
        _sut.Part2(Part2TestData).As<long>().Should().Be(2);
    }
}

