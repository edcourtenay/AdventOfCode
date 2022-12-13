using AdventOfCode.Year2022;

namespace AdventOfCode.Tests.Year2022;

public class Day07Tests : IClassFixture<Day07>
{
    private readonly Day07 _sut;

    private const string TestData = """
    $ cd /
    $ ls
    dir a
    14848514 b.txt
    8504156 c.dat
    dir d
    $ cd a
    $ ls
    dir e
    29116 f
    2557 g
    62596 h.lst
    $ cd e
    $ ls
    584 i
    $ cd ..
    $ cd ..
    $ cd d
    $ ls
    4060174 j
    8033020 d.log
    5626152 d.ext
    7214296 k
    """;

    public Day07Tests(Day07 sut)
    {
        _sut = sut;
    }

}