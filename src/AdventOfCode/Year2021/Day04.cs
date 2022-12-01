namespace AdventOfCode.Year2021;

[Description("Giant Squid")]
public class Day04 : IPuzzle
{
    public object Part1(string input)
    {
        IEnumerable<string> data = input.ToLines();
        var (balls, boards) = ParseData(data);

        int ball = 0;
        Board? winningBoard = null;

        while (balls.Count > 0 && (winningBoard = FindWinningBoard(boards)) == null)
        {
            ball = balls.Dequeue();

            MarkNumber(boards, ball);
        }

        if (winningBoard == null)
            return 0;

        var sum = winningBoard.Cells.Where(cell => cell.Marked == false).Sum(cell => cell.Number);

        return sum * ball;
    }

    public object Part2(string input)
    {
        IEnumerable<string> data = input.ToLines();
        var (balls, boards) = ParseData(data);
        var (lastBall, winningBoard) = Play(balls, boards);

        var sum = winningBoard.Cells.Where(cell => cell.Marked == false).Sum(cell => cell.Number);

        return sum * lastBall;
    }

    private bool IsWinningBoard(Board board)
    {
        var rows = Enumerable.Range(0, 5).Select(i => Enumerable.Range(i * 5, 5));
        var cols = Enumerable.Range(0, 5).Select(i => Enumerable.Range(0, 5).Select(j => j * 5 + i ));
        var all = rows.Concat(cols);

        return all.Any(check => check.Select(n => board.Cells[n]).All(c => c.Marked));
    }

    private Board? FindWinningBoard(Board[] boards)
    {
        return boards.FirstOrDefault(IsWinningBoard);
    }

    private void MarkNumber(Board[] boards, int number)
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

    private (int lastBall, Board? winningBoard) Play(Queue<int> balls, Board[] boards)
    {
        int lastBall = 0;
        Board? winningBoard = null;

        while (balls.Count > 0 && (winningBoard = FindWinningBoard(boards)) == null)
        {
            lastBall = balls.Dequeue();

            MarkNumber(boards, lastBall);
        }

        return (lastBall, winningBoard);
    }

    private static (Queue<int> balls, Board[] boards) ParseData(IEnumerable<string> data)
    {
        var lines = data.ToArray();

        var balls = new Queue<int>(lines.First().Split(',').Select(s => Convert.ToInt32(s)));

        var boards = lines.Skip(1).Where(s => !string.IsNullOrEmpty(s))
            .Chunk(5)
            .Select(b => b.Aggregate("", (s, s1) => $"{s} {s1}"))
            .Select(s =>
                s.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => new Cell(Convert.ToInt32(s), false)).ToArray())
            .Select(c => new Board(c, false))
            .ToArray();

        return (balls, boards);
    }

    public record Cell(int Number, bool Marked);

    public record Board(Cell[] Cells, bool Marked);
}