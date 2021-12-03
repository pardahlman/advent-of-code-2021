using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021
{
  public class Day02 : IProblem
  {
    public int Day => 2;

    public string SolvePartOne(ICollection<string> puzzleInput)
    {
      return puzzleInput.Aggregate(new Position(), (position, input) =>
      {
        var move = input.Split(" ");
        var direction = move[0];
        var amount = int.Parse(move[1]);
        switch (direction)
        {
          case "up":
            position.Y -= amount;
            break;
          case "down":
            position.Y += amount;
            break;
          case "forward":
            position.X += amount;
            break;
          default:
            throw new Exception($"Unknown direction {direction}");
        }

        return position;
      }, position => (position.X * position.Y).ToString());
    }

    public string SolvePartTwo(ICollection<string> puzzleInput)
    {
      return puzzleInput.Aggregate(new Position(), (position, input) =>
      {
        var move = input.Split(" ");
        var direction = move[0];
        var amount = int.Parse(move[1]);
        switch (direction)
        {
          case "up":
            position.Aim -= amount;
            break;
          case "down":
            position.Aim += amount;
            break;
          case "forward":
            position.X += amount;
            position.Y += amount * position.Aim;
            break;
          default:
            throw new Exception($"Unknown direction {direction}");
        }

        return position;
      }, position => (position.X * position.Y).ToString());
    }

    private class Position
    {
      public int Aim { get; set; }
      public int X { get; set; }
      public int Y { get; set; }
    }
  }
}
