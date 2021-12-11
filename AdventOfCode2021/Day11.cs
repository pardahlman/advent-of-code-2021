using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021
{
  public class Day11 : IProblem
  {
    public int Day => 11;

    public string SolvePartOne(ICollection<string> puzzleInput)
    {
      var grid = CreateOctopusGrid(puzzleInput);

      var flashCount = 0;
      for (var day = 0; day < 100; day++)
      {
        var hasFlashed = IncreaseEnergyLevel(grid).ToList();
        PropagateFlashes(hasFlashed, grid);
        flashCount += CalculateAndResetFlashedOctopuses(grid);
      }

      return flashCount.ToString();
    }

    public string SolvePartTwo(ICollection<string> puzzleInput)
    {
      var grid = CreateOctopusGrid(puzzleInput);
      var octopusCount = grid.Sum(row => row.Count);

      var day = 0;
      var flashCount = 0;
      while (flashCount != octopusCount)
      {
        day += 1;
        var hasFlashed = IncreaseEnergyLevel(grid).ToList();
        PropagateFlashes(hasFlashed, grid);
        flashCount = CalculateAndResetFlashedOctopuses(grid);
      }

      return day.ToString();
    }

    private static int CalculateAndResetFlashedOctopuses(List<List<Octopus>> grid)
    {
      var flashCount = 0;
      foreach (var octopus in grid.SelectMany(r => r))
      {
        if (octopus.EnergyLevel > 9)
        {
          octopus.EnergyLevel = 0;
          flashCount += 1;
        }
      }

      return flashCount;
    }

    private static IEnumerable<Octopus> IncreaseEnergyLevel(List<List<Octopus>> grid)
    {
      foreach (var octopus in grid.SelectMany(row => row))
      {
        octopus.EnergyLevel += 1;
        if (octopus.EnergyLevel > 9)
        {
          yield return octopus;
        }
      }
    }

    private static void PropagateFlashes(List<Octopus> newlyFlashed, List<List<Octopus>> grid)
    {
      if (newlyFlashed.Count == 0)
      {
        return;
      }

      var nextIteration = new List<Octopus>();
      foreach (var octopus in newlyFlashed)
      {
        foreach (var neighbour in GetNeighbours(octopus.Position, grid))
        {
          neighbour.EnergyLevel += 1;
          if (neighbour.EnergyLevel == 10)
          {
            nextIteration.Add(neighbour);
          }
        }
      }

      PropagateFlashes(nextIteration, grid);
    }

    private static List<List<Octopus>> CreateOctopusGrid(IEnumerable<string> puzzleInput)
    {
      return puzzleInput
        .Select((row, y) => row
          .Select((energyLevel, x) => new Octopus
          {
            Position = new Position(x, y),
            EnergyLevel = int.Parse(energyLevel.ToString())
          })
          .ToList())
        .ToList();
    }

    private static IEnumerable<Octopus> GetNeighbours(Position position, List<List<Octopus>> octopusGrid)
    {
      var hasRowAbove = position.Y != 0;
      var hasRowBelow = position.Y != octopusGrid.Count - 1;
      var hasColumnToTheLeft = position.X != 0;
      var hasColumnToTheRight = position.X != octopusGrid[position.X].Count - 1;

      if (hasRowAbove)
      {
        var rowAbove = octopusGrid[position.Y - 1];
        yield return rowAbove[position.X];
        if (hasColumnToTheLeft)
        {
          yield return rowAbove[position.X - 1];
        }

        if (hasColumnToTheRight)
        {
          yield return rowAbove[position.X + 1];
        }
      }

      if (hasColumnToTheLeft)
      {
        yield return octopusGrid[position.Y][position.X - 1];
      }

      if (hasColumnToTheRight)
      {
        yield return octopusGrid[position.Y][position.X + 1];
      }

      if (hasRowBelow)
      {
        var rowBelow = octopusGrid[position.Y + 1];
        yield return rowBelow[position.X];
        if (hasColumnToTheLeft)
        {
          yield return rowBelow[position.X - 1];
        }

        if (hasColumnToTheRight)
        {
          yield return rowBelow[position.X + 1];
        }
      }
    }

    private class Octopus
    {
      public Position Position { get; init; }
      public int EnergyLevel { get; set; }
      public int FlashCount { get; set; }
    }

    private record Position(int X, int Y);
  }
}
