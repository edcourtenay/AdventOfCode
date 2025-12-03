namespace AdventOfCode.Solutions.Extensions;

public static class StringExtensions
{
    extension(string input)
    {
        public IEnumerable<string> ToLines()
        {
            using var reader = new StringReader(input);
            while (reader.ReadLine() is { } line)
                yield return line;
        }

        public IEnumerable<T> ToLines<T>(Func<string, T> selector) =>
            input.ToLines().Select(selector);

        public IEnumerable<TResult> ToLines<TResult>(Func<string, int, TResult> selector) =>
            input.ToLines().Select(selector);
    }
}

