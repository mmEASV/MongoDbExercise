using MongoDB.Driver;
using MongoDbExercise.Core.Domain.Interfaces;
using MongoDbExercise.Infrastructure.Data;
using MongoDbExercise.Models;

namespace MongoDbExercise.Infrastructure.Repositories;

public class MongoDbPostRepository : IPostRepository
{
    private readonly MongoDbContext _context;

    public MongoDbPostRepository(MongoDbContext context)
    {
        _context = context;
    }

    public async Task<List<Post>> GetPostsAsync() =>
        await _context.Posts.Find(_ => true).ToListAsync();
    public async Task<Post?> GetPostAsync(string id) =>
        await _context.Posts.Find(p => p.Id == id).FirstOrDefaultAsync();
    public async Task CreatePostAsync(Post post) =>
        await _context.Posts.InsertOneAsync(post);
    public async Task UpdatePostAsync(Post post) =>
        await _context.Posts.ReplaceOneAsync(p => p.Id == post.Id, post);
    public async Task DeletePostAsync(string id) =>
        await _context.Posts.DeleteOneAsync(p => p.Id == id);
}