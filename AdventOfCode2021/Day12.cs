using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2021
{
  public class Day12 : IProblem
  {
    public int Day => 12;

    private const string StartCave = "start";
    private const string EndCave = "end";

    public string SolvePartOne(ICollection<string> puzzleInput)
    {
      var caveConnections = puzzleInput
        .SelectMany(row =>
        {
          var caves = row.Split("-");
          return new[]
          {
            (caves[0], caves[1]),
            (caves[1], caves[0]),
          };
        })
        .GroupBy(c => c.Item1, c => c.Item2)
        .ToDictionary(c => c.Key, c => c.ToList());
      caveConnections[EndCave] = new List<string>();

      var paths = caveConnections[StartCave]
        .SelectMany(nextCave => ExploreCave(new List<string> { StartCave, nextCave }, caveConnections))
        .ToList();

      return paths.Count.ToString();
    }

    private static List<List<string>> ExploreCave(List<string> path, IReadOnlyDictionary<string, List<string>> caveConnections)
    {
      var currentCave = path.Last();
      if (currentCave == EndCave)
      {
        return new List<List<string>> { path };
      }

      return caveConnections[currentCave]
        .Where(adjacentCave => adjacentCave.All(char.IsUpper) || !path.Contains(adjacentCave))
        .SelectMany(nextCave => ExploreCave(new List<string>(path) { nextCave }, caveConnections))
        .ToList();
    }

    public string SolvePartTwo(ICollection<string> puzzleInput)
    {
      throw new System.NotImplementedException();
    }
  }
}
