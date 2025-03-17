using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDbExercise.Models;

public class Comment
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string Value { get; set; }
    public string PostId { get; set; }
    public string UserId { get; set; }
}