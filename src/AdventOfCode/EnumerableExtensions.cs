namespace AdventOfCode;

public static class EnumerableExtensions
{
    public static IEnumerable<T[]> Permutations<T>(this T[] rgt) {
        IEnumerable<T[]> PermutationsRec(int i) {
            if (i == rgt.Length) {
                yield return rgt.ToArray();
            }

            for (var j = i; j < rgt.Length; j++) {
                (rgt[i], rgt[j]) = (rgt[j], rgt[i]);
                foreach (var perm in PermutationsRec(i + 1)) {
                    yield return perm;
                }
                (rgt[i], rgt[j]) = (rgt[j], rgt[i]);
            }
        }

        return PermutationsRec(0);
    }

    public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> source, int chunkSize)
    {
        IEnumerable<IEnumerable<T>> ChunkInternal(IEnumerable<T> source, int chunkSize)
        {
            using var enumerator = source.GetEnumerator();
            do
            {
                if (!enumerator.MoveNext())
                    yield break;

                yield return ChunkSequence(enumerator, chunkSize);
            } while (true);
        }

        IEnumerable<T> ChunkSequence(IEnumerator<T> enumerator, int chunkSize)
        {
            var count = 0;

            do
            {
                yield return enumerator.Current;
            } while (++count < chunkSize && enumerator.MoveNext());
        }

        return ChunkInternal(source, chunkSize);
    }

    public static IEnumerable<IEnumerable<T>> Window<T>(this IEnumerable<T> source, int size, bool allowPartialWindow = false)
    {
        using var enumerator = source.GetEnumerator();
        var queue = new Queue<T>();

        while (enumerator.MoveNext())
        {
            queue.Enqueue(enumerator.Current);

            if (queue.Count != size)
                continue;

            yield return queue;
            queue = new Queue<T>();
        }

        if (allowPartialWindow && queue.Count > 0)
            yield return queue;
    }

    public static IEnumerable<IEnumerable<T>> SlidingWindow<T>(this IEnumerable<T> source, int size)
    {
        using var enumerator = source.GetEnumerator();
        var queue = new Queue<T>();

        while (enumerator.MoveNext())
        {
            queue.Enqueue(enumerator.Current);
            if (queue.Count < size)
                continue;

            if (queue.Count > size)
                queue.Dequeue();

            yield return queue.ToArray();
        }
    }

    public static IEnumerable<string> ToLines(this string input)
    {
        using var reader = new StringReader(input);
        while (reader.ReadLine() is { } line)
        {
            yield return line;
        }
    }

    public static IEnumerable<T> ToLines<T>(this string input, Func<string, T> convert)
    {
        return input.ToLines().Select(convert);
    }

    public static IEnumerable<IEnumerable<T>> Pivot<T>(this IEnumerable<IEnumerable<T>> source)
    {
        var enumerators = source.Select(e => e.GetEnumerator()).ToArray();
        try
        {
            while (enumerators.All(e => e.MoveNext()))
            {
                yield return enumerators.Select(e => e.Current).ToArray();
            }
        }
        finally
        {
            Array.ForEach(enumerators, e => e.Dispose());
        }
    }

    public static IEnumerable<(T First, T Second)> Pairwise<T>(this IEnumerable<T> source)
    {
        var previous = default(T);
        using var enumerator = source.GetEnumerator();

        if (enumerator.MoveNext())
        {
            previous = enumerator.Current;
        }

        while (enumerator.MoveNext())
        {
            yield return (previous, previous = enumerator.Current)!;
        }
    }
}