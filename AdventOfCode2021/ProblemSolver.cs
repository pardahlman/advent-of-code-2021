using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace AdventOfCode2021
{
  public interface IProblemSolver
  {
    Task SolveAsync(ICollection<IProblem> problems, CancellationToken ct = default);
  }

  public class ProblemSolver : IProblemSolver
  {
    private readonly IAdventOfCodeService _adventOfCodeService;
    private readonly ILogger<ProblemSolver> _logger;

    public ProblemSolver(IAdventOfCodeService adventOfCodeService, ILogger<ProblemSolver> logger)
    {
      _adventOfCodeService = adventOfCodeService;
      _logger = logger;
    }

    public async Task SolveAsync(ICollection<IProblem> problems, CancellationToken ct = default)
    {
      _logger.LogInformation("Preparing to solve {problemCount} problems", problems.Count);

      foreach (var problem in problems)
      {
        var input = await _adventOfCodeService.GetPuzzleInputAsync(problem.Day, ct);

        try
        {
          var firstSolution = problem.SolvePartOne(input);
          await _adventOfCodeService.SubmitAnswerAsync(problem.Day, 1, firstSolution, ct);
        }
        catch (Exception e)
        {
          _logger.LogError(e, "Part 1 of {day} throw an unhandled exception.", problem.Day);
        }

        try
        {
          var secondSolution = problem.SolvePartTwo(input);
          await _adventOfCodeService.SubmitAnswerAsync(problem.Day, 2, secondSolution, ct);
        }
        catch (Exception e)
        {
          _logger.LogError(e, "Part 2 of {day} throw an unhandled exception.", problem.Day);
        }
      }
    }
  }
}
