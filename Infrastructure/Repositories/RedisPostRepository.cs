using System.Text.Json;
using MongoDbExercise.Core.Domain.Interfaces;
using MongoDbExercise.Infrastructure.Data;
using MongoDbExercise.Models;

namespace MongoDbExercise.Infrastructure.Repositories;

public class RedisPostRepository : IRedisPostRepository
{
    private readonly RedisContext _redisContext;

    public RedisPostRepository(RedisContext redisContext)
    {
        _redisContext = redisContext;
    }

    public Task<List<Post>> GetPostsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Post?> GetPostAsync(string id)
    {
        var post = await _redisContext.Database.StringGetAsync(id);

        return post.HasValue ? JsonSerializer.Deserialize<Post>(post) : null;
    }

    public async Task CreatePostAsync(Post post)
    {
        var serializedPost = JsonSerializer.Serialize(post);

        await _redisContext.Database.StringSetAsync(post.Id, serializedPost);
    }

    public Task UpdatePostAsync(Post post)
    {
        throw new NotImplementedException();
    }

    public async Task DeletePostAsync(string id)
    {
        await _redisContext.Database.KeyDeleteAsync(id);
    }
}