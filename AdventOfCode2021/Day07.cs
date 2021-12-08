using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021
{
  public class Day07 : IProblem
  {
    public int Day => 7;

    public string SolvePartOne(ICollection<string> puzzleInput) => SolveProblem(puzzleInput, LinearFuelCalculation);

    public string SolvePartTwo(ICollection<string> puzzleInput) => SolveProblem(puzzleInput, ArithmeticFuelCalculation);

    private static int LinearFuelCalculation(int targetPosition, int crabPosition) =>
      Math.Abs(targetPosition - crabPosition);

    private static int ArithmeticFuelCalculation(int targetPosition, int crabPosition)
    {
      var distance = Math.Abs(targetPosition - crabPosition);
      return distance * (1 + distance) / 2;
    }

    private static string SolveProblem(IEnumerable<string> puzzleInput, Func<int, int, int> fuelCalculation)
    {
      var crabPositions = puzzleInput.First().Split(",").Select(int.Parse).ToList();

      var minPosition = crabPositions.Min();
      var maxPosition = crabPositions.Max();
      var lowestFuelCost = int.MaxValue;

      for (var targetPosition = minPosition; targetPosition <= maxPosition; targetPosition++)
      {
        var fuelCost = crabPositions.Sum(crabPosition => fuelCalculation(targetPosition, crabPosition));
        if (lowestFuelCost > fuelCost)
        {
          lowestFuelCost = fuelCost;
        }
      }

      return lowestFuelCost.ToString();
    }
  }
}
