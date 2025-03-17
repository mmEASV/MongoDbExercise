using MongoDbExercise.Core.Domain.Exceptions;
using MongoDbExercise.Core.Domain.Interfaces;
using MongoDbExercise.Core.Services.Interfaces;
using MongoDbExercise.Models;

namespace MongoDbExercise.Core.Services;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;

    public PostService(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }
    
    public async Task<List<Post>> GetPosts() =>
        await _postRepository.GetPostsAsync();

    public async Task<Post?> GetPostById(string id) =>
        await _postRepository.GetPostAsync(id);

    public async Task<Post> CreatePost(Post post)
    { 
        await _postRepository.CreatePostAsync(post);

        return post;
    }
    
    public async Task<Post> UpdatePost(string id, Post post)
    {
        var postToUpdate = await _postRepository.GetPostAsync(id);
        if (postToUpdate is null)
            throw new NotFoundException();
        
        postToUpdate = post;
        await _postRepository.UpdatePostAsync(postToUpdate);

        return postToUpdate;
    }

    public async Task DeletePost(string id)
    {
        var post = await _postRepository.GetPostAsync(id);
        if (post is null)
            throw new NotFoundException();
        
        await _postRepository.DeletePostAsync(id);
    }
}