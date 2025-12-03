namespace AdventOfCode.Solutions.Extensions;

public static class WindowingExtensions
{
    extension<T>(IEnumerable<T> source)
    {
        public IEnumerable<IEnumerable<T>> Window(int size, bool allowPartialWindow = false)
        {
            using var enumerator = source.GetEnumerator();
            var queue = new Queue<T>();

            while (enumerator.MoveNext())
            {
                queue.Enqueue(enumerator.Current);

                if (queue.Count == size)
                {
                    yield return queue;
                    queue = new Queue<T>();
                }
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

        public IEnumerable<(T First, T Second)> Pairwise()
        {
            using var enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
                yield break;

            var previous = enumerator.Current;
            while (enumerator.MoveNext())
            {
                yield return (previous, enumerator.Current)!;
                previous = enumerator.Current;
            }
        }
    }
}

