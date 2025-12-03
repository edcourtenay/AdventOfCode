namespace AdventOfCode.Solutions.Extensions;

public static class CombinatorialExtensions
{
    extension<T>(IEnumerable<T> source)
    {
        public IEnumerable<IEnumerable<T>> Combinations(int width)
        {
            var arr = source as T[] ?? source.ToArray();

            return GenerateCombinations(arr.Length, width)
                .Select(indexes => indexes.Select(i => arr[i - 1]));
        }
    }

    private static IEnumerable<int[]> GenerateCombinations(int n, int k)
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

