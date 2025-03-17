using MongoDbExercise.Models;

namespace MongoDbExercise.Core.Domain.Interfaces;

public interface IPostRepository
{
    Task<List<Post>> GetPostsAsync();
    Task<Post?> GetPostAsync(string id);
    Task CreatePostAsync(Post post);
    Task UpdatePostAsync(Post post);
    Task DeletePostAsync(string id);
}