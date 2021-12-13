using System.Collections.Generic;
using NUnit.Framework;

namespace AdventOfCode2021.Tests
{
  public class Day13Tests
  {
    private readonly List<string> _testInput = new()
    {
      "6,10",
      "0,14",
      "9,10",
      "0,3",
      "10,4",
      "4,11",
      "6,0",
      "6,12",
      "4,1",
      "0,13",
      "10,12",
      "3,4",
      "3,0",
      "8,4",
      "1,10",
      "2,14",
      "8,10",
      "9,0",
      "",
      "fold along y=7",
      "fold along x=5"
    };

    [Test]
    public void Can_Solve_Part_One()
    {
      // Arrange
      var problem = new Day13();

      // Act
      var solution = problem.SolvePartOne(_testInput);

      // Assert
      Assert.That(solution, Is.EqualTo("17"));
    }

    [Test]
    public void Can_Solve_Part_Two()
    {
      // Arrange
      var problem = new Day13();

      // Act
      var solution = problem.SolvePartTwo(_testInput);

      // Assert
      Assert.That(solution, Is.EqualTo("0"));
    }
  }
}
