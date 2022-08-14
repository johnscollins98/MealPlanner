using System.Linq;
using System.Security.Claims;

namespace MealPlanner
{
  public static class UserExtensions
  {
    public static string GetNameIdentifier(this ClaimsPrincipal user)
    {
      return user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
    }   
  }
}