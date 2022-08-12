using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DotNet_6_REST_API_with_mongoDB.Models;

// todo: MongoDb Collection's field name & This model classes variable's name must be same
public class Our_Clients
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string name { get; set; }

    public string email { get; set; }

    public string password { get; set; }

    public string date_of_birth { get; set; }


}