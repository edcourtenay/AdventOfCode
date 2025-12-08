using System.Runtime.CompilerServices;

namespace AdventOfCode.Solutions.Extensions;

public static class StringExtensions
{
    extension(string input)
    {
        public IEnumerable<string> ToLines()
        {
            ArgumentNullException.ThrowIfNull(input);

            if (input.Length == 0)
            {
                yield break;
            }

            int start = 0;
            int length = input.Length;

            while (start < length)
            {
                // Find the next line break using optimized IndexOfAny on span
                int lineBreakIndex = input.AsSpan(start).IndexOfAny('\r', '\n');

                if (lineBreakIndex == -1)
                {
                    // No more line breaks - yield the rest of the string
                    yield return input[start..];
                    yield break;
                }

                // Calculate absolute position
                int absoluteIndex = start + lineBreakIndex;

                // Yield the line
                yield return input.Substring(start, lineBreakIndex);

                // Consume line break(s): \r\n or single \n or \r
                start = absoluteIndex + 1;
                if (absoluteIndex < length - 1 &&
                    input[absoluteIndex] == '\r' &&
                    input[absoluteIndex + 1] == '\n')
                {
                    start++; // Skip the \n in \r\n
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> ToLines<T>(Func<string, T> selector) =>
            input.ToLines().Select(selector ?? throw new ArgumentNullException(nameof(selector)));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<TResult> ToLines<TResult>(Func<string, int, TResult> selector) =>
            input.ToLines().Select(selector ?? throw new ArgumentNullException(nameof(selector)));
    }
}

