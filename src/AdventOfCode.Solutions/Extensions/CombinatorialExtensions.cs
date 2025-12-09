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
            
            static IEnumerable<int[]> GenerateCombinations(int n, int k)
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

                        if (index != k)
                        {
                            continue;
                        }

                        yield return result.ToArray();
                        break;
                    }
                }
            }
        }
        
        public IEnumerable<T[]> Permutations()
        {
            var array = source as T[] ?? source.ToArray();
            return GeneratePermutations(array, 0);
            
            static IEnumerable<T[]> GeneratePermutations(T[] array, int i)
            {
                if (i == array.Length)
                {
                    yield return array.ToArray();
                    yield break;
                }

                for (var j = i; j < array.Length; j++)
                {
                    (array[i], array[j]) = (array[j], array[i]);

                    foreach (var perm in GeneratePermutations(array, i + 1))
                        yield return perm;

                    (array[i], array[j]) = (array[j], array[i]);
                }
            }
        }
    }
}

