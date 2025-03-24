namespace MongoDbExercise.Core.Domain.Interfaces;

public interface IRedisCommentRepository
{
    Task<int> IncrementCommentRate(string userId);
    Task<bool> IsCommentCountWithinLimitAsync(string userId);
}