using MongoDB.Bson;

namespace DotNet_6_REST_API_with_mongoDB.Models;

// todo: MongoDb Collection's field name & This model classes variable's name must be same
public class Our_Client
{
    public ObjectId Id { get; set; }

    public string name { get; set; }

    public string email { get; set; }

    public string password { get; set; }

    public DateTime date_of_birth { get; set; }

}