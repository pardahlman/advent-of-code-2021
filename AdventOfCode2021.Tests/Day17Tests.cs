using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace AdventOfCode2021.Tests
{
  public class Day17Tests
  {
    private readonly List<string> _testInput = new()
    {
      "target area: x=20..30, y=-10..-5"
    };

    [Test]
    public void Can_Solve_Part_One()
    {
      // Arrange
      var problem = new Day17();

      // Act
      var solution = problem.SolvePartOne(_testInput);

      // Assert
      Assert.That(solution, Is.EqualTo("45"));
    }

    [Test]
    public void Can_Solve_Part_Two()
    {
      // Arrange
      var problem = new Day17();

      // Act
      var solution = problem.SolvePartTwo(_testInput);

      // Assert
      Assert.That(solution, Is.EqualTo("112"));
    }
  }
}
