using System.Collections.Generic;
using NUnit.Framework;

namespace AdventOfCode2021.Tests
{
  public class Day14Tests
  {
    private readonly List<string> _testInput = new()
    {
      "NNCB",
      "",
      "CH -> B",
      "HH -> N",
      "CB -> H",
      "NH -> C",
      "HB -> C",
      "HC -> B",
      "HN -> C",
      "NN -> C",
      "BH -> H",
      "NC -> B",
      "NB -> B",
      "BN -> B",
      "BB -> N",
      "BC -> B",
      "CC -> N",
      "CN -> C",
    };

    [Test]
    public void Can_Solve_Part_One()
    {
      // Arrange
      var problem = new Day14();

      // Act
      var solution = problem.SolvePartOne(_testInput);

      // Assert
      Assert.That(solution, Is.EqualTo("1588"));
    }

    [Test]
    public void Can_Solve_Part_Two()
    {
      // Arrange
      var problem = new Day14();

      // Act
      var solution = problem.SolvePartTwo(_testInput);

      // Assert
      Assert.That(solution, Is.EqualTo("2188189693529"));
    }
  }
}
