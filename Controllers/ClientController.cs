using System.Text;
using DotNet_6_REST_API_with_mongoDB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DotNet_6_REST_API_with_mongoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ClientController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("dbConnectionURI"));

            var dbList = dbClient.GetDatabase("dotnet_6_REST_API_mongoDB").GetCollection<Our_Clients>("Our_Clients").AsQueryable();

            return new JsonResult(dbList);
        }

        [HttpPost]
        public JsonResult Post(Our_Clients client)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("dbConnectionURI"));

            int lastItemId = dbClient.GetDatabase("dotnet_6_REST_API_mongoDB").GetCollection<Our_Clients>("Our_Clients").AsQueryable().Count();

            client.name = RandomString(5);
            client.email = RandomString(8) + "@gmail.com";
            client.password = RandomString(8);
            client.date_of_birth = "1998-11-06";
            client.Id = ObjectId.GenerateNewId().ToString();

            dbClient.GetDatabase("dotnet_6_REST_API_mongoDB").GetCollection<Our_Clients>("Our_Clients").InsertOne(client);

            return new JsonResult("added");
        }

        private string RandomString(int length)
        {
            const string src = "abcdefghijklmnopqrstuvwxyz0123456789";
            var sb = new StringBuilder();
            Random random_number = new Random();
            for (var i = 0; i < length; i++)
            {
                var c = src[random_number.Next(0, src.Length)];
                sb.Append(c);
            }

            return sb.ToString();
        }
    }
}
