namespace AdventOfCode.Solutions;

public static class EnumerableExtensions
{
    extension<T>(T[] source)
    {
        public IEnumerable<T[]> Permutations() {
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
    }

    extension<T>(IEnumerable<T> source)
    {
        public IEnumerable<IEnumerable<T>> Chunk(int chunkSize)
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

        public IEnumerable<IEnumerable<T>> ChunkBy(Func<T, bool> predicate, bool dropChunkSeparator = true)
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

        public IEnumerable<IEnumerable<T>> Window(int size, bool allowPartialWindow = false)
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

        public IEnumerable<IEnumerable<T>> SlidingWindow(int size)
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
    }

    extension(string input)
    {
        public IEnumerable<string> ToLines()
        {
            using var reader = new StringReader(input);
            while (reader.ReadLine() is { } line)
            {
                yield return line;
            }
        }

        public IEnumerable<T> ToLines<T>(Func<string, T> selector)
        {
            return input.ToLines().Select(selector);
        }

        public IEnumerable<TResult> ToLines<TResult>(Func<string, int, TResult> selector)
        {
            int index = -1;
            foreach (string line in input.ToLines())
            {
                index++;
                yield return selector(line, index);
            }
        }
    }

    extension<T>(IEnumerable<IEnumerable<T>> source)
    {
        public IEnumerable<IEnumerable<T>> Pivot()
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
    }

    extension<T>(IEnumerable<T> source)
    {
        public IEnumerable<(T First, T Second)> Pairwise()
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

    extension(IEnumerable<string> source)
    {
        public IEnumerable<IEnumerable<string>> ToSequences(Func<string, bool> predicate)
        {
            using IEnumerator<string> enumerator = source.GetEnumerator();
            do
            {
                if (!enumerator.MoveNext())
                    yield break;

                yield return GroupSequence(enumerator, predicate);
            } while (true);

            IEnumerable<string> GroupSequence(IEnumerator<string> groupEnumerator, Func<string, bool> func)
            {
                do
                {
                    yield return groupEnumerator.Current;
                } while (groupEnumerator.MoveNext() && !func(groupEnumerator.Current));
            }
        }
    }

    extension<T>(IEnumerable<T> source)
    {
        public IEnumerable<IEnumerable<T>> Combinations(int width)
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

        public IEnumerable<T> Flatten(Func<T, IEnumerable<T>> f)
        {
            IEnumerable<T> enumerable = source as T[] ?? source.ToArray();
            return enumerable.SelectMany(c => f(c).Flatten(f)).Concat(enumerable);
        }

        public IEnumerable<T> TakeUntil(Func<T, bool> predicate)
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
    }

    extension<T>(IEnumerable<T> source) where T : IComparable<T>
    {
        public (T Min, T Max) MinMax()
        {
            return MinMax(source, Comparer<T>.Default);
        }
    }

    extension<T>(IEnumerable<T> source)
    {
        public (T Min, T Max) MinMax(IComparer<T> comparer)
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

        public (TValue Min, TValue Max) MinMaxBy<TValue>(Func<T, TValue> selector) where TValue : IComparable<TValue>
        {
            return MinMaxBy(source, selector, Comparer<TValue>.Default);
        }
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