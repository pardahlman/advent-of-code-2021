using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace AdventOfCode2021
{
  public class Day15 : IProblem
  {
    public int Day => 15;

    public string SolvePartOne(ICollection<string> puzzleInput)
    {
      var cave = ParsePuzzleInput(puzzleInput);
      var start = (0, 0);
      var destination = (cave.Keys.Max(p => p.x), cave.Keys.Max(p => p.y));

      return CalculateRisk(cave, start, destination).ToString();
    }

    public string SolvePartTwo(ICollection<string> puzzleInput)
    {
      var originalCave = ParsePuzzleInput(puzzleInput);
      var cave = ExpandCave(originalCave);
      var start = (0, 0);
      var destination = (cave.Keys.Max(p => p.x), cave.Keys.Max(p => p.y));

      return CalculateRisk(cave, start, destination).ToString();
    }

    private static int CalculateRisk(IImmutableDictionary<(int, int), int> cave,
      (int, int) start, (int, int) destination)
    {
      // https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm

      // 1. Mark all nodes unvisited. Create a set of all the unvisited nodes called the unvisited set.
      var unvisitedSet = cave.Select(c => c.Key).ToHashSet();

      // 2. Assign to every node a tentative distance value: set it to zero for our initial node and to infinity for all
      //    other nodes. The tentative distance of a node v is the length of the shortest path discovered so far between
      //    the node v and the starting node. Since initially no path is known to any other vertex than the source
      //    itself (which is a path of length zero), all other tentative distances are initially set to infinity. Set the
      //    initial node as current.
      var nodeDistance = cave.ToDictionary(kvp => kvp.Key, kvp => kvp.Key == start ? 0 : int.MaxValue);

      var priorityQueue = new PriorityQueue<(int, int), int>();
      priorityQueue.Enqueue(start, 0);
      while (priorityQueue.TryDequeue(out var current, out _))
      {
        // 3. For the current node, consider all of its unvisited neighbors and calculate their tentative distances through
        //    the current node. Compare the newly calculated tentative distance to the current assigned value and assign
        //    the smaller one. For example, if the current node A is marked with a distance of 6, and the edge connecting
        //    it with a neighbor B has length 2, then the distance to B through A will be 6 + 2 = 8. If B was previously
        //    marked with a distance greater than 8 then change it to 8. Otherwise, the current value will be kept.
        var unvisitedNeighbors = GetNeighbours(current).Where(n => cave.ContainsKey(n) && unvisitedSet.Contains(n));
        foreach (var neighbor in unvisitedNeighbors)
        {
          var tentativeDistance = nodeDistance[current] + cave[neighbor];
          if (tentativeDistance < nodeDistance[neighbor])
          {
            nodeDistance[neighbor] = tentativeDistance;
            priorityQueue.Enqueue(neighbor, tentativeDistance);
          }
        }

        // 4. When we are done considering all of the unvisited neighbors of the current node, mark the current node as
        //    visited and remove it from the unvisited set. A visited node will never be checked again.
        unvisitedSet.Remove(current);

        // 5. If the destination node has been marked visited (when planning a route between two specific nodes) or if the
        //    smallest tentative distance among the nodes in the unvisited set is infinity (when planning a complete
        //    traversal; occurs when there is no connection between the initial node and remaining unvisited nodes), then
        //    stop. The algorithm has finished.
        if (current == destination)
        {
          return nodeDistance[current];
        }

        // 6. Otherwise, select the unvisited node that is marked with the smallest tentative distance, set it as the new
        //   current node, and go back to step 3.
      }

      return int.MaxValue;
    }

    private static IEnumerable<(int, int)> GetNeighbours((int, int) position)
    {
      var (x, y) = position;
      yield return (x - 1, y);
      yield return (x + 1, y);
      yield return (x, y - 1);
      yield return (x, y + 1);
    }

    private static IImmutableDictionary<(int x, int y), int> ParsePuzzleInput(IEnumerable<string> puzzleInput)
    {
      return puzzleInput
        .SelectMany((row, y) => row
          .Select((risk, x) => ((x, y), int.Parse(risk.ToString()))))
        .ToImmutableDictionary(pair => pair.Item1, pair => pair.Item2);
    }

    private static IImmutableDictionary<(int x, int y), int> ExpandCave(IReadOnlyDictionary<(int x, int y), int> cave)
    {
      var expandedCave = new Dictionary<(int, int), int>();
      var caveWidth = cave.Keys.Max(k => k.y) + 1;
      var caveHeight = cave.Keys.Max(k => k.x) + 1;

      foreach (var ((x, y), risk) in cave)
      {
        for (var verticalRepeat = 0; verticalRepeat < 5; verticalRepeat++)
        {
          for (var horizontalRepeat = 0; horizontalRepeat < 5; horizontalRepeat++)
          {
            expandedCave[(x + horizontalRepeat * caveWidth, y + verticalRepeat * caveHeight)] = ((risk - 1 + verticalRepeat + horizontalRepeat) % 9) + 1;
          }
        }
      }

      return expandedCave.ToImmutableDictionary();
    }
  }
}
