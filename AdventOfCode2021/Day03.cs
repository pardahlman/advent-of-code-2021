using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021
{
  public class Day03 : IProblem
  {
    public int Day => 3;

    public string SolvePartOne(ICollection<string> puzzleInput)
    {
      var bitsByPosition = new List<List<int>>();
      return puzzleInput.Aggregate(bitsByPosition, TransformPuzzleInput, MultiplyGammaAndEpsilonRate);
    }

    private static List<List<int>> TransformPuzzleInput(List<List<int>> bitByPosition, string input)
    {
      for (var i = 0; i < input.Length; i++)
      {
        if (bitByPosition.Count <= i)
        {
          bitByPosition.Add(new List<int>());
        }
        bitByPosition[i].Add(int.Parse(input[i].ToString()));
      }

      return bitByPosition;
    }

    private static string MultiplyGammaAndEpsilonRate(List<List<int>> bitByPosition)
    {
      var gammaBinary = string.Join(string.Empty, bitByPosition
        .Select(position => position
          .GroupBy(b => b)
          .OrderByDescending(g => g.Count())
          .Select(g => g.Key)
          .First())
        .ToArray());

      var epsilonBinary = string.Join(string.Empty, bitByPosition
        .Select(position => position
          .GroupBy(b => b)
          .OrderBy(g => g.Count())
          .Select(g => g.Key.ToString()[0])
          .First())
        .ToArray());


      var gammaDecimal = Convert.ToInt32(gammaBinary, fromBase: 2);
      var epsilonDecimal = Convert.ToInt32(epsilonBinary, fromBase: 2);
      return (gammaDecimal * epsilonDecimal).ToString();
    }

    public string SolvePartTwo(ICollection<string> puzzleInput)
    {
      var dataIsOfSameLength = puzzleInput.GroupBy(i => i.Length).Count() == 1;
      if (!dataIsOfSameLength)
      {
        throw new Exception("This approach only works with data of the same length.");
      }

      var oxygenGeneratorRating = FilterInput(puzzleInput, keepLargestGroup: true);
      var co2ScrubberRating = FilterInput(puzzleInput, keepLargestGroup: false);

      var oxygenGeneratorRatingDecimal = Convert.ToInt32(oxygenGeneratorRating, fromBase: 2);
      var co2ScrubberRatingDecimal = Convert.ToInt32(co2ScrubberRating, fromBase: 2);
      return (oxygenGeneratorRatingDecimal * co2ScrubberRatingDecimal).ToString();
    }

    private static string FilterInput(ICollection<string> puzzleInput, bool keepLargestGroup)
    {
      var remainingData = new List<string>(puzzleInput);
      for (var i = 0; i < int.MaxValue; i++)
      {
        if (remainingData.Count == 0)
        {
          throw new Exception("No data remaining, something went wrong :/");
        }

        if (remainingData.Count == 1)
        {
          return remainingData.First();
        }

        var groupedData = remainingData.GroupBy(d => d[i]).ToList();
        if (groupedData.Count == 1)
        {
          remainingData = groupedData.First().ToList();
          continue;
        }

        if (groupedData.Count != 2)
        {
          throw new Exception($"Expected two groups (0 and 1), got {groupedData.Count} groups.");
        }

        var matchingZeros = groupedData.First(g => g.Key == '0').ToList();
        var matchingOnes = groupedData.First(g => g.Key == '1').ToList();

        if (keepLargestGroup)
        {
          remainingData = matchingZeros.Count > matchingOnes.Count ? matchingZeros : matchingOnes;
        }
        else
        {
          remainingData = matchingZeros.Count > matchingOnes.Count ? matchingOnes : matchingZeros;
        }
      }

      throw new Exception("");
    }
  }
}
