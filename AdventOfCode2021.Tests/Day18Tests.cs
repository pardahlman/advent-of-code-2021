using NUnit.Framework;

namespace AdventOfCode2021.Tests
{
  public class Day18Tests
  {
    [Test]
    public void Sum_Without_Explode_Or_Split()
    {
      // Arrange
      // Act
      var sum = Day18.Sum("[1,1]", "[2,2]", "[3,3]","[4,4]");
      // Assert
      Assert.That(sum, Is.EqualTo("[[[[1,1],[2,2]],[3,3]],[4,4]]"));
    }

    [TestCase("[[[[[9,8],1],2],3],4]", "[[[[0,9],2],3],4]")]
    [TestCase("[7,[6,[5,[4,[3,2]]]]]", "[7,[6,[5,[7,0]]]]")]
    [TestCase("[[6,[5,[4,[3,2]]]],1]", "[[6,[5,[7,0]]],3]")]
    [TestCase("[[6,[5,[4,[3,2]]]],1]", "[[6,[5,[7,0]]],3]")]
    [TestCase("[[3,[2,[1,[7,3]]]],[6,[5,[4,[3,2]]]]]", "[[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]]")]
    [TestCase("[[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]]", "[[3,[2,[8,0]]],[9,[5,[7,0]]]]")]
    public void Explode(string number, string expected)
    {
      // Arrange
      // Act
      var success = Day18.TryExplode(number, out var result);

      // Assert
      Assert.That(success, Is.True);
      Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase("[[[[0,7],4],[15,[0,13]]],[1,1]]", "[[[[0,7],4],[[7,8],[0,13]]],[1,1]]")]
    [TestCase("[[[[0,7],4],[[7,8],[0,13]]],[1,1]]", "[[[[0,7],4],[[7,8],[0,[6,7]]]],[1,1]]")]
    public void Split(string number, string expected)
    {
      var success = Day18.TrySplit(number, out var result);
      Assert.That(success, Is.True);
      Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase("[[[[4,3],4],4],[7,[[8,4],9]]]", "[1,1]", "[[[[0,7],4],[[7,8],[6,0]]],[8,1]]")]
    [TestCase("[[[[7,7],[7,7]],[[8,7],[8,7]]],[[[7,0],[7,7]],9]]", "[[[[4,2],2],6],[8,7]]", "[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]")]
    [TestCase("[[[0,[4,5]],[0,0]],[[[4,5],[2,6]],[9,5]]]", "[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]", "[[[[4,0],[5,4]],[[7,7],[6,0]]],[[8,[7,7]],[[7,9],[5,0]]]]")]
    [TestCase("[[[[4,0],[5,4]],[[7,7],[6,0]]],[[8,[7,7]],[[7,9],[5,0]]]]", "[[2,[[0,8],[3,4]]],[[[6,7],1],[7,[1,6]]]]", "[[[[6,7],[6,7]],[[7,7],[0,7]]],[[[8,7],[7,7]],[[8,8],[8,0]]]]")]
    [TestCase("[[[[6,7],[6,7]],[[7,7],[0,7]]],[[[8,7],[7,7]],[[8,8],[8,0]]]]", "[[[[2,4],7],[6,[0,5]]],[[[6,8],[2,8]],[[2,1],[4,5]]]]", "[[[[7,0],[7,7]],[[7,7],[7,8]]],[[[7,7],[8,8]],[[7,7],[8,7]]]]")]
    public void Sum(string first, string second, string expectedSum)
    {
      var sum = Day18.Sum(first, second);
      Assert.That(sum, Is.EqualTo(expectedSum));
    }

    [Test]
    public void Sum_Multiple_Numbers()
    {
      var sum = Day18.Sum("[1,1]", "[2,2]", "[3,3]", "[4,4]", "[5,5]", "[6,6]");
      Assert.That(sum, Is.EqualTo("[[[[5,0],[7,4]],[5,5]],[6,6]]"));
    }

    [Test]
    public void Larger_Example()
    {
      const string number = "[[[[[4,3],4],4],[7,[[8,4],9]]],[1,1]]";
      Assert.True(Day18.TryExplode(number, out var first));
      Assert.That(first, Is.EqualTo("[[[[0,7],4],[7,[[8,4],9]]],[1,1]]"));

      Assert.True(Day18.TryExplode(first, out var second));
      Assert.That(second, Is.EqualTo("[[[[0,7],4],[15,[0,13]]],[1,1]]"));
      Assert.False(Day18.TryExplode(second, out _));

      Assert.True(Day18.TrySplit(second, out var third));
      Assert.That(third, Is.EqualTo("[[[[0,7],4],[[7,8],[0,13]]],[1,1]]"));
      Assert.False(Day18.TryExplode(third, out _));

      Assert.True(Day18.TrySplit(third, out var forth));
      Assert.That(forth, Is.EqualTo("[[[[0,7],4],[[7,8],[0,[6,7]]]],[1,1]]"));

      Assert.True(Day18.TryExplode(forth, out var fifth));
      Assert.That(fifth, Is.EqualTo("[[[[0,7],4],[[7,8],[6,0]]],[8,1]]"));
    }

    [TestCase("[9,1]", 29)]
    [TestCase("[[1,2],[[3,4],5]]", 143)]
    [TestCase("[[[[7,8],[6,6]],[[6,0],[7,7]]],[[[7,8],[8,8]],[[7,9],[0,6]]]]", 3993)]
    public void Magnitude(string number, int expectedMagnitude)
    {
      var magnitude = Day18.CalculateMagnitude(number);
      Assert.That(magnitude, Is.EqualTo(expectedMagnitude));
    }

    [Test]
    public void Solve_Part_2()
    {
      // Arrange
      var input = new[]
      {
        "[[[0,[5,8]],[[1,7],[9,6]]],[[4,[1,2]],[[1,4],2]]]",
        "[[[5,[2,8]],4],[5,[[9,9],0]]]",
        "[6,[[[6,2],[5,6]],[[7,6],[4,7]]]]",
        "[[[6,[0,7]],[0,9]],[4,[9,[9,0]]]]",
        "[[[7,[6,4]],[3,[1,3]]],[[[5,5],1],9]]",
        "[[6,[[7,3],[3,2]]],[[[3,8],[5,7]],4]]",
        "[[[[5,4],[7,7]],8],[[8,3],8]]",
        "[[9,3],[[9,9],[6,[4,9]]]]",
        "[[2,[[7,7],7]],[[5,8],[[9,3],[0,2]]]]",
        "[[[[5,2],5],[8,[3,7]]],[[5,[7,5]],[4,4]]]",
      };

      var problem = new Day18();

      // Act
      var solution = problem.SolvePartTwo(input);

      // Assert
      Assert.That(solution, Is.EqualTo("3993"));
    }
  }
}
