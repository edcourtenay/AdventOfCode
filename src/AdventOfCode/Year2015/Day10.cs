using System.Text;

namespace AdventOfCode.Year2015;

[Description("Elves Look, Elves Say")]
public class Day10 : IPuzzle
{
    public object Part1(string input)
    {
        return Iterate(40, input).Length;
    }

    public object Part2(string input)
    {
        return Iterate(50, input).Length;
    }

    public string Iterate(int count, string input)
    {
        var current = input;
        while (count-- > 0)
        {
            current = LookAndSay(current);
        }

        return current;
    }

    public string LookAndSay(string input)
    {
        var sb = new StringBuilder();
        sb.AppendJoin(null, DoStuff(input));
        
        return sb.ToString();
    }

    private IEnumerable<string> DoStuff(string input)
    {
        foreach (string split in SplitStringByCharacterChange(input))
        {
            yield return split.Length.ToString();
            yield return split[0].ToString();
        }
    }

    private IEnumerable<string> SplitStringByCharacterChange(string input)
    {
        for (int i = 0; i < input.Length; i++)
        {
            int j = i;
            while (j < input.Length && input[i] == input[j])
            {
                j++;
            }

            yield return input.Substring(i, j - i);
            
            i = j - 1;
        }
    }
}