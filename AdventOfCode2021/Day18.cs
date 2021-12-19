using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2021
{
  public class Day18 : IProblem
  {
    public int Day => 18;

    public string SolvePartOne(ICollection<string> puzzleInput) => CalculateMagnitude(Sum(puzzleInput.ToArray())).ToString();

    public string SolvePartTwo(ICollection<string> puzzleInput)
    {
      var magnitudes = new List<int>();
      for (var i = 0; i < puzzleInput.Count - 1; i++)
      {
        for (var j = i+1; j < puzzleInput.Count; j++)
        {
          magnitudes.Add(CalculateMagnitude(Sum(puzzleInput.ElementAt(i), puzzleInput.ElementAt(j))));
          magnitudes.Add(CalculateMagnitude(Sum(puzzleInput.ElementAt(j), puzzleInput.ElementAt(i))));
        }
      }

      return magnitudes.Max().ToString();
    }

    public static string Sum(params string[] numbers) => numbers.Aggregate((a, b) => ExplodeAndSplit($"[{a},{b}]"));

    private static string ExplodeAndSplit(string number)
    {
      while (TryExplode(number, out var exploded))
      {
        number = exploded;
      }

      if (TrySplit(number, out var split))
      {
        return ExplodeAndSplit(split);
      }

      return number;
    }

    public static bool TryExplode(string number, out string exploded)
    {
      var depth = 0;
      int? startIndex = default;
      int? endIndex = default;
      for (var i = 0; i < number.Length; i++)
      {
        if (number[i] == '[' && (depth += 1) == 5)
        {
          startIndex = i;
        }

        if (number[i] == ']' && (depth -= 1) == 4)
        {
          endIndex = i;
          break;
        }
      }

      if (!startIndex.HasValue || !endIndex.HasValue)
      {
        exploded = default;
        return false;
      }

      var leftSegment = number[..(startIndex.Value)];
      var pair = Regex.Matches(number[startIndex.Value..endIndex.Value], "\\d+").Select(g => int.Parse(g.Value)).ToList();
      var rightSegment = number[(endIndex.Value + 1)..];

      var leftNumber = Regex.Matches(leftSegment, "\\d+").LastOrDefault();
      var rightNumber = Regex.Matches(rightSegment, "\\d+").FirstOrDefault();

      if (leftNumber != default)
      {
        var newNumber = int.Parse(leftNumber.Value) + pair[0];
        leftSegment = $"{leftSegment[..leftNumber.Index]}{newNumber}{leftSegment[(leftNumber.Index + leftNumber.Value.Length)..]}";
      }

      if (rightNumber != default)
      {
        var newNumber = int.Parse(rightNumber.Value) + pair[1];
        rightSegment = $"{rightSegment[..rightNumber.Index]}{newNumber}{rightSegment[(rightNumber.Index + rightNumber.Value.Length)..]}";
      }

      exploded = $"{leftSegment}0{rightSegment}";
      return true;
    }

    public static bool TrySplit(string number, out string split)
    {
      var match = Regex.Match(number, "\\d{2}");
      if (!match.Success)
      {
        split = default;
        return false;
      }

      var largeNumber = float.Parse(match.Value);
      var newPair = $"[{Math.Round(largeNumber/2, MidpointRounding.ToZero)},{Math.Round(largeNumber/2, MidpointRounding.AwayFromZero)}]";

      var regex = new Regex(match.Value);
      split = regex.Replace(number, newPair, 1);;
      return true;
    }

    public static int CalculateMagnitude(string number)
    {
      var magnitudeRegex = new Regex("\\[(\\d+),(\\d+)\\]");
      var matches = magnitudeRegex.Matches(number);

      while (matches.Count > 0)
      {
        foreach (var match in matches.Reverse())
        {
          var magnitude = int.Parse(match.Groups[1].Value) * 3 + int.Parse(match.Groups[2].Value) * 2;
          number = number[..(match.Index)] + magnitude + number[(match.Index + match.Length)..];
        }

        matches = magnitudeRegex.Matches(number);
      }

      return int.Parse(number);
    }
  }
}
