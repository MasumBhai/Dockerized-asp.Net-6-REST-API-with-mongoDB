using System.Text;
using DotNet_6_REST_API_with_mongoDB.Models;
using DotNet_6_REST_API_with_mongoDB.Service;
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
        // private readonly IConfiguration _configuration;
        private readonly MongoDBService _mongoDBService;
        // public ClientController(IConfiguration configuration)
        // {
        //     _configuration = configuration;
        // }

        public ClientController(MongoDBService mongoDBService)
        {
            _mongoDBService = mongoDBService;
        }

        [HttpGet]
        public async Task<List<Our_Clients>> Get()
        {
            return await _mongoDBService.GetAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Our_Clients client)
        {
            client.Id = ObjectId.GenerateNewId().ToString();
            client.name = RandomString(6);
            client.email = RandomString(8) + "@gmail.com";
            client.password = RandomString(8);
            client.date_of_birth = "1998-11-06";

            await _mongoDBService.CreateAsync(client);

            //return StatusCode(200);
            return CreatedAtAction(nameof(Get), new { id = client.Id }, client);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AddToPlaylist(string id, [FromBody] string client_pass)
        {
            await _mongoDBService.AddToClientsAsync(id, "MasumTheHero");
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _mongoDBService.DeleteAsync(id);
            return NoContent();
        }
        // [HttpGet]
        // public JsonResult Get()
        // {
        //     MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("dbConnectionURI"));
        //
        //     var dbList = dbClient.GetDatabase("dotnet_6_REST_API_mongoDB").GetCollection<Our_Clients>("Our_Clients").AsQueryable();
        //
        //     return new JsonResult(dbList);
        // }
        //
        // [HttpPost]
        // public JsonResult Post(Our_Clients client)
        // {
        //     MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("dbConnectionURI"));
        //
        //     int lastItemId = dbClient.GetDatabase("dotnet_6_REST_API_mongoDB").GetCollection<Our_Clients>("Our_Clients").AsQueryable().Count();
        //
        //     client.name = RandomString(5);
        //     client.email = RandomString(8) + "@gmail.com";
        //     client.password = RandomString(8);
        //     client.date_of_birth = "1998-11-06";
        //     client.Id = ObjectId.GenerateNewId().ToString();
        //
        //     dbClient.GetDatabase("dotnet_6_REST_API_mongoDB").GetCollection<Our_Clients>("Our_Clients").InsertOne(client);
        //
        //     return new JsonResult(client);
        // }
        //
        // [HttpPut]
        // public JsonResult Put(Our_Clients client)
        // {
        //     MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("dbConnectionURI"));
        //
        //     var filter = Builders<Our_Clients>.Filter.Eq("Id", client.Id);
        //
        //     var update = Builders<Our_Clients>.Update.Set("name", "Masum The Zero").Set("password", client.password).Set("date_of_birth", client.date_of_birth).Set("email", client.email);
        //
        //     dbClient.GetDatabase("dotnet_6_REST_API_mongoDB").GetCollection<Our_Clients>("Our_Clients")
        //         .UpdateOne(filter, update);
        //
        //     return new JsonResult("updated atlast");
        // }

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