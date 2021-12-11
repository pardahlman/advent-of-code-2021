using System.Collections.Generic;
using NUnit.Framework;

namespace AdventOfCode2021.Tests
{
  public class Day11Tests
  {
    private readonly List<string> _testInput = new()
    {
      "5483143223",
      "2745854711",
      "5264556173",
      "6141336146",
      "6357385478",
      "4167524645",
      "2176841721",
      "6882881134",
      "4846848554",
      "5283751526",
    };

    [Test]
    public void Can_Solve_Part_One()
    {
      // Arrange
      var problem = new Day11();

      // Act
      var solution = problem.SolvePartOne(_testInput);

      // Assert
      Assert.That(solution, Is.EqualTo("1656"));
    }

    [Test]
    public void Can_Solve_Part_Two()
    {
      // Arrange
      var problem = new Day11();

      // Act
      var solution = problem.SolvePartTwo(_testInput);

      // Assert
      Assert.That(solution, Is.EqualTo("unknown"));
    }
  }
}
