using System.Runtime.CompilerServices;

namespace AdventOfCode.Solutions.Extensions;

public static class StringExtensions
{
    extension(string input)
    {
        public IEnumerable<string> ToLines()
        {
            ArgumentNullException.ThrowIfNull(input);

            int i = 0;
            int len = input.Length;

            while (i < len)
            {
                int start = i;
                // Scan until line break
                while (i < len)
                {
                    char ch = input[i];
                    if (ch is '\n' or '\r')
                        break;
                    i++;
                }

                // Emit the line (may be empty)
                yield return input.Substring(start, i - start);

                // Consume line break(s): \r\n or single \n or \r
                if (i < len)
                {
                    char br = input[i++];
                    if (br == '\r' && i < len && input[i] == '\n')
                        i++;
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

