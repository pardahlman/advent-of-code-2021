using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021
{
  public class Day06 : IProblem
  {
    public int Day => 6;

    public string SolvePartOne(ICollection<string> puzzleInput) => SolveProblem(puzzleInput, days: 80);

    public string SolvePartTwo(ICollection<string> puzzleInput) => SolveProblem(puzzleInput, days: 256);

    private static string SolveProblem(ICollection<string> puzzleInput, int days)
    {
      var grownUps = puzzleInput.First().Split(",")
        .Select(int.Parse)
        .GroupBy(f => f)
        .ToDictionary(f => f.Key, f => (double)f.Count());

      var children = new Dictionary<int, double>();

      for (var day = 1; day <= days; day++)
      {
        // Transition grown-up fish to next stage
        grownUps = grownUps
          .ToDictionary(
            kvp => NextStage(kvp.Key),
            kvp => kvp.Value);

        // Transition children to next stage
        children = children
          .ToDictionary(
            kvp => kvp.Key - 1,
            kvp => kvp.Value);

        // Spawn newborns for fish that entered stage 6
        var numberOfNewborn = grownUps.ContainsKey(6) ? grownUps[6] : 0;

        // Move children that are in stage 6 to grown-up list
        if (children.ContainsKey(6))
        {
          grownUps[6] = (grownUps.ContainsKey(6) ? grownUps[6] : 0) + children[6];
          children.Remove(6);
        }

        // Move newborns to rest of children
        children[8] = numberOfNewborn;
      }

      return (grownUps.Sum(kvp => kvp.Value) + children.Sum(c => c.Value)).ToString();
    }

    private static int NextStage(int fish)
    {
      var nextStage = fish - 1;
      return nextStage >= 0 ? nextStage : 6;
    }
  }
}
