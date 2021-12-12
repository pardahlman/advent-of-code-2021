using System.Collections.Generic;
using NUnit.Framework;

namespace AdventOfCode2021.Tests
{
  public class Day12Tests
  {
    private readonly List<string> _testInput = new()
    {
      "fs-end",
      "he-DX",
      "fs-he",
      "start-DX",
      "pj-DX",
      "end-zg",
      "zg-sl",
      "zg-pj",
      "pj-he",
      "RW-he",
      "fs-DX",
      "pj-RW",
      "zg-RW",
      "start-pj",
      "he-WI",
      "zg-he",
      "pj-fs",
      "start-RW"
    };

    [Test]
    public void Can_Solve_Part_One()
    {
      // Arrange
      var problem = new Day12();

      // Act
      var solution = problem.SolvePartOne(_testInput);

      // Assert
      Assert.That(solution, Is.EqualTo("226"));
    }

    [Test]
    public void Can_Solve_Part_Two()
    {
      // Arrange
      var problem = new Day12();

      // Act
      var solution = problem.SolvePartTwo(_testInput);

      // Assert
      Assert.That(solution, Is.EqualTo("3509"));
    }
  }
}
