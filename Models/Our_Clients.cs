using System.Text.Json.Serialization;
using CommandLine;
using Microsoft.Build.Framework;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Xunit.Sdk;

namespace DotNet_6_REST_API_with_mongoDB.Models;

// todo: MongoDb Collection's field name & This model classes variable's name must be same
public class Our_Clients
{

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    // [BsonElement("client_name")] // within our C# application, in MongoDB, the field will be known as "client_name"
    //[JsonPropertyName("client_name")] // when sending or receiving JSON, the field will also be known as "client_name" instead of movieIds
    public string name { get; set; }
    [Required]
    public string email { get; set; } = null!;
    [Required]
    //[BsonRepresentation(BsonType.String)]
    public string password { get; set; } = null!;

    public string date_of_birth { get; set; } = null!;


}