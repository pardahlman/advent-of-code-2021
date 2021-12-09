using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021
{
  public class Day09 : IProblem
  {
    public int Day => 9;

    public string SolvePartOne(ICollection<string> puzzleInput)
    {
      var heightmap = puzzleInput
        .Select(row => row.Select(c => int.Parse(c.ToString())).ToList())
        .ToList();

      var riskLevels = new List<int>();

      for (var rowIndex = 0; rowIndex < heightmap.Count; rowIndex++)
      {
        var row = heightmap[rowIndex];
        for (var columnIndex = 0; columnIndex < row.Count; columnIndex++)
        {
          var cell = row[columnIndex];
          var toTheLeft = columnIndex == 0 ? int.MaxValue : row[columnIndex-1];
          var toTheRight = columnIndex == row.Count - 1 ? int.MaxValue : row[columnIndex + 1];
          var above = rowIndex == 0 ? int.MaxValue : heightmap[rowIndex - 1][columnIndex];
          var below = rowIndex == heightmap.Count - 1 ? int.MaxValue : heightmap[rowIndex + 1][columnIndex];
          if (cell < toTheLeft && cell < toTheRight && cell < above && cell < below)
          {
            riskLevels.Add(cell + 1);
          }
        }
      }

      return riskLevels.Sum().ToString();
    }

    public string SolvePartTwo(ICollection<string> puzzleInput)
    {
      throw new NotImplementedException();
    }
  }
}
