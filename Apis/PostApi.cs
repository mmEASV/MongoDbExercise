using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MongoDbExercise.Core.Domain.Exceptions;
using MongoDbExercise.Core.Services.Interfaces;
using MongoDbExercise.Models;

namespace MongoDbExercise.Apis;

public static class PostApi
{
    public static RouteGroupBuilder AddPostApi(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("Post");

        api.MapGet("/", GetPosts)
            .WithName("Get Posts");

        api.MapGet("/{id}", GetPost)
            .WithName("Get Post");

        api.MapPost("/", CreatePost)
            .WithName("Create Post");

        api.MapPut("/{id}", UpdatePost)
            .WithName("Update Post");

        api.MapDelete("/{id}", DeletePost)
            .WithName("Delete Post");

        return api;
    }

    public static async Task<Results<Ok<List<Post>>, ProblemHttpResult>> GetPosts(IPostService postService)
    {
        var posts = await postService.GetPosts();

        return TypedResults.Ok(posts);
    }

    public static async Task<Results<Ok<Post>, NotFound, ProblemHttpResult>> GetPost(IPostService postService, [FromRoute] string id)
    {
        var post = await postService.GetPostById(id);

        if (post is null)
        {
            throw new NotFoundException();
        }

        return TypedResults.Ok(post);
    }

    public static async Task<Results<Created<Post>, ProblemHttpResult>> CreatePost(IPostService postService,
        [FromBody] Post post)
    {
        var outputPost = await postService.CreatePost(post);

        return TypedResults.Created("", outputPost);
    }

    public static async Task<Results<Ok<Post>, NotFound, ProblemHttpResult>> UpdatePost(IPostService postService, [FromRoute] string id,
        [FromBody] Post post)
    {
        var outputPost = await postService.UpdatePost(id, post);

        return TypedResults.Ok(outputPost);
    }

    public static async Task<Results<NoContent, NotFound, ProblemHttpResult>> DeletePost(IPostService postService,
        [FromRoute] string id)
    {
        await postService.DeletePost(id);

        return TypedResults.NoContent();
    }
}