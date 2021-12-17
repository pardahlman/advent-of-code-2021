using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2021
{
  public class Day17 : IProblem
  {
    public int Day => 17;

    public string SolvePartOne(ICollection<string> puzzleInput)
    {
      var match = Regex.Matches(puzzleInput.Single(), "-?\\d+").Select(m => int.Parse(m.Value)).ToList();

      var xMin = match[0];
      var xMax = match[1];
      var yMin = match[2];
      var yMax = match[3];

      // Assumption: the targets X position is always positive => The initial X is 0 or greater.
      const int minVelocityX = 0;

      // Initial X velocity can not be larger than xMax, as it would result in a position to the right of the target
      // after the first step.
      var maxVelocityX = xMax;

      // Initial Y velocity can not be smaller than yMin, as it would result in a position below the target after the
      // first step.
      var minVelocityY = yMin;

      // The velocity decreases according to a arithmetic series, like: 7 6 5 4 3 2 1 0
      // After which it starts to accumulate negative speed: -1 -2 -3 -4 -5 -6 -7
      // This means that the downward velocity in any given Y is equal to the upward velocity (for example right before
      // the probe starts to fall at y, its positive velocity is 1 so it reaches y + 1 with a velocity of 0, then falls
      // back to y with a velocity of -1 and so on.
      // The target appears to be below (0,0), which means that the initial positive velocity can not be larger than
      // yMin, as it would make the next step overshoot the target.
      var maxVelocityY = -yMin;

      var matchingTrajectories = new List<List<(int x, int y)>>();

      for (var velocityX = minVelocityX; velocityX < maxVelocityX; velocityX++)
      {
        for (var velocityY = minVelocityY; velocityY < maxVelocityY; velocityY++)
        {
          var trajectory = GetTrajectory((0, 0), (velocityX, velocityY))
            .TakeWhile(position => position.x <= xMax && position.y >= yMin)
            .ToList();
          var hitsTarget = trajectory.Any(position =>
            xMin <= position.x && position.x <= xMax && yMin <= position.y && position.y <= yMax);
          if (hitsTarget)
          {
            matchingTrajectories.Add(trajectory);
          }
        }
      }

      return matchingTrajectories.Max(t => t.Max(p => p.y)).ToString();
    }

    private static IEnumerable<(int x, int y)> GetTrajectory((int x, int y) position, (int vx, int vy) velocity)
    {
      var xDrag = velocity.vx == 0 ? 0 : velocity.vx/velocity.vx;

      while (true)
      {
        yield return position;
        position.x += velocity.vx;
        position.y += velocity.vy;
        velocity.vx = velocity.vx == 0 ? 0 : velocity.vx - xDrag;
        velocity.vy -= 1;
      }
    }

    public string SolvePartTwo(ICollection<string> puzzleInput)
    {
      throw new NotImplementedException();
    }
  }
}
