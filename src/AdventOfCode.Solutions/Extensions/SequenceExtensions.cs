using System.Runtime.CompilerServices;

namespace AdventOfCode.Solutions.Extensions;

public static class SequenceExtensions
{
    extension<T>(IEnumerable<T> source)
    {
        // Post-order flatten of a forest (children first, then node) without recursion or repeated materialization
        public IEnumerable<T> Flatten(Func<T, IEnumerable<T>> children)
        {
            if (children is null) throw new ArgumentNullException(nameof(children));

            // Each frame holds an enumerator to advance and, optionally, a node to yield
            // after that enumerator completes (post-order semantics)
            var frames = new Stack<(IEnumerator<T> e, T node, bool returnNode)>();
            var enumerator = source.GetEnumerator();
            frames.Push((enumerator, default!, false));

            try
            {
                while (frames.Count > 0)
                {
                    var frame = frames.Peek();
                    if (frame.e.MoveNext())
                    {
                        var node = frame.e.Current;
                        var childEnumerable = children(node);

                        if (childEnumerable is null)
                        {
                            // No children; yield node immediately
                            yield return node;
                            continue;
                        }

                        var childEnumerator = childEnumerable.GetEnumerator();
                        frames.Push((childEnumerator, node, true));
                    }
                    else
                    {
                        frames.Pop();
                        frame.e.Dispose();
                        if (frame.returnNode)
                            yield return frame.node;
                    }
                }
            }
            finally
            {
                // Ensure we dispose any remaining enumerators if enumeration stops early
                while (frames.Count > 0)
                {
                    var f = frames.Pop();
                    f.e.Dispose();
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> TakeUntil(Func<T, bool> predicate)
        {
            if (predicate is null) throw new ArgumentNullException(nameof(predicate));
            foreach (var item in source)
            {
                yield return item;
                if (predicate(item))
                    yield break;
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
                if (enumerators.Length == 0)
                    yield break; // avoid infinite loop on empty input

                while (true)
                {
                    for (int i = 0; i < enumerators.Length; i++)
                    {
                        if (!enumerators[i].MoveNext())
                            yield break;
                    }

                    var row = new T[enumerators.Length];
                    for (int i = 0; i < enumerators.Length; i++)
                        row[i] = enumerators[i].Current;

                    yield return row;
                }
            }
            finally
            {
                for (int i = 0; i < enumerators.Length; i++)
                    enumerators[i].Dispose();
            }
        }
    }

    public static IEnumerable<T> ToEnumerable<T>(this T item)
    {
        yield return item;
    }
}

