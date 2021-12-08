using System.Collections.Generic;
using NUnit.Framework;

namespace AdventOfCode2021.Tests
{
  public class Day07Tests
  {
    private readonly List<string> _testInput = new() { "16,1,2,0,4,2,7,1,2,14" };

    [Test]
    public void Can_Solve_Part_One()
    {
      // Arrange
      var problem = new Day07();

      // Act
      var solution = problem.SolvePartOne(_testInput);

      // Assert
      Assert.That(solution, Is.EqualTo("37"));
    }

    [Test]
    public void Can_Solve_Part_Two()
    {
      // Arrange
      var problem = new Day07();

      // Act
      var solution = problem.SolvePartTwo(_testInput);

      // Assert
      Assert.That(solution, Is.EqualTo("168"));
    }
  }
}
