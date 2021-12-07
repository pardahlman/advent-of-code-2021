using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021
{
  public class Day06 : IProblem
  {
    public int Day => 6;

    public string SolvePartOne(ICollection<string> puzzleInput)
    {
      var grownUps = puzzleInput.First().Split(",").Select(int.Parse).ToList();
      var children = new List<int>();

      for (var day = 1; day <= 80; day++)
      {
        // Transition grown-up fish to next stage
        grownUps = grownUps.Select(NextStage).ToList();

        // Spawn newborns for fish that entered stage 6
        var newborns = grownUps.Where(SpawnsNewborn).Select(_ => 8).ToList();

        // Transition children to next stage
        children = children.Select(fish => fish - 1).ToList();

        // Move children that are in stage 6 to grown-up list
        grownUps.AddRange((children.Where(c => c == 6)));

        // Move newborns to rest of children
        children = children.Where(fish => fish > 6).Concat(newborns).ToList();
      }

      return (grownUps.Count + children.Count).ToString();
    }

    private static bool SpawnsNewborn(int fish) => fish == 6;

    public string SolvePartTwo(ICollection<string> puzzleInput)
    {
      throw new System.NotImplementedException();
    }

    private static int NextStage(int fish)
    {
      var nextStage = fish - 1;
      return nextStage >= 0 ? nextStage : 6;
    }
  }
}
