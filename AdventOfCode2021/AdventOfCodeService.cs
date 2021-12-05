using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace AdventOfCode2021
{
  public interface IAdventOfCodeService
  {
    Task<ICollection<string>> GetPuzzleInputAsync(int day, CancellationToken ct = default);
    Task SubmitAnswerAsync(int day, int part, string answer, CancellationToken ct = default);
  }

  public class AdventOfCodeService : IAdventOfCodeService
  {
    private readonly ILogger<AdventOfCodeService> _logger;
    private readonly HttpClient _httpClient;

    public AdventOfCodeService(string sessionId, ILogger<AdventOfCodeService> logger)
    {
      var aocBaseUri = new Uri("https://adventofcode.com");
      var cookieContainer = new CookieContainer();
      cookieContainer.Add(aocBaseUri, new Cookie("session", sessionId));

      _logger = logger;
      _httpClient = new HttpClient(new HttpClientHandler { CookieContainer = cookieContainer })
      {
        BaseAddress = aocBaseUri
      };
    }

    public async Task<ICollection<string>> GetPuzzleInputAsync(int day, CancellationToken ct = default)
    {
      if (day is < 0 or > 24)
      {
        throw new ArgumentException($"Argument {nameof(day)} has to be within range 1 - 24");
      }

      var response = await _httpClient.GetAsync($"2021/day/{day}/input", ct);
      var contentAsString = await response.Content.ReadAsStringAsync(ct);

      if (!response.IsSuccessStatusCode)
      {
        throw new HttpRequestException($"Unable to retrieve puzzle input. Error message: '{contentAsString}'");
      }

      var puzzleInput = contentAsString.Split('\n').ToList();
      if (puzzleInput.Count > 0 && string.IsNullOrEmpty(puzzleInput[^1]))
      {
        puzzleInput.RemoveAt(puzzleInput.Count -1);
      }

      return puzzleInput;
    }

    public async Task SubmitAnswerAsync(int day, int part, string answer, CancellationToken ct = default)
    {
      using var content = new FormUrlEncodedContent(new Dictionary<string, string>
      {
        { "level", part.ToString() },
        { "answer", answer },
      });

      var response = await _httpClient.PostAsync($"2021/day/{day}/answer", content, ct);
      response.EnsureSuccessStatusCode();

      var responseContent = await response.Content.ReadAsStringAsync(ct);
      if (responseContent.Contains("That's the right answer!"))
      {
        _logger.LogInformation("Answer {answer} is correct for part {part} of day {day}.", answer, part, day);
      } else if (responseContent.Contains("That's not the right answer."))
      {
        _logger.LogWarning("Answer {answer} is not correct for part {part} of day {day}.", answer, part, day);
      }
      else if (responseContent.Contains("You don't seem to be solving the right level."))
      {
        _logger.LogWarning("Part {part} of day {day} is already answered.", part, day);
      }
      else if (responseContent.Contains("You gave an answer too recently"))
      {
        _logger.LogWarning("Answer for part {part} of day {day} has been throttled.", part, day);
      }
    }
  }
}
