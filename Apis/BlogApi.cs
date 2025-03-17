using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MongoDbExercise.Core.Domain.Exceptions;
using MongoDbExercise.Core.Services.Interfaces;
using MongoDbExercise.Models;

namespace MongoDbExercise.Apis;

public static class BlogApi
{
    public static RouteGroupBuilder AddBlogApi(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("Blog");

        api.MapGet("/", GetBlogs)
            .WithName("Get Blogs");

        api.MapGet("/{id}", GetBlog)
            .WithName("Get Blog");

        api.MapPost("/", CreateBlog)
            .WithName("Create Blog");

        api.MapPut("/{id}", UpdateBlog)
            .WithName("Update Blog");

        api.MapDelete("/{id}", DeleteBlog)
            .WithName("Delete Blog");

        return api;
    }

    public static async Task<Results<Ok<List<Blog>>, ProblemHttpResult>> GetBlogs(IBlogService blogService)
    {
        var blogs = await blogService.GetBlogsAsync();

        return TypedResults.Ok(blogs);
    }

    public static async Task<Results<Ok<Blog>, NotFound, ProblemHttpResult>> GetBlog(IBlogService blogService, [FromRoute] string id)
    {
        var blog = await blogService.GetBlogAsync(id);

        if (blog is null)
        {
            throw new NotFoundException();
        }

        return TypedResults.Ok(blog);
    }

    public static async Task<Results<Created<Blog>, ProblemHttpResult>> CreateBlog(IBlogService blogService,
        [FromBody] Blog blog)
    {
        var outputBlog = await blogService.CreateBlogAsync(blog);

        return TypedResults.Created("", outputBlog);
    }

    public static async Task<Results<Ok<Blog>, NotFound, ProblemHttpResult>> UpdateBlog(IBlogService blogService, [FromRoute] string id,
        [FromBody] Blog blog)
    {
        var outputBlog = await blogService.UpdateBlogAsync(id, blog);
        
        return TypedResults.Ok(outputBlog);
    }

    public static async Task<Results<NoContent, NotFound, ProblemHttpResult>> DeleteBlog(IBlogService blogService,
        [FromRoute] string id)
    {
        await blogService.DeleteBlogAsync(id);

        return TypedResults.NoContent();
    }
}