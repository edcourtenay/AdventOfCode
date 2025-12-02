using AdventOfCode.Solutions.Year2025;

namespace AdventOfCode.Tests.Year2025;

public class Day02Tests : IClassFixture<Day02>
{
    private readonly Day02 _sut;

    private const string TestData =
        "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124";
    
    public Day02Tests(Day02 sut)
    {
        _sut = sut;
    }

    [Fact]
    public void Part1Test()
    {
        _sut.Part1(TestData).As<long>().Should().Be(1227775554L);
    }
    
    [Fact]
    public void Part2Test()
    {
        _sut.Part2(TestData).As<long>().Should().Be(4174379265L);
    }
}