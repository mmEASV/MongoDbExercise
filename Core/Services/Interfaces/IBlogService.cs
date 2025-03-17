using MongoDbExercise.Models;

namespace MongoDbExercise.Core.Services.Interfaces;

public interface IBlogService
{
    Task<List<Blog>> GetBlogsAsync();
    Task<Blog?> GetBlogAsync(string id);
    Task<Blog> CreateBlogAsync(Blog blog);
    Task<Blog> UpdateBlogAsync(string id, Blog blog);
    Task DeleteBlogAsync(string id);
}