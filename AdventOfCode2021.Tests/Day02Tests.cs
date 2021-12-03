using System.Collections.Generic;
using NUnit.Framework;

namespace AdventOfCode2021.Tests
{
  public class Day02Tests
  {
    private readonly List<string> _testInput = new()
    {
      "forward 5",
      "down 5",
      "forward 8",
      "up 3",
      "down 8",
      "forward 2",
    };

    [Test]
    public void Can_Solve_Part_One()
    {
      // Arange
      var problem = new Day02();

      // Act
      var solution = problem.SolvePartOne(_testInput);

      // Assert
      Assert.That(solution, Is.EqualTo("150"));
    }

    [Test]
    public void Can_Solve_Part_Two()
    {
      // Arange
      var problem = new Day02();

      // Act
      var solution = problem.SolvePartTwo(_testInput);

      // Assert
      Assert.That(solution, Is.EqualTo("900"));
    }
  }
}
