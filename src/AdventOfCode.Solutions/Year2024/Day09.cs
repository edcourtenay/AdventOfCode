namespace AdventOfCode.Solutions.Year2024;

[Description("Disk Fragmenter")]
public class Day09 : IPuzzle
{
    private const int EmptyFileId = -1;

    public object Part1(string input)
    {
        return Checksum(OptimizeFileSystem(Parse(input), true));
    }

    public object Part2(string input)
    {
        return Checksum(OptimizeFileSystem(Parse(input), false));
    }

    private FileSystem OptimizeFileSystem(FileSystem fileSystem, bool fragmentsEnabled)
    {
        (LinkedListNode<Block>? leftNode, LinkedListNode<Block>? rightNode) = (fileSystem.First, fileSystem.Last);

        while (leftNode != null && rightNode != null && leftNode != rightNode)
        {
            if (IsOccupiedBlock(leftNode))
            {
                leftNode = leftNode.Next;
            }
            else if (IsEmptyBlock(rightNode))
            {
                rightNode = rightNode.Previous;
            }
            else if (CanRelocateBlocks(fileSystem, leftNode, rightNode, fragmentsEnabled))
            {
                rightNode = rightNode.Previous;
            }
        }

        return fileSystem;

        bool CanRelocateBlocks(FileSystem fs, LinkedListNode<Block> left, LinkedListNode<Block> right, bool fragments)
        {
            RelocateBlock(fs, left, right, fragments);
            return true;
        }

        bool IsOccupiedBlock(LinkedListNode<Block> node)
        {
            return node.Value.FileId != EmptyFileId;
        }

        bool IsEmptyBlock(LinkedListNode<Block> node)
        {
            return node.Value.FileId == EmptyFileId;
        }
    }

    private void RelocateBlock(FileSystem fileSystem, LinkedListNode<Block> start, LinkedListNode<Block> target,
        bool fragmentsEnabled)
    {
        for (LinkedListNode<Block>? current = start; current != target; current = current.Next)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            if (current.Value.FileId != EmptyFileId)
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            {
                continue;
            }

            if (current.Value.Length == target.Value.Length)
            {
                SwapBlocks(current, target);
                return;
            }

            int lengthDifference = Math.Abs(current.Value.Length - target.Value.Length);

            if (current.Value.Length > target.Value.Length)
            {
                SplitBlockAndRelocate(fileSystem, current, target, lengthDifference);
                return;
            }

            if (current.Value.Length < target.Value.Length && fragmentsEnabled)
            {
                FragmentBlockAndRelocate(fileSystem, current, target, lengthDifference);
            }
        }
    }

    private void SwapBlocks(LinkedListNode<Block> a, LinkedListNode<Block> b)
    {
        (a.Value, b.Value) = (b.Value, a.Value);
    }

    private void SplitBlockAndRelocate(FileSystem fileSystem, LinkedListNode<Block> source,
        LinkedListNode<Block> target, int lengthDifference)
    {
        source.Value = target.Value;
        target.Value = target.Value with { FileId = EmptyFileId };
        fileSystem.AddAfter(source, new Block(EmptyFileId, lengthDifference));
    }

    private void FragmentBlockAndRelocate(FileSystem fileSystem, LinkedListNode<Block> source,
        LinkedListNode<Block> target, int lengthDifference)
    {
        source.Value = source.Value with { FileId = target.Value.FileId };
        target.Value = target.Value with { Length = lengthDifference };
        fileSystem.AddAfter(target, source.Value with { FileId = EmptyFileId });
    }

    private long Checksum(FileSystem fileSystem)
    {
        long res = 0L;
        int l = 0;

        foreach (Block block in fileSystem)
        {
            for (int k = 0; k < block.Length; k++)
            {
                if (block.FileId != -1)
                {
                    res += l * block.FileId;
                }

                l++;
            }
        }

        return res;
    }

    private FileSystem Parse(string input)
    {
        return new FileSystem(input.Select((ch, i) => new Block(i % 2 == 1 ? -1 : i / 2, ch - '0')));
    }

    private readonly record struct Block(int FileId, int Length);

    private class FileSystem : LinkedList<Block>
    {
        public FileSystem(IEnumerable<Block> blocks) : base(blocks)
        {
        }
    }
}