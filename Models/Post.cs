using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDbExercise.Models;

public class Post
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string Name { get; set; }
    public string Content { get; set; }
    public string BlogId { get; set; }
    public string UserId { get; set; }
}