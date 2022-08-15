using System.Runtime.Serialization;

namespace MealPlanner;

public class NoNameIdentifierException : Exception
{
  public NoNameIdentifierException()
  {
  }

  public NoNameIdentifierException(string? message) : base(message)
  {
  }

  public NoNameIdentifierException(string? message, Exception? innerException) : base(message, innerException)
  {
  }

  protected NoNameIdentifierException(SerializationInfo info, StreamingContext context) : base(info, context)
  {
  }
}