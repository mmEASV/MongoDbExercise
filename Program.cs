using MongoDbExercise.Apis;
using MongoDbExercise.Application.Handlers;
using MongoDbExercise.Core.Configuration;
using MongoDbExercise.Core.Domain.Interfaces;
using MongoDbExercise.Core.Services;
using MongoDbExercise.Core.Services.Interfaces;
using MongoDbExercise.Infrastructure.Data;
using MongoDbExercise.Infrastructure.Repositories;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Global Exception Handling
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

// Add Settings
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDb"));

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Register Data Context
builder.Services.AddScoped<MongoDbContext>();

// Register Repositories
builder.Services.AddScoped<IBlogRepository, MongoDbBlogRepository>();
builder.Services.AddScoped<IPostRepository, MongoDbPostRepository>();
builder.Services.AddScoped<ICommentRepository, MongoDbCommentRepository>();
builder.Services.AddScoped<IUserRepository, MongoDbUserRepository>();

// Register Services
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<IPostService, PostService>();

var app = builder.Build();

// Global Exception Handling
app.UseExceptionHandler();

// Map APIs
app.AddBlogApi();
app.AddPostApi();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.Run();