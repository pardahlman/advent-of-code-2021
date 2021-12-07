using System.Collections.Generic;
using NUnit.Framework;

namespace AdventOfCode2021.Tests
{
  public class Day06Tests
  {
    private readonly List<string> _testInput = new() { "3,4,3,1,2" };

    [Test]
    public void Can_Solve_Part_One()
    {
      // Arrange
      var problem = new Day06();

      // Act
      var solution = problem.SolvePartOne(_testInput);

      // Assert
      Assert.That(solution, Is.EqualTo("5934"));
    }
  }
}
