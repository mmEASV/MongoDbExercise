using Microsoft.Extensions.Options;
using MongoDbExercise.Core.Configuration;
using NRedisStack;
using NRedisStack.RedisStackCommands;
using NRedisStack.Search;
using NRedisStack.Search.Literals.Enums;
using StackExchange.Redis;

namespace MongoDbExercise.Infrastructure.Data;

public class RedisContext
{
    public readonly IDatabase Database;

    public RedisContext(IOptions<RedisDbSettings> redisOptions)
    {
        ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(redisOptions.Value.ConnectionString);
        Database = redis.GetDatabase();
    }
}