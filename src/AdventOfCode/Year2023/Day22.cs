using Range = (int from, int to);

namespace AdventOfCode.Year2023;

[Description("Sand Slabs")]
public class Day22 : IPuzzle
{
    public object Part1(string input)
    {
        return CalculateFallingBlocks(input)
            .Count(x => x == 0);
    }

    public object Part2(string input)
    {
        return CalculateFallingBlocks(input)
            .Sum();
    }

    private static IEnumerable<int> CalculateFallingBlocks(string input)
    {
        Block[] blocks = Fall(ParseBlocks(input));
        Supports supports = GetSupports(blocks);

        foreach (Block disintegratedBlock in blocks)
        {
            Queue<Block> q = new();
            q.Enqueue(disintegratedBlock);

            HashSet<Block> falling = [];
            while (q.TryDequeue(out Block block))
            {
                falling.Add(block);

                IEnumerable<Block> blocksStartFalling = supports.BlocksAbove[block]
                    .Where(blockT => supports.BlocksBelow[blockT].IsSubsetOf(falling));

                foreach (Block blockT in blocksStartFalling)
                {
                    q.Enqueue(blockT);
                }
            }

            yield return falling.Count - 1;
        }
    }

    private static Block[] Fall(Block[] blocks)
    {
        blocks = blocks.OrderBy(block => block.Z.from).ToArray();

        for (int i = 0; i < blocks.Length; i++)
        {
            int newBottom = 1;
            for (int j = 0; j < i; j++)
            {
                Block blockA = blocks[i];
                Block blockB = blocks[j];
                if (blockA.X.Intersects(blockB.X) && blockA.Y.Intersects(blockB.Y))
                {
                    newBottom = Math.Max(newBottom, blocks[j].Z.to + 1);
                }
            }

            int fall = blocks[i].Z.from - newBottom;
            blocks[i] = blocks[i] with { Z = new Range(blocks[i].Z.from - fall, blocks[i].Z.to - fall) };
        }

        return blocks;
    }

    private static Supports GetSupports(IReadOnlyList<Block> blocks)
    {
        Dictionary<Block, HashSet<Block>> blocksAbove = blocks
            .ToDictionary(b => b, _ => new HashSet<Block>());
        Dictionary<Block, HashSet<Block>> blocksBelow = blocks
            .ToDictionary(b => b, _ => new HashSet<Block>());

        for (int i = 0; i < blocks.Count; i++)
        {
            for (int j = i + 1; j < blocks.Count; j++)
            {
                bool zNeighbours = blocks[j].Z.from == 1 + blocks[i].Z.to;
                Block blockA = blocks[i];
                Block blockB = blocks[j];
                if (!zNeighbours || (!blockA.X.Intersects(blockB.X) || !blockB.Y.Intersects(blockA.Y)))
                {
                    continue;
                }

                blocksBelow[blocks[j]].Add(blocks[i]);
                blocksAbove[blocks[i]].Add(blocks[j]);
            }
        }

        return new Supports(blocksAbove, blocksBelow);
    }

    private static Block[] ParseBlocks(string input)
    {
        return input.ToLines()
            .Select(line => line.Split(',', '~')
                .Select(int.Parse).ToArray())
            .Select(numbers => new Block(
                new Range(numbers[0], numbers[3]),
                new Range(numbers[1], numbers[4]),
                new Range(numbers[2], numbers[5])))
            .ToArray();
    }

    private readonly record struct Block(Range X, Range Y, Range Z);

    private readonly record struct Supports(
        Dictionary<Block, HashSet<Block>> BlocksAbove,
        Dictionary<Block, HashSet<Block>> BlocksBelow
    );
}