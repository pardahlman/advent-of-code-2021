using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace AdventOfCode2021
{
  internal static class Program
  {
    private static async Task Main(string[] args)
    {
      var sessionId = Environment.GetEnvironmentVariable("AOC2021_SESSIONID", EnvironmentVariableTarget.User);
      if (sessionId == default)
      {
        throw new Exception(
          "The Advent of code session ID is not found. Set the environment variable AOC2021_SESSIONID");
      }
      var cts = new CancellationTokenSource();
      Console.CancelKeyPress += (_, e) =>
      {
        cts.Cancel();
        e.Cancel = true;
      };

      using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
      var adventOfCodeService = new AdventOfCodeService(sessionId, loggerFactory.CreateLogger<AdventOfCodeService>());
      var problemSolver = new ProblemSolver(adventOfCodeService, loggerFactory.CreateLogger<ProblemSolver>());
      await problemSolver.SolveAsync(new List<IProblem> { new Day07() }, cts.Token);
    }
  }
}
