namespace AdventOfCode.Year2018;

[Description("Chronal Calibration")]
public class Day01 : IPuzzle
{
    public object Part1(string input) =>
        input.ToLines(int.Parse).Sum();

    public object Part2(string input)
    {
        var linkedList = new LinkedList<int>(input.ToLines(int.Parse));
        var set = new HashSet<int>();
        var sum = 0;

        var node = linkedList.First;
        while (true)
        {
            sum += node!.Value;
            if (set.Contains(sum))
                return sum;

            set.Add(sum);
            node = node.NextOrFirst();
        }
    }
}