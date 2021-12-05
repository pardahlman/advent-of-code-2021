using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2021
{
  public class Day04 : IProblem
  {
    public int Day => 4;

    public string SolvePartOne(ICollection<string> puzzleInput)
    {
      var numbersToCall = puzzleInput.First().Split(",").Select(int.Parse);
      var boards = CreateBingoBoards(puzzleInput).ToList();
      foreach (var number in numbersToCall)
      {
        foreach (var board in boards)
        {
          foreach (var row in board)
          {
            var cells = row.Where(r => r.Number == number && !r.Marked).ToList();
            if (cells.Count > 0)
            {
              foreach (var cell in cells)
              {
                cell.Marked = true;
              }

              if (HasBingo(board))
              {
                return CalculateScore(board, number).ToString();
              }
            }
          }
        }
      }

      return "No winner";
    }

    public string SolvePartTwo(ICollection<string> puzzleInput)
    {
      throw new System.NotImplementedException();
    }

    internal static IEnumerable<List<List<BingoCell>>> CreateBingoBoards(IEnumerable<string> puzzleInput)
    {
      var boardInput = puzzleInput.Skip(2);
      var current = new List<List<BingoCell>>();
      foreach (var row in boardInput)
      {
        if (string.IsNullOrEmpty(row))
        {
          yield return current;
          current = new List<List<BingoCell>>();
        }
        else
        {
          current.Add(Regex
            .Matches(row, "\\d+")
            .Select(match => new BingoCell
            {
              Number = int.Parse(match.Value),
              Marked = false
            })
            .ToList());
        }
      }

      if (current.Count > 0)
      {
        yield return current;
      }
    }

    private static bool HasBingo(List<List<BingoCell>> board)
    {
      var candidateRowsForVerticalBingo = Enumerable.Range(0, board[0].Count).ToList();
      foreach (var row in board)
      {
        var horizontalBingo = row.All(cell => cell.Marked);
        if (horizontalBingo)
        {
          return true;
        }

        candidateRowsForVerticalBingo = candidateRowsForVerticalBingo.Where(index => row[index].Marked).ToList();
        if (candidateRowsForVerticalBingo.Count == 0)
        {
          return false;
        }
      }

      return true;
    }

    private static int CalculateScore(IEnumerable<List<BingoCell>> board, int lastCalledNumber)
    {
      var sumOfUnmarkedNumbers = board
        .SelectMany(b => b)
        .Where(b => !b.Marked)
        .Sum(b => b.Number);

      return sumOfUnmarkedNumbers * lastCalledNumber;
    }

    internal class BingoCell
    {
      public int Number { get; set; }
      public bool Marked { get; set; }
    }
  }
}
