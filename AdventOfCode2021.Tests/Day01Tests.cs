using System.Collections.Generic;
using NUnit.Framework;

namespace AdventOfCode2021.Tests
{
  public class Day01Tests
  {
    private readonly List<string> _testInput = new()
    {
      "199", "200", "208", "210", "200", "207", "240", "269", "260", "263",
    };

    [Test]
    public void Can_Solve_Part_One()
    {
      // Arrange
      var problem = new Day01();

      // Act
      var solution = problem.SolvePartOne(_testInput);

      // Assert
      Assert.That(solution, Is.EqualTo("7"));
    }

    [Test]
    public void Can_Solve_Part_Two()
    {
      // Arrange
      var problem = new Day01();

      // Act
      var solution = problem.SolvePartTwo(_testInput);

      // Assert
      Assert.That(solution, Is.EqualTo("5"));
    }
  }
}
