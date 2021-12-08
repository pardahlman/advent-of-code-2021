using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021
{
  public class Day07 : IProblem
  {
    public int Day => 7;

    public string SolvePartOne(ICollection<string> puzzleInput)
    {
      var crabPositions = puzzleInput.First().Split(",").Select(int.Parse).ToList();
      var minPosition = crabPositions.Min();
      var maxPosition = crabPositions.Max();

      var lowestFuelCost = int.MaxValue;

      for (var position = minPosition; position <= maxPosition; position++)
      {
        var fuelCost = crabPositions.Select(p => Math.Abs(position - p)).Sum();
        if (lowestFuelCost > fuelCost)
        {
          lowestFuelCost = fuelCost;
        }
      }

      return lowestFuelCost.ToString();
    }

    public string SolvePartTwo(ICollection<string> puzzleInput)
    {
      throw new System.NotImplementedException();
    }
  }
}
