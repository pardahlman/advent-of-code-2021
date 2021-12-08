using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021
{
  public class Day08 : IProblem
  {
    public int Day => 8;

    public string SolvePartOne(ICollection<string> puzzleInput)
    {
      return puzzleInput
        .Select(r => r.Split(" | ")[1])
        .SelectMany(outputValues => outputValues.Split(" "))
        .Count(value => value.Length is 2 or 4 or 3 or 7)
        .ToString();
    }

    public string SolvePartTwo(ICollection<string> puzzleInput)
    {
      throw new System.NotImplementedException();
    }
  }
}
