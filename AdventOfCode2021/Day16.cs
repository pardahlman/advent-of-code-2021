using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021
{
  public class Day16 : IProblem
  {
    public int Day => 16;

    public string SolvePartOne(ICollection<string> puzzleInput)
    {
      var transmission = ParseRootTransmission(puzzleInput.Single());
      return SumVersion(transmission).ToString();
    }

    public string SolvePartTwo(ICollection<string> puzzleInput)
    {
      var transmission = ParseRootTransmission(puzzleInput.Single());
      return transmission.Value.ToString();
    }

    private static long SumVersion(BitsTransmission transmission) =>
      transmission.Type switch
      {
        4 => transmission.Version,
        _ => transmission.Version + transmission.SubPackets.Sum(SumVersion),
      };

    public static string HexToBinary(string hexString) =>
      string.Join(string.Empty,
        hexString.Select(
          hex =>
          {
            var asDecimal = Convert.ToInt32(hex.ToString(), fromBase: 16);
            var asBinary = Convert.ToString(asDecimal, toBase: 2);
            return asBinary.PadLeft(4, '0');
          })
      );

    private static int BinaryToInt(string binary) => Convert.ToInt32(binary, fromBase: 2);

    private static long BinaryToLong(string binary) => Convert.ToInt64(binary, fromBase: 2);

    public static BitsTransmission ParseRootTransmission(string hexString) => ParseTransmissions(HexToBinary(hexString)).Single();

    private static List<BitsTransmission> ParseTransmissions(string binaryString)
    {
      var result = new List<BitsTransmission>();
      var reader = new BitReader(binaryString);
      while (!reader.Completed)
      {
        result.Add(ParseOnePackage(reader));
      }

      return result;
    }

    private static BitsTransmission ParseOnePackage(BitReader reader)
    {
      var version = BinaryToInt(reader.ReadNext(3));
      var type = BinaryToInt(reader.ReadNext(3));

      return type == 4
        ? ParseLiteralTransmission(reader, version, type)
        : ParseOperationTransmission(reader, version, type);
    }

    private static BitsTransmission ParseOperationTransmission(BitReader reader, int version, int type)
    {
      var lengthTypeId = reader.ReadNext(1);
      List<BitsTransmission> subPackets;
      switch (lengthTypeId)
      {
        case "0":
        {
          var lengthOfSubPackets = BinaryToInt(reader.ReadNext(15));
          subPackets = ParseTransmissions(reader.ReadNext(lengthOfSubPackets));
          break;
        }
        case "1":
        {
          var numberOfSubPackets = BinaryToInt(reader.ReadNext(11));
          subPackets = new List<BitsTransmission>();
          for (var i = 0; i < numberOfSubPackets; i++)
          {
            subPackets.Add(ParseOnePackage(reader));
          }

          break;
        }
        default:
          throw new Exception();
      }

      var value = type switch
      {
        0 => subPackets.Sum(p => p.Value),
        1 => subPackets.Aggregate(1L, (product, t) => product * t.Value),
        2 => subPackets.Min(p => p.Value),
        3 => subPackets.Max(p => p.Value),
        5 => subPackets[0].Value > subPackets[1].Value ? 1 : 0,
        6 => subPackets[0].Value < subPackets[1].Value ? 1 : 0,
        7 => subPackets[0].Value == subPackets[1].Value ? 1 : 0,
        _ => throw new Exception($"Unexpected type {type}")
      };

      var transmission = new BitsTransmission
      {
        Version = version,
        Type = type,
        SubPackets = subPackets,
        Value = value
      };

      return transmission;
    }

    private static BitsTransmission ParseLiteralTransmission(BitReader reader, int version, int type)
    {
      var binaryValue = "";
      bool keepReading;
      do
      {
        keepReading = reader.ReadNext(1) == "1";
        binaryValue += reader.ReadNext(4);
      } while (keepReading);

      return new BitsTransmission
      {
        Version = version,
        Type = type,
        Value = BinaryToLong(binaryValue)
      };
    }

    private class BitReader
    {
      private readonly string _binaryString;
      private int _position;
      public bool Completed => _binaryString[_position..].All(c => c == '0');

      public BitReader(string binaryString)
      {
        Span<char> c = binaryString.ToArray();
        _binaryString = binaryString;
        _position = 0;
      }

      public string ReadNext(int count) => _binaryString[_position..(_position += count)];
    }

    public class BitsTransmission
    {
      public int Version { get; init; }
      public int Type { get; init; }
      public long Value { get; init; }
      public List<BitsTransmission> SubPackets { get; init; }
    }
  }
}
