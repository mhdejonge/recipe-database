using System.Text.Json.Serialization;

namespace Domain;

using MongoDB.Bson.Serialization.Attributes;

public class Recipe
{
    [BsonId]
    [JsonRequired]
    public required string Name { get; set; }

    [BsonIgnoreIfNull]
    public required string? Ingredients { get; set; }

    [BsonIgnoreIfNull]
    public required string? Instructions { get; set; }

    [BsonIgnoreIfNull]
    public List<string>? Tags { get; set; }

    [BsonRequired]
    [JsonRequired]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required MealType MealType { get; set; }

    [BsonIgnoreIfNull]
    public string? Source { get; set; }
}
