namespace AdventOfCode.Solutions.Year2015;

[Description("Infinite Elves and Infinite Houses")]
public class Day20 : IPuzzle
{
    public object Part1(string input)
    {
        return PresentsByHouse(1000000, 10, 29000000);
    }

    public object Part2(string input)
    {
        return PresentsByHouse(50, 11, 29000000);
    }

    int PresentsByHouse(int steps, int mul, int l) {
        var presents = new int[1000000];
        for (var i = 1; i < presents.Length; i++) {
            var j = i;
            var step = 0;
            while (j < presents.Length && step < steps) {
                presents[j] += mul * i;
                j += i;
                step++;
            }
        }

        for (var i = 0; i < presents.Length; i++) {
            if (presents[i] >= l) {
                return i;
            }
        }
        return -1;
    }
}