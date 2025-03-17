using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDbExercise.Core.Configuration;
using MongoDbExercise.Models;

namespace MongoDbExercise.Infrastructure.Data;

public class MongoDbContext
{
    private readonly IOptions<MongoDbSettings> _mongoDbSettings;
    private readonly IMongoDatabase _database;

    public IMongoCollection<User> Users { get; }
    public IMongoCollection<Blog> Blogs { get; }
    public IMongoCollection<Post> Posts { get; }
    public IMongoCollection<Comment> Comments { get; }

    public MongoDbContext(IOptions<MongoDbSettings> mongoDbSettings)
    {
        _mongoDbSettings = mongoDbSettings;
        _database = new MongoClient(_mongoDbSettings.Value.ConnectionString).GetDatabase(_mongoDbSettings.Value.DatabaseName);
        
        // Register Collections
        Users = _database.GetCollection<User>("Users");
        Blogs = _database.GetCollection<Blog>("Blogs");
        Posts = _database.GetCollection<Post>("Posts");
        Comments = _database.GetCollection<Comment>("Comments");
    }
}