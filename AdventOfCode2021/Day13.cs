using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2021
{
  public class Day13 : IProblem
  {
    public int Day => 13;

    public string SolvePartOne(ICollection<string> puzzleInput)
    {
      var (dots, foldInstructions) = ParsePuzzleInput(puzzleInput);
      return Fold(dots, foldInstructions).First().Count.ToString();
    }

    public string SolvePartTwo(ICollection<string> puzzleInput)
    {
      var (dots, foldInstructions) = ParsePuzzleInput(puzzleInput);
      var foldedPaper = Fold(dots, foldInstructions).Last();
      var letters = GroupToLetters(foldedPaper);
      var ocr = CreateOcrDictionary();

      var serialNumber = letters
        .Select(letterDots =>
        {
          var letter = ocr.FirstOrDefault(k => k.Key.SetEquals(letterDots)).Value;
          if (letter == 0)
          {
            throw new Exception(CreateErrorMessageForMissingLetter(letterDots));
          }

          return letter;
        })
        .ToArray();


      return new string(serialNumber);
    }

    private static List<List<Position>> GroupToLetters(IEnumerable<Position> positions)
    {
      positions = positions
        .OrderBy(dot => dot.X)
        .ThenBy(dot => dot.Y);

      var letters = new List<List<Position>>();
      Position previous = default;
      var offset = 0;
      foreach (var dot in positions)
      {
        if (previous == default || dot.X - previous.X > 1)
        {
          offset = dot.X;
          letters.Add(new List<Position>());
        }

        letters[^1].Add(new Position(dot.X - offset, dot.Y));

        previous = dot;
      }

      return letters;
    }

    private static IEnumerable<List<Position>> Fold(List<Position> dots, IEnumerable<FoldInstruction> foldInstructions)
    {
      foreach (var foldInstruction in foldInstructions)
      {
        var (orientation, value) = foldInstruction;
        Func<Position, Position> foldTransformation = orientation == 'x'
          ? dot => new Position(CalculateValueAfterFold(dot.X, value), dot.Y)
          : dot => new Position(dot.X, CalculateValueAfterFold(dot.Y, value));

        dots = dots
          .Select(foldTransformation)
          .Distinct()
          .ToList();

        yield return dots;
      }
    }

    private static int CalculateValueAfterFold(int xOrY, int foldValue)
    {
      if (xOrY < foldValue)
      {
        return xOrY;
      }

      var distanceFromFold = xOrY - foldValue;
      return foldValue - distanceFromFold;
    }

    private static (List<Position>, List<FoldInstruction>) ParsePuzzleInput(ICollection<string> puzzleInput)
    {
      var dots = puzzleInput
        .TakeWhile(row => row != string.Empty)
        .Select(dot =>
        {
          var parts = dot.Split(",");
          return new Position(int.Parse(parts[0]), int.Parse(parts[1]));
        })
        .ToList();

      var foldInstructions = puzzleInput
        .Skip(dots.Count + 1)
        .Select(foldInstruction =>
        {
          var match = Regex.Match(foldInstruction, "fold along ([x|y])=(\\d+)");
          return new FoldInstruction(match.Groups[1].Value[0], int.Parse(match.Groups[2].Value));
        })
        .ToList();

      return (dots, foldInstructions);
    }

    private static IDictionary<HashSet<Position>, char> CreateOcrDictionary()
    {
      return new Dictionary<HashSet<Position>, char>
      {
        {
          new HashSet<Position>
          {
            new(0, 1), new(0, 2), new(0, 3), new(0, 4),
            new(1, 0), new(1, 5), new(2, 0), new(2, 5),
            new(3, 1), new(3, 4),
          },
          'C'
        },
        {
          new HashSet<Position>
          {
            new (0, 0), new  (0, 1), new  (0, 2), new  (0, 3),
            new  (0, 4), new  (0, 5), new  (1, 0), new  (1, 3),
            new  (2, 0), new  (2, 3), new  (3, 1), new  (3, 2)
          },
          'P'
        },
        {
          new HashSet<Position>
          {
            new (0, 0), new  (0, 4), new  (0, 5), new  (1, 0),
            new  (1, 3), new  (1, 5), new  (2, 0), new  (2, 2),
            new  (2, 5), new  (3, 0), new  (3, 1), new  (3, 5),
          },
          'Z'
        },

        {
          new HashSet<Position>
          {
            new (0, 0), new  (0, 1), new  (0, 2),
            new  (0, 3), new  (0, 4), new  (0, 5),
            new  (1, 5), new  (2, 5), new  (3, 5),
          },
          'L'
        },

        {
          new HashSet<Position>
          {
            new (0, 0), new  (0, 1), new  (0, 2), new  (0, 3),
            new  (0, 4), new  (0, 5), new  (1, 0), new  (1, 2),
            new  (2, 0), new  (2, 2), new  (3, 0),
          },
          'F'
        },
      };
    }

    private static string CreateErrorMessageForMissingLetter(List<Position> unknownLetter)
    {
      var stringBuilder = new StringBuilder();
      stringBuilder.Append("Letter not found in the OCR dictionary. It's positions is:");
      stringBuilder.Append(Environment.NewLine);
      stringBuilder.Append(Environment.NewLine);
      stringBuilder.Append(unknownLetter.Aggregate(string.Empty, (s, position) => s + $"({position.X}, {position.Y}), "));
      stringBuilder.Append(Environment.NewLine);
      stringBuilder.Append("Rendered, it looks like this:");
      stringBuilder.Append(Environment.NewLine);
      stringBuilder.Append(Environment.NewLine);

      var xMax = unknownLetter.Max(p => p.X);
      var yMax = unknownLetter.Max(p => p.Y);

      for (var y = 0; y <= yMax; y++)
      {
        for (var x = 0; x <= xMax; x++)
        {
          var characterToWrite = (unknownLetter.Contains(new Position(x, y))) ? '█' : ' ';
          stringBuilder.Append(characterToWrite);
        }
        stringBuilder.Append(Environment.NewLine);
      }
      stringBuilder.Append(Environment.NewLine);
      stringBuilder.Append("Consider adding this letter to the dictionary");
      return stringBuilder.ToString();
    }

    private record Position(int X, int Y);

    private record FoldInstruction(char Orientation, int Value);
  }
}
