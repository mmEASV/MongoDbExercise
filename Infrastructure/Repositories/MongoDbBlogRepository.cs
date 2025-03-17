using MongoDB.Driver;
using MongoDbExercise.Core.Domain.Interfaces;
using MongoDbExercise.Infrastructure.Data;
using MongoDbExercise.Models;

namespace MongoDbExercise.Infrastructure.Repositories;

public class MongoDbBlogRepository : IBlogRepository
{
    private readonly MongoDbContext _context;

    public MongoDbBlogRepository(MongoDbContext context)
    {
        _context = context;
    }

    public async Task<List<Blog>> GetBlogsAsync() =>
        await _context.Blogs.Find(_ => true).ToListAsync();
    public async Task<Blog?> GetBlogAsync(string id) =>
        await _context.Blogs.Find(b => b.Id == id).FirstOrDefaultAsync();
    public async Task CreateBlogAsync(Blog blog) =>
        await _context.Blogs.InsertOneAsync(blog);
    public async Task UpdateBlogAsync(Blog blog) =>
        await _context.Blogs.ReplaceOneAsync(b => b.Id == blog.Id, blog);
    public async Task DeleteBlogAsync(string id) =>
        await _context.Blogs.DeleteOneAsync(b => b.Id == id);
}