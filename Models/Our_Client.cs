using MongoDB.Bson;

namespace DotNet_6_REST_API_with_mongoDB.Models;

public class Our_Client
{
    public ObjectId Id { get; set; }

    public string Client_Name { get; set; }

    public string Client_email { get; set; }

    public string Client_Password { get; set; }

    public DateTime Client_Date_of_birth { get; set; }

}