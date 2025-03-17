using MongoDB.Driver;
using MongoDbExercise.Core.Domain.Interfaces;
using MongoDbExercise.Infrastructure.Data;
using MongoDbExercise.Models;

namespace MongoDbExercise.Infrastructure.Repositories;

public class MongoDbCommentRepository : ICommentRepository
{
    private readonly MongoDbContext _context;

    public MongoDbCommentRepository(MongoDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Comment>> GetCommentsAsync () =>
        await _context.Comments.Find(_ => true).ToListAsync();
    public async Task<Comment?> GetCommentAsync (string id) =>
        await _context.Comments.Find(c => c.Id == id).FirstOrDefaultAsync();
    public async Task CreateCommentAsync (Comment comment) =>
        await _context.Comments.InsertOneAsync(comment);
    public async Task UpdateCommentAsync (Comment comment) =>
        await _context.Comments.ReplaceOneAsync(c => c.Id == comment.Id, comment);
    public async Task DeleteCommentAsync (string id) =>
        await _context.Comments.DeleteOneAsync(c => c.Id == id);
}