using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace AdventOfCode.Year2015;

[SuppressMessage("ReSharper", "UnusedType.Global")]
[Description("Matchsticks")]
public class Day08 : IPuzzle
{
    public object Part1(string input)
    {
        return Data(input).Select(s => s.Length - Unencode(s).Count()).Sum();
    }

    public object Part2(string input)
    {
        return Data(input).Select(s => Encode(s).Count() - s.Length).Sum();
    }

    private IEnumerable<char> Unencode(string input)
    {
        using var enumerator = input[1..^1].GetEnumerator();

        bool escape = false;
        while (enumerator.MoveNext())
        {
            var c = enumerator.Current;

            if (escape == false)
            {
                if (c == '\\')
                {
                    escape = true;
                }
                else
                {
                    yield return enumerator.Current;
                }
            }
            else
            {
                escape = false;
                if (c != 'x')
                {
                    yield return c;
                }
                else
                {
                    var hexString = "";
                    enumerator.MoveNext();
                    hexString += enumerator.Current;
                    enumerator.MoveNext();
                    hexString += enumerator.Current;

                    var hex = int.Parse(hexString, NumberStyles.HexNumber);
                    yield return Convert.ToChar(hex);
                }
            }
        }
    }
    
    private IEnumerable<char> Encode(string input)
    {
        yield return '"';
            
        using var enumerator = input.GetEnumerator();
        while (enumerator.MoveNext())
        {
            switch (enumerator.Current)
            {
                case '"' or '\\':
                    yield return '\\';
                    yield return enumerator.Current;
                    break;
                    
                default:
                    yield return enumerator.Current;
                    break;
            }
        }

        yield return '"';
    }

    public IEnumerable<string> Data(string input)
    {
        using var r = new StringReader(input);

        while (r.ReadLine() is { } line)
        {
            yield return line;
        }
    }
}