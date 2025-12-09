namespace AdventOfCode.Solutions.Extensions;

public static class ChunkingExtensions
{
    extension<T>(IEnumerable<T> source)
    {
        public IEnumerable<IEnumerable<T>> ChunkBy(Func<T, bool> predicate, bool dropChunkSeparator = true)
        {
            var enumerator = source.GetEnumerator();

            while (enumerator.MoveNext())
                yield return TakeUntilPredicate(enumerator, predicate, dropChunkSeparator).ToList();
        }

        public IEnumerable<IEnumerable<T>> ToSequences(Func<T, bool> predicate)
        {
            using var enumerator = source.GetEnumerator();

            while (enumerator.MoveNext())
                yield return TakeUntilPredicate(enumerator, predicate, dropSeparator: true).ToList();
        }
    }

    private static IEnumerable<T> TakeUntilPredicate<T>(IEnumerator<T> enumerator, Func<T, bool> predicate, bool dropSeparator)
    {
        do
        {
            var current = enumerator.Current;
            var matches = predicate(current);

            if (!matches || !dropSeparator)
                yield return current;

            if (matches)
                yield break;
        } while (enumerator.MoveNext());
    }
}

