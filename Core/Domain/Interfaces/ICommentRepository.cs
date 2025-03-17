using MongoDbExercise.Models;

namespace MongoDbExercise.Core.Domain.Interfaces;

public interface ICommentRepository
{
    Task<List<Comment>> GetCommentsAsync();
    Task<Comment?> GetCommentAsync(string id);
    Task CreateCommentAsync(Comment comment);
    Task UpdateCommentAsync(Comment comment);
    Task DeleteCommentAsync(string id);
}