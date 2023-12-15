using System.Text.RegularExpressions;

namespace AdventOfCode.Year2023;

[Description("Lens Library")]
public partial class Day15 : IPuzzle
{
    [GeneratedRegex("([a-z]+)([=-])([0-9]?)", RegexOptions.Compiled)]
    public static partial Regex LabelRegex();

    public object Part1(string input)
    {
        ReadOnlySpan<char> source = input.AsSpan();

        var sum = 0L;
        foreach (var match in LabelRegex().EnumerateMatches(source))
        {
            sum += CalculateHash(source.Slice(match.Index, match.Length));
        }
        return sum;
    }

    public object Part2(string input)
    {
        ReadOnlySpan<char> source = input.AsSpan();

        var boxes = Enumerable.Repeat(0, 256).Select(_ => new LinkedList<(string label, int focalLength)>()).ToArray();

        var sum = 0L;
        foreach (var match in LabelRegex().EnumerateMatches(source))
        {
            ParseLabel(source.Slice(match.Index, match.Length), out var boxId, out var label, out var operation, out var focalLength);
            LinkedList<(string label, int focalLength)> box = boxes[boxId];
            LinkedListNode<(string label, int focalLength)>? node = box.First;
            switch (operation)
            {
                case '-':
                    while (node != null)
                    {
                        if (label.SequenceEqual(node.Value.label))
                        {
                            box.Remove(node);
                            break;
                        }

                        node = node.Next;
                    }

                    break;

                case '=':
                    bool replaced = false;
                    LinkedListNode<(string label, int focalLength)> newNode = new((label.ToString(), focalLength));
                    while (node != null)
                    {
                        if (label.SequenceEqual(node.Value.label))
                        {
                            box.AddAfter(node, newNode);
                            box.Remove(node);
                            replaced = true;
                            break;
                        }

                        node = node.Next;
                    }

                    if (!replaced)
                    {
                        box.AddLast(newNode);
                    }
                    break;
            }
        }

        for (int i = 0; i < boxes.Length; i++)
        {
            var j = 1;
            var node = boxes[i].First;
            while (node != null)
            {
                sum += (i + 1) * (j++) * node.Value.focalLength;
                node = node.Next;
            }
        }

        return sum;
    }

    private void ParseLabel(ReadOnlySpan<char> slice, out int boxId, out ReadOnlySpan<char> label, out char operation,
        out int focalLength)
    {
        boxId = CalculateBoxId(slice);

        var index = slice.IndexOfAny('-','=');
        label = slice[..index];
        operation = slice[index];
        focalLength = operation == '-'
            ? 0
            : slice[index + 1] - '0';
    }

    private static int CalculateBoxId(ReadOnlySpan<char> span)
    {
        for (int i = 0; i < span.Length; i++)
        {
            if (char.IsAsciiLetter(span[i]))
            {
                continue;
            }

            return CalculateHash(span[..i]);
        }

        return 0;
    }

    private static int CalculateHash(ReadOnlySpan<char> span)
    {
        int current = 0;
        foreach (var c in span)
        {
            current = ((current + c) * 17) & 0b1111_1111;
        }

        return current;
    }
}