using AdventOfCode.Solutions.Extensions;

namespace AdventOfCode.Tests.Extensions;

public class WindowingExtensionsTest
{
    [Fact(DisplayName = "Window should create non-overlapping windows")]
    public void WindowTest()
    {
        int[] numbers = [1, 2, 3, 4, 5, 6];
        var windows = numbers.Window(2).ToList();
        
        windows.Should().HaveCount(3);
        windows[0].Should().Equal(1, 2);
        windows[1].Should().Equal(3, 4);
        windows[2].Should().Equal(5, 6);
    }

    [Fact(DisplayName = "Window should drop partial window by default")]
    public void WindowDropPartialTest()
    {
        int[] numbers = [1, 2, 3, 4, 5];
        var windows = numbers.Window(2).ToList();
        
        windows.Should().HaveCount(2);
        windows[0].Should().Equal(1, 2);
        windows[1].Should().Equal(3, 4);
    }

    [Fact(DisplayName = "Window should keep partial window when specified")]
    public void WindowKeepPartialTest()
    {
        int[] numbers = [1, 2, 3, 4, 5];
        var windows = numbers.Window(2, allowPartialWindow: true).ToList();
        
        windows.Should().HaveCount(3);
        windows[0].Should().Equal(1, 2);
        windows[1].Should().Equal(3, 4);
        windows[2].Should().Equal(5);
    }

    [Fact(DisplayName = "Window should handle empty sequence")]
    public void WindowEmptySequenceTest()
    {
        int[] numbers = [];
        var windows = numbers.Window(2).ToList();
        
        windows.Should().BeEmpty();
    }

    [Fact(DisplayName = "Window should handle size larger than sequence")]
    public void WindowLargeSizeTest()
    {
        int[] numbers = [1, 2, 3];
        var windows = numbers.Window(5).ToList();
        
        windows.Should().BeEmpty();
    }

    [Fact(DisplayName = "Window with size 1 should return individual elements")]
    public void WindowSize1Test()
    {
        int[] numbers = [1, 2, 3];
        var windows = numbers.Window(1).ToList();
        
        windows.Should().HaveCount(3);
        windows[0].Should().Equal(1);
        windows[1].Should().Equal(2);
        windows[2].Should().Equal(3);
    }

    [Fact(DisplayName = "SlidingWindow should create overlapping windows")]
    public void SlidingWindowTest()
    {
        int[] numbers = [1, 2, 3, 4, 5];
        var windows = numbers.SlidingWindow(3).ToList();
        
        windows.Should().HaveCount(3);
        windows[0].Should().Equal(1, 2, 3);
        windows[1].Should().Equal(2, 3, 4);
        windows[2].Should().Equal(3, 4, 5);
    }

    [Fact(DisplayName = "SlidingWindow should handle size equal to sequence length")]
    public void SlidingWindowFullSizeTest()
    {
        int[] numbers = [1, 2, 3];
        var windows = numbers.SlidingWindow(3).ToList();
        
        windows.Should().HaveCount(1);
        windows[0].Should().Equal(1, 2, 3);
    }

    [Fact(DisplayName = "SlidingWindow should handle size larger than sequence")]
    public void SlidingWindowLargeSizeTest()
    {
        int[] numbers = [1, 2, 3];
        var windows = numbers.SlidingWindow(5).ToList();
        
        windows.Should().BeEmpty();
    }

    [Fact(DisplayName = "SlidingWindow should handle empty sequence")]
    public void SlidingWindowEmptySequenceTest()
    {
        int[] numbers = [];
        var windows = numbers.SlidingWindow(2).ToList();
        
        windows.Should().BeEmpty();
    }

    [Fact(DisplayName = "SlidingWindow with size 1 should return individual elements")]
    public void SlidingWindowSize1Test()
    {
        int[] numbers = [1, 2, 3];
        var windows = numbers.SlidingWindow(1).ToList();
        
        windows.Should().HaveCount(3);
        windows[0].Should().Equal(1);
        windows[1].Should().Equal(2);
        windows[2].Should().Equal(3);
    }

    [Fact(DisplayName = "Pairwise should create consecutive pairs")]
    public void PairwiseTest()
    {
        int[] numbers = [1, 2, 3, 4, 5];
        var pairs = numbers.Pairwise().ToList();
        
        pairs.Should().HaveCount(4);
        pairs[0].Should().Be((1, 2));
        pairs[1].Should().Be((2, 3));
        pairs[2].Should().Be((3, 4));
        pairs[3].Should().Be((4, 5));
    }

    [Fact(DisplayName = "Pairwise should handle sequence with two elements")]
    public void PairwiseTwoElementsTest()
    {
        int[] numbers = [1, 2];
        var pairs = numbers.Pairwise().ToList();
        
        pairs.Should().HaveCount(1);
        pairs[0].Should().Be((1, 2));
    }

    [Fact(DisplayName = "Pairwise should handle single element")]
    public void PairwiseSingleElementTest()
    {
        int[] numbers = [1];
        var pairs = numbers.Pairwise().ToList();
        
        pairs.Should().BeEmpty();
    }

    [Fact(DisplayName = "Pairwise should handle empty sequence")]
    public void PairwiseEmptySequenceTest()
    {
        int[] numbers = [];
        var pairs = numbers.Pairwise().ToList();
        
        pairs.Should().BeEmpty();
    }
}

