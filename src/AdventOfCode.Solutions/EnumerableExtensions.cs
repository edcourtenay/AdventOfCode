namespace AdventOfCode.Solutions;

public static class EnumerableExtensions
{
    public static IEnumerable<T[]> Permutations<T>(this T[] source) {
        return PermutationsRec(0);

        IEnumerable<T[]> PermutationsRec(int i) {
            if (i == source.Length) {
                yield return source.ToArray();
            }

            for (var j = i; j < source.Length; j++) {
                (source[i], source[j]) = (source[j], source[i]);
                foreach (var perm in PermutationsRec(i + 1)) {
                    yield return perm;
                }
                (source[i], source[j]) = (source[j], source[i]);
            }
        }
    }

    public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> source, int chunkSize)
    {
        using var enumerator = source.GetEnumerator();
        do
        {
            if (!enumerator.MoveNext())
                yield break;

            yield return ChunkSequence(enumerator);
        } while (true);

        IEnumerable<T> ChunkSequence(IEnumerator<T> sourceEnumerator)
        {
            var count = 0;

            do
            {
                yield return sourceEnumerator.Current;
            } while (++count < chunkSize && sourceEnumerator.MoveNext());
        }
    }

    public static IEnumerable<IEnumerable<T>> ChunkBy<T>(this IEnumerable<T> source, Func<T, bool> predicate, bool dropChunkSeparator = true)
    {
        using var enumerator = source.GetEnumerator();
        do
        {
            if (!enumerator.MoveNext())
                yield break;

            yield return ChunkSequence(enumerator);
        } while (true);

        IEnumerable<T> ChunkSequence(IEnumerator<T> sourceEnumerator)
        {
            bool predicateMatched;
            do
            {
                predicateMatched = predicate(sourceEnumerator.Current);

                if (!predicateMatched || !dropChunkSeparator)
                {
                    yield return sourceEnumerator.Current;
                }
            } while (!predicateMatched && sourceEnumerator.MoveNext());
        }
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

    public static IEnumerable<T> ToLines<T>(this string input, Func<string, T> selector)
    {
        return input.ToLines().Select(selector);
    }

    public static IEnumerable<TResult> ToLines<TResult>(this string input, Func<string, int, TResult> selector)
    {
        int index = -1;
        foreach (string line in input.ToLines())
        {
            index++;
            yield return selector(line, index);
        }
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

    public static IEnumerable<IEnumerable<string>> ToSequences(this IEnumerable<string> source, Func<string, bool> predicate)
    {
        using IEnumerator<string> enumerator = source.GetEnumerator();
        do
        {
            if (!enumerator.MoveNext())
                yield break;

            yield return GroupSequence(enumerator, predicate);
        } while (true);

        IEnumerable<string> GroupSequence(IEnumerator<string> enumerator, Func<string, bool> func)
        {
            do
            {
                yield return enumerator.Current;
            } while (enumerator.MoveNext() && !func(enumerator.Current));
        }
    }

    public static IEnumerable<IEnumerable<T>> Combinations<T>(this IEnumerable<T> source, int width)
    {
        var arr = source as T[] ?? source.ToArray();

        return GetCombinations(n: arr.Length, k: width)
            .Select(indexes => indexes.Select(i => arr[i - 1]));

        static IEnumerable<int[]> GetCombinations(int k, int n)
        {
            var result = new int[k];
            var stack = new Stack<int>();
            stack.Push(1);

            while (stack.Count > 0)
            {
                var index = stack.Count - 1;
                var value = stack.Pop();

                while (value <= n)
                {
                    result[index++] = value++;
                    stack.Push(value);
                    if (index == k)
                    {
                        yield return result.ToArray();
                        break;
                    }
                }
            }
        }
    }

    public static IEnumerable<T> Flatten<T>(this IEnumerable<T> e, Func<T, IEnumerable<T>> f)
    {
        IEnumerable<T> enumerable = e as T[] ?? e.ToArray();
        return enumerable.SelectMany(c => f(c).Flatten(f)).Concat(enumerable);
    }

    public static IEnumerable<T> TakeUntil<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        foreach (T item in source)
        {
            yield return item;
            if (predicate(item))
            {
                yield break;
            }
        }
    }

    public static (T Min, T Max) MinMax<T>(this IEnumerable<T> source) where T : IComparable<T>
    {
        return MinMax(source, Comparer<T>.Default);
    }

    public static (T Min, T Max) MinMax<T>(this IEnumerable<T> source, IComparer<T> comparer)
    {
        using var enumerator = source.GetEnumerator();

        if (!enumerator.MoveNext())
        {
            throw new InvalidOperationException("Sequence contains no elements.");
        }

        var min = enumerator.Current;
        var max = enumerator.Current;
        while (enumerator.MoveNext())
        {
            if (comparer.Compare(enumerator.Current, min) < 0)
            {
                min = enumerator.Current;
            }

            if (comparer.Compare(enumerator.Current, max) > 0)
            {
                max = enumerator.Current;
            }
        }

        return (min, max);
    }

    public static (TValue Min, TValue Max) MinMaxBy<TSource, TValue>(this IEnumerable<TSource> source, Func<TSource, TValue> selector) where TValue : IComparable<TValue>
    {
        return MinMaxBy(source, selector, Comparer<TValue>.Default);
    }

    private static (TValue Min, TValue Max) MinMaxBy<TSource, TValue>(IEnumerable<TSource> source, Func<TSource, TValue> selector, Comparer<TValue> comparer) where TValue : IComparable<TValue>
    {
        using var enumerator = source.GetEnumerator();

        if (!enumerator.MoveNext())
        {
            throw new InvalidOperationException("Sequence contains no elements.");
        }

        var min = selector(enumerator.Current);
        var max = selector(enumerator.Current);
        while (enumerator.MoveNext())
        {
            var value = selector(enumerator.Current);

            if (comparer.Compare(value, min) < 0)
            {
                min = value;
            }

            if (comparer.Compare(value, max) > 0)
            {
                max = value;
            }
        }

        return (min, max);
    }

    public static IEnumerable<T> ToEnumerable<T>(this T item)
    {
        yield return item;
    }
}