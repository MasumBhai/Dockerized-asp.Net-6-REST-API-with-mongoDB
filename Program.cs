using DotNet_6_REST_API_with_mongoDB.Models;
using DotNet_6_REST_API_with_mongoDB.Service;
using Newtonsoft.Json.Serialization;

await Bootstrapper
    .Factory
    .CreateDocs(args)
    .AddSourceFiles("{!.git,!.idea,!.vs,!bin,!obj,!packages,!*.Tests,}/**/*.cs")
    .RunAsync();

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigin = "_myAllowSpecificOrigins";

// Add services to the container.
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<MongoDBService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Building specific origin supported Cors (change origin here before deploying)
builder.Services.AddCors(c =>
{
    c.AddPolicy(MyAllowSpecificOrigin, policy => policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddControllersWithViews().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
.AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

builder.Services.AddHealthChecks();
var app = builder.Build();
app.MapHealthChecks("/health");

// Enabling Cors
app.UseCors(MyAllowSpecificOrigin);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();