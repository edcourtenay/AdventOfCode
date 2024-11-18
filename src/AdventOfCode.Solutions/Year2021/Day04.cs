namespace AdventOfCode.Solutions.Year2021;

[Description("Giant Squid")]
public class Day04 : IPuzzle
{
    public object Part1(string input)
    {
        return Solve(input, Enumerable.First);
    }

    public object Part2(string input)
    {
        return Solve(input, Enumerable.Last);
    }

    private int Solve(string input, Func<IEnumerable<(int ball, Board winningBoard)>, (int ball, Board? winningBoard)> findWinningBoard)
    {
        IEnumerable<string> data = input.ToLines();
        var (balls, boards) = ParseData(data);

        (int ball, Board? winningBoard) = findWinningBoard(WinningBoards(balls, boards));

        if (winningBoard == null)
            return 0;

        var sum = winningBoard.Cells.Where(cell => cell.Marked == false).Sum(cell => cell.Number);

        return sum * ball;
    }

    private IEnumerable<(int ball, Board winningBoard)> WinningBoards(Queue<int> balls, HashSet<Board> boards)
    {
        int ball = 0;
        while (true)
        {
            Board? winningBoard = null;

            while (balls.Count > 0 && (winningBoard = FindWinningBoard(boards)) == null)
            {
                ball = balls.Dequeue();

                MarkNumber(boards, ball);
            }

            if (winningBoard == null)
            {
                yield break;
            }

            yield return (ball, winningBoard);
            boards.Remove(winningBoard);
        }
    }

    private static bool IsWinningBoard(Board board)
    {
        var rows = Enumerable.Range(0, 5).Select(i => Enumerable.Range(i * 5, 5));
        var cols = Enumerable.Range(0, 5).Select(i => Enumerable.Range(0, 5).Select(j => j * 5 + i ));
        var all = rows.Concat(cols);

        return all.Any(check => check.Select(n => board.Cells[n]).All(c => c.Marked));
    }

    private static Board? FindWinningBoard(IEnumerable<Board> boards)
    {
        return boards.FirstOrDefault(IsWinningBoard);
    }

    private static void MarkNumber(HashSet<Board> boards, int number)
    {
        foreach (var board in boards)
        {
            for (int cell = 0; cell < board.Cells.Length; cell++)
            {
                if (board.Cells[cell].Number == number)
                {
                    board.Cells[cell] = board.Cells[cell] with { Marked = true };
                }
            }
        }
    }

    private static (Queue<int> balls, HashSet<Board> boards) ParseData(IEnumerable<string> data)
    {
        var lines = data.ToArray();

        var balls = new Queue<int>(lines.First().Split(',').Select(s => Convert.ToInt32(s)));

        var boards = lines.Skip(1).Where(s => !string.IsNullOrEmpty(s))
            .Chunk(5)
            .Select<IEnumerable<string>, string>(b => b.Aggregate("", (s, s1) => $"{s} {s1}"))
            .Select(s =>
                s.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => new Cell(Convert.ToInt32(s), false)).ToArray())
            .Select(c => new Board(c, false))
            .ToHashSet();

        return (balls, boards);
    }

    public record Cell(int Number, bool Marked);

    public record Board(Cell[] Cells, bool Marked);
}