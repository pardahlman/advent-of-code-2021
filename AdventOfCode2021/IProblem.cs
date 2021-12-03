using System.Collections.Generic;

namespace AdventOfCode2021
{
  public interface IProblem
  {
    int Day { get; }
    string SolvePartOne(ICollection<string> puzzleInput);
    string SolvePartTwo(ICollection<string> puzzleInput);
  }
}
