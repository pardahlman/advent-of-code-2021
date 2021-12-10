using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021
{
  public class Day10 : IProblem
  {
    public int Day => 10;

    public string SolvePartOne(ICollection<string> puzzleInput) =>
      puzzleInput.Sum(line => GetScore(line, ScoreType.SyntaxError)).ToString();

    public string SolvePartTwo(ICollection<string> puzzleInput)
    {
      var scores = puzzleInput
        .Select(line => GetScore(line, ScoreType.IncompleteLine))
        .Where(score => score > 0)
        .OrderBy(score => score)
        .ToList();

      return scores[scores.Count / 2].ToString();
    }

    private static long GetScore(string line, ScoreType scoreType)
    {
      var bracketStack = new Stack<char>();
      foreach (var bracket in line)
      {
        switch ((bracketStack.FirstOrDefault(), bracket))
        {
          case ('(', ')'):
          case ('[', ']'):
          case ('{', '}'):
          case ('<', '>'):
            bracketStack.Pop();
            break;
          case (_, ')'):
            return scoreType == ScoreType.SyntaxError ? 3 : 0;
          case (_, ']'):
            return scoreType == ScoreType.SyntaxError ? 57 : 0;
          case (_, '}'):
            return scoreType == ScoreType.SyntaxError ? 1197 : 0;
          case (_, '>'):
            return scoreType == ScoreType.SyntaxError ? 25137 : 0;
          default:
            bracketStack.Push(bracket);
            break;
        }
      }

      if (scoreType == ScoreType.SyntaxError)
      {
        return 0;
      }

      return bracketStack
        .Select(b => b switch
        {
          '(' => 1,
          '[' => 2,
          '{' => 3,
          '<' => 4,
          _ => throw new Exception($"Unexpected bracket {b}")
        })
        .Aggregate(0L, (total, score) => total * 5 + score );
    }

    private enum ScoreType
    {
      SyntaxError,
      IncompleteLine
    }
  }
}
