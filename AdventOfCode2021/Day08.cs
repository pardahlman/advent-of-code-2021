using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021
{
  public class Day08 : IProblem
  {
    public int Day => 8;

    public string SolvePartOne(ICollection<string> puzzleInput)
    {
      return puzzleInput
        .Select(r => r.Split(" | ")[1])
        .SelectMany(outputValues => outputValues.Split(" "))
        .Count(value => value.Length is 2 or 4 or 3 or 7)
        .ToString();
    }

    public string SolvePartTwo(ICollection<string> puzzleInput)
    {
      var sum = 0;
      foreach (var input in puzzleInput)
      {
        var entry = input.Split(" | ");
        var signalPattern = entry[0].Split(" ");
        var outputValue = entry[1].Split(" ");

        // 1 is the only pattern with 2 segments
        var one = signalPattern.Single(p => p.Length == 2);

        // 4 is the only pattern with 4 segments
        var four = signalPattern.Single(p => p.Length == 4);

        // 7 is the only pattern with 3 segments
        var seven = signalPattern.Single(p => p.Length == 3);

        // 8 is the only pattern with 7 segments
        var eight = signalPattern.Single(p => p.Length == 7);

        // 3 is the only pattern with 5 segments that contains both segments from 1
        var three = signalPattern.Where(p => p.Length == 5).Single(segment => one.All(segment.Contains));

        // 6 is the only pattern with 6 segments that does not contain both segments from 1
        var six = signalPattern.Where(p => p.Length == 6).Single(p => !one.All(p.Contains));

        var segmentC = one.Single(s => !six.Contains(s));

        // 2 is the only pattern with 5 segments except for 3 that uses segment C
        var two = signalPattern.Single(p => p.Length == 5 && p != three && p.Contains(segmentC));

        // 5 is the only pattern with 5 segment that is not 2 or 3
        var five = signalPattern.Single(p => p.Length == 5 && p != two && p != three);

        // 9 is the only pattern with 6 segments that is not 6 that contains all segments from 5
        var nine = signalPattern.Single(p => p.Length == 6 && p != six && five.All(p.Contains));

        // 0 is the only pattern with 6 segments that is not 6 or 9
        var zero = signalPattern.Single(p => p.Length == 6 && p != six && p != nine);

        var clearTextSignal = outputValue.Select(signal =>
        {
          if (IsSignalMatch(signal, zero))
          {
            return 0;
          }

          if (IsSignalMatch(signal, one))
          {
            return 1;
          }

          if (IsSignalMatch(signal, two))
          {
            return 2;
          }

          if (IsSignalMatch(signal, three))
          {
            return 3;
          }

          if (IsSignalMatch(signal, four))
          {
            return 4;
          }

          if (IsSignalMatch(signal, five))
          {
            return 5;
          }

          if (IsSignalMatch(signal, six))
          {
            return 6;
          }

          if (IsSignalMatch(signal, seven))
          {
            return 7;
          }

          if (IsSignalMatch(signal, eight))
          {
            return 8;
          }

          if (IsSignalMatch(signal, nine))
          {
            return 9;
          }

          throw new Exception();
        }).Aggregate(0, (total, next) => total*10 + next);

        sum += clearTextSignal;
      }

      return sum.ToString();
    }

    private static bool IsSignalMatch(string signal, string pattern) =>
      signal.Length == pattern.Length && signal.All(pattern.Contains);
  }
}
