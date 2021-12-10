using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021
{
  public class Day09 : IProblem
  {
    public int Day => 9;

    private const int MaxHeight = 9;

    public string SolvePartOne(ICollection<string> puzzleInput)
    {
      var heightMap = CreateHeightMap(puzzleInput);
      var lowPoints = GetLowPoints(heightMap);
      return lowPoints.Sum(cell => cell.Height + 1).ToString();
    }

    public string SolvePartTwo(ICollection<string> puzzleInput)
    {
      var heightMap = CreateHeightMap(puzzleInput);
      var lowPoints = GetLowPoints(heightMap);
      var visitedPositions = new HashSet<Coordinate>();
      var basins = new List<List<Cell>>();

      foreach (var cell in lowPoints)
      {
        var basin = new List<Cell>();
        ExploreBasin(cell, basin, heightMap, visitedPositions);
        if (basin.Count > 0)
        {
          basins.Add(basin);
        }
      }

      return basins
        .Select(b => b.Count)
        .OrderByDescending(i => i)
        .Take(3)
        .Aggregate(1, (a, b) => a * b, total => total.ToString());
    }

    private static List<Cell> GetNeighbours(IReadOnlyList<List<Cell>> heightMap, Cell cell)
    {
      var neighbours = new List<Cell>();
      var ((x, y), _) = cell;
      if (y != 0)
      {
        neighbours.Add(heightMap[y - 1][x]);
      }

      if (y != heightMap.Count - 1)
      {
        neighbours.Add(heightMap[y+1][x]);
      }

      if (x != 0)
      {
        neighbours.Add(heightMap[y][x - 1]);
      }

      if (x != heightMap[y].Count - 1)
      {
        neighbours.Add(heightMap[y][x + 1]);
      }

      return neighbours;
    }

    private static List<List<Cell>> CreateHeightMap(IEnumerable<string> puzzleInput) =>
      puzzleInput
        .Select((row, y) => row.Select((c, x) => new Cell(new Coordinate(x, y), int.Parse(c.ToString()))).ToList())
        .ToList();

    private static ICollection<Cell> GetLowPoints(List<List<Cell>> heightMap)
    {
      return heightMap
        .SelectMany(row => row)
        .Where(cell => GetNeighbours(heightMap, cell).All(neighbour => neighbour.Height > cell.Height))
        .ToList();
    }

    private static void ExploreBasin(Cell currentCell, ICollection<Cell> basin, IReadOnlyList<List<Cell>> heightMap, ISet<Coordinate> visitedPositions)
    {
      if (currentCell.Height == MaxHeight)
      {
        return;
      }

      if (visitedPositions.Contains(currentCell.Coordinate))
      {
        return;
      }

      basin.Add(currentCell);
      visitedPositions.Add(currentCell.Coordinate);

      foreach (var neighbour in GetNeighbours(heightMap, currentCell))
      {
        ExploreBasin(neighbour, basin, heightMap, visitedPositions);
      }
    }

    private record Coordinate(int X, int Y);

    private record Cell(Coordinate Coordinate, int Height);
  }
}
