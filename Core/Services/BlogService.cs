using MongoDbExercise.Core.Domain.Exceptions;
using MongoDbExercise.Core.Domain.Interfaces;
using MongoDbExercise.Core.Services.Interfaces;
using MongoDbExercise.Models;

namespace MongoDbExercise.Core.Services;

public class BlogService : IBlogService
{
    private readonly IBlogRepository _blogRepository;

    public BlogService(IBlogRepository blogRepository)
    {
        _blogRepository = blogRepository;
    }

    public async Task<List<Blog>> GetBlogsAsync() =>
        await _blogRepository.GetBlogsAsync();

    public async Task<Blog?> GetBlogAsync(string id) =>
        await _blogRepository.GetBlogAsync(id);

    public async Task<Blog> CreateBlogAsync(Blog blog)
    {
        await _blogRepository.CreateBlogAsync(blog);

        return blog;
    }

    public async Task<Blog> UpdateBlogAsync(string id, Blog blog)
    {
        var blogToUpdate = await _blogRepository.GetBlogAsync(id);
        if (blogToUpdate is null)
        {
            throw new NotFoundException("Blog not found");
        }

        blogToUpdate = blog;
        await _blogRepository.UpdateBlogAsync(blogToUpdate);

        return blogToUpdate;
    }

    public async Task DeleteBlogAsync(string id)
    {
        var blog = await _blogRepository.GetBlogAsync(id);
        if (blog is null)
        {
            throw new NotFoundException("Blog not found");
        }

        await _blogRepository.DeleteBlogAsync(id);
    }
}