namespace MongoDbExercise.Core.Domain.Exceptions;

public class RateLimitException(string error) : CustomException(error)
{
    public RateLimitException() : this("Rate limit exceeded.") { }
}