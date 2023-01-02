using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MindScratcher.Models;

public record Folder
{
    [BsonId] public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
    public string FolderName { get; set; }
    public string Language { get; set; }
}