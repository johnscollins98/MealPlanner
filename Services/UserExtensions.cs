using System.Security.Claims;

namespace MealPlanner;

public static class UserExtensions
{
  public static string GetNameIdentifier(this ClaimsPrincipal user)
  {
    var id = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

    if (id == null)
    {
      throw new NoNameIdentifierException();
    }

    return id.Value;
  }
}