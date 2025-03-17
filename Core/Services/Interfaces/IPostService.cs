using MongoDbExercise.Models;

namespace MongoDbExercise.Core.Services.Interfaces;

public interface IPostService
{
    Task<List<Post>> GetPosts();
    Task<Post?> GetPostById(string id);
    Task<Post> CreatePost(Post post);
    Task<Post> UpdatePost(string id, Post post);
    Task DeletePost(string id);
}