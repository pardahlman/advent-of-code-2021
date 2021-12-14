using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace AdventOfCode2021
{
  public class Day14 : IProblem
  {
    public int Day => 14;

    public string SolvePartOne(ICollection<string> puzzleInput)
    {
      var (polymerTemplate, pairInsertionRule) = ParsePuzzleInput(puzzleInput);
      var result = Evolve(polymerTemplate, pairInsertionRule).Skip(9).First();
      var group = result.GroupBy(c => c).ToList();
      return (group.Max(g => g.Count()) - group.Min(g => g.Count())).ToString();
    }

    public string SolvePartTwo(ICollection<string> puzzleInput)
    {
      throw new System.NotImplementedException();
    }

    private IEnumerable<string> Evolve(string polymerTemplate, IImmutableDictionary<string, char> rules)
    {
      while (true)
      {
        var nextTemplate = "";
        for (var i = 0; i < polymerTemplate.Length -1; i++)
        {
          var pair = polymerTemplate[i..(i+2)];
          nextTemplate += pair[0];
          if (rules.ContainsKey(pair))
          {
            nextTemplate += rules[pair];
          }
        }

        nextTemplate += polymerTemplate.Last();

        yield return nextTemplate;
        polymerTemplate = nextTemplate;
      }
    }

    private static (string, IImmutableDictionary<string, char>) ParsePuzzleInput(ICollection<string> puzzleInput)
    {
      var polymerTemplate = puzzleInput.First();
      var pairInsertionRules = puzzleInput
        .Skip(2)
        .ToImmutableDictionary(
          row => row[..2],
          row => row.Last());

      return (polymerTemplate, pairInsertionRules);
    }

    private record PairInsertionRule(char ElementA, char ElementB, char ElementToInsert);
  }
}
