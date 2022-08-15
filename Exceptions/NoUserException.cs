using System.Runtime.Serialization;

namespace MealPlanner;

public class NoUserException : Exception
{
  public NoUserException()
  {
  }

  public NoUserException(string? message) : base(message)
  {
  }

  public NoUserException(string? message, Exception? innerException) : base(message, innerException)
  {
  }

  protected NoUserException(SerializationInfo info, StreamingContext context) : base(info, context)
  {
  }
}