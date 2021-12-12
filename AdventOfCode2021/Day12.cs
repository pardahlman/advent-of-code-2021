using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace AdventOfCode2021
{
  public class Day12 : IProblem
  {
    public int Day => 12;

    private const string StartCave = "start";
    private const string EndCave = "end";

    public string SolvePartOne(ICollection<string> puzzleInput) =>
      ExploreCave(new List<string> { StartCave }, CreateCaveConnections(puzzleInput))
        .Count(path => !HasVisitedSmallCaveTwice(path))
        .ToString();

    public string SolvePartTwo(ICollection<string> puzzleInput) =>
      ExploreCave(new List<string> { StartCave }, CreateCaveConnections(puzzleInput))
        .Count()
        .ToString();

    private static ImmutableDictionary<string, ImmutableList<string>> CreateCaveConnections(
      IEnumerable<string> puzzleInput) =>
      puzzleInput
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
        .ToImmutableDictionary(
          g => g.Key,
          g => g.Where(c => c!= StartCave).ToImmutableList()
        );

    private static IEnumerable<List<string>> ExploreCave(List<string> path, ImmutableDictionary<string, ImmutableList<string>> caveConnections)
    {
      var currentCave = path.Last();
      if (currentCave == EndCave)
      {
        return new List<List<string>> { path };
      }

      return caveConnections[currentCave]
        .Where(adjacentCave =>
        {
          if (adjacentCave.All(char.IsUpper))
          {
            return true;
          }

          if (!path.Contains(adjacentCave))
          {
            return true;
          }

          return !HasVisitedSmallCaveTwice(path);
        })
        .SelectMany(nextCave => ExploreCave(new List<string>(path) { nextCave }, caveConnections));
    }

    private static bool HasVisitedSmallCaveTwice(IEnumerable<string> path) =>
      path
        .Where(c => c.All(char.IsLower))
        .GroupBy(c => c)
        .Max(c => c.Count()) == 2;
  }
}
