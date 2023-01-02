using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MindScratcher.Models;

public record Card
{
    [BsonId] public string Id { get; } = ObjectId.GenerateNewId().ToString();

    [Required] public string FolderId { get; set; }
    
    [Required] public string Language { get; set; }
    [Required] public string Word { get; set; }

    [Required] public string Description { get; set; }
}