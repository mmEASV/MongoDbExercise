using MongoDbExercise.Core.Domain.Exceptions;
using MongoDbExercise.Core.Domain.Interfaces;
using MongoDbExercise.Core.Services.Interfaces;
using MongoDbExercise.Models;

namespace MongoDbExercise.Core.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IRedisCommentRepository _redisCommentRepository;

    public CommentService(ICommentRepository commentRepository, IRedisCommentRepository redisCommentRepository)
    {
        _commentRepository = commentRepository;
        _redisCommentRepository = redisCommentRepository;
    }

    public async Task<List<Comment>> GetComments()
    {
        return await _commentRepository.GetCommentsAsync();
    }

    public async Task<Comment?> GetCommentById(string id)
    {
        return await _commentRepository.GetCommentAsync(id);
    }

    public async Task<Comment> CreateComment(Comment comment)
    {
        var canComment = await _redisCommentRepository.IsCommentCountWithinLimitAsync(comment.UserId);

        if (!canComment)
        {
            throw new RateLimitException();
        }

        await _redisCommentRepository.IncrementCommentRate(comment.UserId);
        await _commentRepository.CreateCommentAsync(comment);

        return comment;
    }

    public async Task<Comment> UpdateComment(Comment comment)
    {
        await _commentRepository.UpdateCommentAsync(comment);

        return comment;
    }

    public async Task DeleteComment(string id)
    {
        await _commentRepository.DeleteCommentAsync(id);
    }
}