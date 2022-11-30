namespace AdventOfCode.Year2015;

[Description("Corporate Policy")]
public class Day11 : IPuzzle
{
    public object Part1(string input)
    {
        return SearchForNextPassword(input.ToCharArray());
    }

    public object Part2(string input)
    {
        return SearchForNextPassword(Part1(input).ToString()!.ToCharArray());
    }

    private string SearchForNextPassword(char[] input)
    {
        while (true)
        {
            IncrementPassword(input);
            if (AllTests(input))
            {
                return new string(input);
            }
        }
    }

    public char[] IncrementPassword(char[] password)
    {
        for (int i = password.Length - 1; i >= 0; i--)
        {
            char c = password[i];
            c++;
            if (c > 'z')
            {
                password[i] = 'a';
            }
            else
            {
                password[i] = c;
                break;
            }
        }

        return password;
    }

    public static readonly Func<char[], bool> HasRunOfCharacters = chars =>
    {
        (char?, char?) previous = (null, null);
        foreach (var c in chars)
        {
            if (previous == (c - 1, c - 2))
            {
                return true;
            }

            previous = (c, previous.Item1);
        }

        return false;
    };

    public static readonly Func<char[], bool> HasNonOverlappingPairs = chars =>
    {
        IEnumerable<char[]> SplitArrayByCharacterChange(char[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                int j = i;
                while (j < input.Length && input[i] == input[j])
                {
                    j++;
                }

                yield return input[i..j];

                i = j - 1;
            }
        }

        return SplitArrayByCharacterChange(chars)
            .Count(arr => arr.Length == 2) >= 2;
    };

    public static readonly Func<char[], bool> HasIllegalCharacters = chars =>
    {
        return chars.Any(c => "iol".Contains(c));
    };
    
    public readonly Func<char[], bool> AllTests = chars => HasRunOfCharacters(chars)
                                             && HasNonOverlappingPairs(chars)
                                             && !HasIllegalCharacters(chars);

}