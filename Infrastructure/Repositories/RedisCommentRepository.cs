using MongoDbExercise.Core.Domain.Interfaces;
using MongoDbExercise.Infrastructure.Data;
using StackExchange.Redis;

namespace MongoDbExercise.Infrastructure.Repositories;

public class RedisCommentRepository : IRedisCommentRepository
{
    private const int CommentLimit = 10;
    private readonly TimeSpan _ttl = TimeSpan.FromMinutes(1);
    private readonly IDatabase _daatabase;
    public RedisCommentRepository(RedisContext redisContext)
    {
        _daatabase = redisContext.Database;
    }


    public async Task<int> IncrementCommentRate(string userId)
    {
        var userKey = $"comment_rate:{userId}";
        var rate = await _daatabase.StringIncrementAsync(userKey);
        await _daatabase.KeyExpireAsync(userKey, _ttl);

        return (int)rate;
    }
    
    public async Task<bool> IsCommentCountWithinLimitAsync(string userId)
    {
        var userKey = $"comment_rate:{userId}";
        var commentCount = await _daatabase.StringGetAsync(userKey);

        if (!commentCount.HasValue)
        {
            return true;
        }

        return int.Parse(commentCount) < CommentLimit;
    }
}