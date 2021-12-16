using System.Collections.Generic;
using NUnit.Framework;

namespace AdventOfCode2021.Tests
{
  public class Day16Tests
  {
    [Test]
    public void Can_Convert_Hex_To_Binary()
    {
      // Arrange
      const string transmission = "D2FE28";

      // Act
      var binaryData = Day16.HexToBinary(transmission);

      // Assert
      Assert.That(binaryData, Is.EqualTo("110100101111111000101000"));
    }

    [Test]
    public void Can_Parse_Type_4_Package()
    {
      // Arrange
      const string package = "D2FE28";

      // Act
      var transmission = Day16.ParseRootTransmission(package);

      // Assert
      Assert.That(transmission.Version, Is.EqualTo(6));
      Assert.That(transmission.Type, Is.EqualTo(4));
      Assert.That(transmission.Value, Is.EqualTo(2021));
    }

    [Test]
    public void Can_Parse_Operation_With_Length_Type_0()
    {
      // Arrange
      const string package = "38006F45291200";

      // Act
      var transmission = Day16.ParseRootTransmission(package);

      // Assert
      Assert.That(transmission, Is.Not.Null);
      Assert.That(transmission.SubPackets, Has.Count.EqualTo(2));
      Assert.That((transmission.SubPackets[0]).Value, Is.EqualTo(10));
      Assert.That((transmission.SubPackets[1]).Value, Is.EqualTo(20));
    }

    [Test]
    public void Can_Parse_Operation_With_Length_Type_1()
    {
      // Arrange
      const string package = "EE00D40C823060";

      // Act
      var transmission = Day16.ParseRootTransmission(package);

      // Assert
      Assert.That(transmission, Is.Not.Null);
      Assert.That(transmission.SubPackets, Has.Count.EqualTo(3));
      Assert.That((transmission.SubPackets[0]).Value, Is.EqualTo(1));
      Assert.That((transmission.SubPackets[1]).Value, Is.EqualTo(2));
      Assert.That((transmission.SubPackets[2]).Value, Is.EqualTo(3));
    }

    [TestCase("8A004A801A8002F478", "16")]
    [TestCase("620080001611562C8802118E34", "12")]
    [TestCase("C0015000016115A2E0802F182340", "23")]
    [TestCase("A0016C880162017C3686B18A3D4780", "31")]
    public void Can_Solve_Part_1(string input, string expected)
    {
      // Arrange
      var problem = new Day16();

      // Act
      var result = problem.SolvePartOne(new List<string> { input });

      // Assert
      Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase("C200B40A82", "3")]
    [TestCase("04005AC33890", "54")]
    [TestCase("880086C3E88112", "7")]
    [TestCase("CE00C43D881120", "9")]
    [TestCase("D8005AC2A8F0", "1")]
    [TestCase("F600BC2D8F", "0")]
    [TestCase("9C005AC2F8F0", "0")]
    [TestCase("9C0141080250320F1802104A08", "1")]
    public void Can_Solve_Part_2(string input, string expected)
    {
      // Arrange
      var problem = new Day16();

      // Act
      var result = problem.SolvePartTwo(new List<string> { input });

      // Assert
      Assert.That(result, Is.EqualTo(expected));
    }
  }
}
