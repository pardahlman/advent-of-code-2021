using System.Collections.Generic;
using System.Diagnostics;
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
          if (!TryMarkNumber(board, number))
          {
            continue;
          }

          if (HasBingo(board))
          {
            return CalculateScore(board, number).ToString();
          }
        }
      }

      return "No winner";
    }

    private static bool TryMarkNumber(IEnumerable<List<BingoCell>> board, int number)
    {
      var cells = board
        .SelectMany(row => row)
        .Where(c => c.Number == number && !c.Marked)
        .ToList();

      foreach (var cell in cells)
      {
        cell.Marked = true;
      }

      return cells.Count > 0;
    }

    public string SolvePartTwo(ICollection<string> puzzleInput)
    {
      var numbersToCall = puzzleInput.First().Split(",").Select(int.Parse);
      var boardWithoutBingo = CreateBingoBoards(puzzleInput).ToList().ToList();
      foreach (var number in numbersToCall)
      {
        foreach (var board in boardWithoutBingo.ToList())
        {
          if (!TryMarkNumber(board, number))
          {
            continue;
          }

          if (HasBingo(board))
          {
            if (boardWithoutBingo.Count == 1)
            {
              return CalculateScore(boardWithoutBingo.Single(), number).ToString();
            }
            boardWithoutBingo.Remove(board);
          }
        }
      }

      return "No winner";
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
      }

      return candidateRowsForVerticalBingo.Count > 0;
    }

    private static int CalculateScore(IEnumerable<List<BingoCell>> board, int lastCalledNumber)
    {
      var sumOfUnmarkedNumbers = board
        .SelectMany(b => b)
        .Where(b => !b.Marked)
        .Sum(b => b.Number);

      return sumOfUnmarkedNumbers * lastCalledNumber;
    }

    [DebuggerDisplay("{Number} ({Marked})")]
    internal class BingoCell
    {
      public int Number { get; init; }
      public bool Marked { get; set; }
    }
  }
}
