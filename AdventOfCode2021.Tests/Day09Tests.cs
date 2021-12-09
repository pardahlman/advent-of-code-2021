using System.Collections.Generic;
using NUnit.Framework;

namespace AdventOfCode2021.Tests
{
  public class Day09Tests
  {
    private readonly List<string> _testInput = new()
    {
      "2199943210",
      "3987894921",
      "9856789892",
      "8767896789",
      "9899965678",
    };

    [Test]
    public void Can_Solve_Part_One()
    {
      // Arrange
      var problem = new Day09();

      // Act
      var solution = problem.SolvePartOne(_testInput);

      // Assert
      Assert.That(solution, Is.EqualTo("15"));
    }

    [Test]
    public void Can_Solve_Part_Two()
    {
      // Arrange
      var problem = new Day09();

      // Act
      var solution = problem.SolvePartTwo(_testInput);

      // Assert
      Assert.That(solution, Is.EqualTo("1134"));
    }
  }
}
