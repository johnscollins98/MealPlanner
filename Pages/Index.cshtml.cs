using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MealPlanner;
[Authorize]
public class IndexModel : PageModel
{
  public string Name { get; set; } = String.Empty;

  public IndexModel()
  {
  }

  public IActionResult OnGet()
  {
    if (User.Identity?.Name == null) 
    { 
      throw new NoUserException();
    }

    Name = User.Identity.Name.Split(' ')[0];

    return Page();
  }
}
