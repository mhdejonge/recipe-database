namespace Domain;

using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

public class Recipe
{
    [BsonId]
    [JsonRequired]
    public required string Name { get; set; }

    [BsonIgnoreIfNull]
    public List<string>? Ingredients { get; set; }

    [BsonIgnoreIfNull]
    public string? Instructions { get; set; }

    [BsonIgnoreIfNull]
    public List<string>? Tags { get; set; }

    [BsonIgnoreIfNull]
    public string? Source { get; set; }
}
