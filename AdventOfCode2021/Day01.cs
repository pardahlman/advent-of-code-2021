using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021
{
  public class Day01 : IProblem
  {
    public int Day => 1;

    public string SolvePartOne(ICollection<string> puzzleInput) => SolveProblem(puzzleInput, 1);

    public string SolvePartTwo(ICollection<string> puzzleInput) => SolveProblem(puzzleInput, 3);

    private static string SolveProblem(IEnumerable<string> puzzleInput, int slidingWindowSize)
    {
      var measurements = puzzleInput.Select(int.Parse).ToList();
      var measurementSums = new List<int>();
      for (var i = 0; i <= measurements.Count - slidingWindowSize; i++)
      {
        var slidingWindowMeasurement = measurements
          .GetRange(i, slidingWindowSize)
          .Sum();
        measurementSums.Add(slidingWindowMeasurement);
      }

      var increaseCount = 0;
      for (var i = 0; i < measurementSums.Count - 1; i++)
      {
        if (measurementSums[i] < measurementSums[i + 1])
        {
          increaseCount++;
        }
      }

      return increaseCount.ToString();
    }
  }
}
