using System.Text.RegularExpressions;

namespace AdventOfCode.Year2015;

[Description("Let It Snow")]
public class Day25 : IPuzzle
{
    public object Part1(string input)
    {
        return PartOne(input);
    }

    public object Part2(string input)
    {
        return string.Empty;
    }

    public object PartOne(string input) {
        var m = 20151125L;
        (int iRow, int iCol) = (1, 1);
        (int iRowDest, int iColDest) = Parse(input);
        while (iRow != iRowDest || iCol != iColDest) {
            iRow--;
            iCol++;
            if (iRow == 0) {
                iRow = iCol;
                iCol = 1;
            }
            m = (m * 252533L) % 33554393L;
        }
        return m;
    }

    (int irowDst, int icolDst) Parse(string input){
        var m = Regex.Match(input, @"To continue, please consult the code grid in the manual.  Enter the code at row (\d+), column (\d+).");
        return (int.Parse(m.Groups[1].Value), int.Parse(m.Groups[2].Value));
    }
}