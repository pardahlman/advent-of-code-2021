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
      return SolveProblem(polymerTemplate, pairInsertionRule, 10);
    }

    public string SolvePartTwo(ICollection<string> puzzleInput)
    {
      var (polymerTemplate, pairInsertionRule) = ParsePuzzleInput(puzzleInput);
      return SolveProblem(polymerTemplate, pairInsertionRule, 40);
    }

    private static string SolveProblem(string polymer, IImmutableDictionary<string, char> rules, int iterations)
    {
      var pairCount = new Dictionary<string, long>();
      for (var i = 0; i < polymer.Length -1; i++)
      {
        var pair = polymer[i..(i + 2)];
        pairCount[pair] = pairCount.GetValueOrDefault(pair) + 1;
      }

      for (var i = 0; i < iterations; i++)
      {
        var next = new Dictionary<string, long>();
        foreach (var (pair, count) in pairCount)
        {
          if (!rules.ContainsKey(pair))
          {
            continue;
          }

          var newElement = rules[pair];
          var firstNewPair = $"{pair[0]}{newElement}";
          var secondNewPair = $"{newElement}{pair[1]}";

          next[firstNewPair] = next.GetValueOrDefault(firstNewPair) + count;
          next[secondNewPair] = next.GetValueOrDefault(secondNewPair) + count;
        }

        pairCount = next;
      }

      var elementCount = new Dictionary<char, long>();
      foreach (var (pair, count) in pairCount)
      {
        elementCount[pair[0]] = elementCount.GetValueOrDefault(pair[0]) + count;
      }
      elementCount[polymer.Last()] = elementCount.GetValueOrDefault(polymer.Last()) + 1;

      return (elementCount.Values.Max() - elementCount.Values.Min()).ToString();
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
  }
}
