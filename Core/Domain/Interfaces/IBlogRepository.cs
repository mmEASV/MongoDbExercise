using MongoDbExercise.Models;

namespace MongoDbExercise.Core.Domain.Interfaces;

public interface IBlogRepository
{
    Task<List<Blog>> GetBlogsAsync();
    Task<Blog?> GetBlogAsync(string id);
    Task CreateBlogAsync(Blog blog);
    Task UpdateBlogAsync(Blog blog);
    Task DeleteBlogAsync(string id);
}