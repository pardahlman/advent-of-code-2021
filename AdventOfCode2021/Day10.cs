using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021
{
  public class Day10 : IProblem
  {
    public int Day => 10;

    private static readonly Dictionary<char, char> Brackets = new()
    {
      { '(', ')' },
      { '[', ']' },
      { '{', '}' },
      { '<', '>' },
    };

    private readonly Stack<char> _bracketStack = new();

    public string SolvePartOne(ICollection<string> puzzleInput)
    {
      var characters = new List<char>();
      foreach (var line in puzzleInput)
      {
        if (HasSyntaxError(line, out var firstIllegalCharacter))
        {
          characters.Add(firstIllegalCharacter);
        }
      }

      return characters.Sum(c =>
      {
        return c switch
        {
          ')' => 3,
          ']' => 57,
          '}' => 1197,
          '>' => 25137,
          _ => throw new Exception($"Unexpected character '{c}'")
        };
      }).ToString();
    }

    private bool HasSyntaxError(string line, out char firstIllegalCharacter)
    {
      firstIllegalCharacter = default;

      foreach (var character in line)
      {
        if (Brackets.ContainsKey(character))
        {
          _bracketStack.Push(character);
        }
        else
        {
          var openingBracket = _bracketStack.Pop();
          if (!Brackets.ContainsKey(openingBracket))
          {
            // line is incomplete, ignore for now
            return false;
          }

          var expectedClosingBracket = Brackets[openingBracket];
          if (character != expectedClosingBracket)
          {
            firstIllegalCharacter = character;
            return true;
          }
        }
      }

      return false;
    }

    public string SolvePartTwo(ICollection<string> puzzleInput)
    {
      throw new NotImplementedException();
    }
  }
}
