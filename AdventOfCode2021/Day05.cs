using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2021
{
  public class Day05 : IProblem
  {
    public int Day => 5;

    public string SolvePartOne(ICollection<string> puzzleInput)
    {
      var lines = CreateLines(puzzleInput);
      var grid = new List<List<int>>();
      foreach (var line in lines)
      {
        if (!(IsVerticalLine(line) || IsHorizontalLine(line)))
        {
          continue;
        }

        var xMin = Math.Min(line.Start.X, line.Stop.X);
        var xMax = Math.Max(line.Start.X, line.Stop.X);
        var yMin = Math.Min(line.Start.Y, line.Stop.Y);
        var yMax = Math.Max(line.Start.Y, line.Stop.Y);

        while (grid.Count <= yMax)
        {
          grid.Add(new List<int>());
        }

        for (var y = yMin; y <= yMax; y++)
        {
          var row = grid[y];
          while (row.Count <= xMax)
          {
            row.Add(0);
          }

          for (var x = xMin; x <= xMax; x++)
          {
            row[x] += 1;
          }
        }
      }

      return grid.SelectMany(g => g).Count(i => i > 1).ToString();
    }

    private static bool IsVerticalLine(Line line) => line.Start.X == line.Stop.X;
    private static bool IsHorizontalLine(Line line) => line.Start.Y == line.Stop.Y;

    public string SolvePartTwo(ICollection<string> puzzleInput)
    {
      throw new System.NotImplementedException();
    }

    private static IList<Line> CreateLines(ICollection<string> puzzleInput) =>
      puzzleInput
        .Select(i =>
        {
          var matches = Regex.Matches(i, "^(\\d+),(\\d+)\\s+->\\s(\\d+),(\\d+)$");
          if (matches.Count != 1 || matches[0].Groups.Count != 5)
          {
            throw new Exception($"Expected 1 group with 5 matches, got {matches.Count}");
          }

          return new Line
          {
            Start = new Point
            {
              X = int.Parse(matches[0].Groups[1].Value),
              Y = int.Parse(matches[0].Groups[2].Value)
            },
            Stop = new Point
            {
              X = int.Parse(matches[0].Groups[3].Value),
              Y = int.Parse(matches[0].Groups[4].Value)
            }
          };
        })
        .ToList();

    [DebuggerDisplay("{Start} -> {Stop})")]
    private record Line
    {
      public Point Start { get; init; }
      public Point Stop { get; init; }
    }

    [DebuggerDisplay(("({X}, {Y})"))]
    private record Point
    {
      public int X { get; init; }
      public int Y { get; init; }
    }
  }
}
