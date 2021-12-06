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

    public string SolvePartOne(ICollection<string> puzzleInput) =>
      SolveProblem(puzzleInput, IsVerticalLine, IsHorizontalLine);

    public string SolvePartTwo(ICollection<string> puzzleInput) =>
      SolveProblem(puzzleInput, IsVerticalLine, IsHorizontalLine, Is45DegreeLine);

    private static bool IsVerticalLine(Line line) => line.Start.X == line.Stop.X;
    private static bool IsHorizontalLine(Line line) => line.Start.Y == line.Stop.Y;
    private static bool Is45DegreeLine(Line line)
    {
      var xComponent = line.Stop.X - line.Start.X;
      var yComponent = line.Stop.Y - line.Start.Y;
      var gradient = (double)yComponent / xComponent;
      return Math.Abs(gradient) == 1;
    }

    private static string SolveProblem(ICollection<string> puzzleInput, params Predicate<Line>[] linePredicates)
    {
      var lines = CreateLines(puzzleInput);
      var grid = new List<List<int>>();
      foreach (var line in lines)
      {
        if (!linePredicates.Any(p => p(line)))
        {
          continue;
        }

        var xComponent = line.Stop.X - line.Start.X;
        var yComponent = line.Stop.Y - line.Start.Y;
        var gradient = (double)yComponent / xComponent;

        int xDelta, yDelta;
        if (double.IsPositiveInfinity(gradient) || double.IsNegativeInfinity(gradient))
        {
          xDelta = 0;
          yDelta = yComponent > 0 ? 1 : -1;
        }
        else
        {
          xDelta = xComponent > 0 ? 1 : -1;
          yDelta = (int)gradient * xDelta;
        }

        var points = new List<Point> { line.Start };
        var previousPoint = line.Start;
        while (previousPoint != line.Stop)
        {
          var nextPoint = new Point
          {
            X = previousPoint.X + xDelta,
            Y = previousPoint.Y + yDelta
          };
          points.Add(nextPoint);
          previousPoint = nextPoint;
        }

        foreach (var point in points)
        {
          while (grid.Count <= point.Y)
          {
            grid.Add(new List<int>());
          }

          var row = grid[point.Y];
          while (row.Count <= point.X)
          {
            row.Add(0);
          }

          row[point.X] += 1;
        }
      }

      return grid.SelectMany(g => g).Count(i => i > 1).ToString();
    }

    private static IEnumerable<Line> CreateLines(ICollection<string> puzzleInput) =>
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
