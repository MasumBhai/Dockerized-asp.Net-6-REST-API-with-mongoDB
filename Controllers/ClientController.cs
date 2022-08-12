using DotNet_6_REST_API_with_mongoDB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

            var dbList = dbClient.GetDatabase("dotnet_6_REST_API_mongoDB").GetCollection<Our_Client>("Our_Clients").AsQueryable();

            return new JsonResult(dbList);
        }
    }
}
