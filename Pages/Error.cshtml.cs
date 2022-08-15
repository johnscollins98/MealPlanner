using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace MealPlanner;
[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
public class ErrorModel : PageModel
{
  public string RequestId { get; set; } = String.Empty;

  public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

  private readonly ILogger<ErrorModel> _logger;

  public ErrorModel(ILogger<ErrorModel> logger)
  {
    _logger = logger;
  }

  public void OnGet()
  {
    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
  }
}

