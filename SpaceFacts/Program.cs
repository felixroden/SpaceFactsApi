using System.Text.Json;
using Microsoft.AspNetCore.Http.Json;
using SpaceFacts.Dtos;
using SpaceFacts.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var factsFilePath = Path.Combine(AppContext.BaseDirectory, "facts.json");

if (!File.Exists(factsFilePath))
{
    throw new FileNotFoundException("facts.json not found", factsFilePath);
}

var facts = JsonSerializer.Deserialize<List<FactDto>>(File.ReadAllText(factsFilePath))
            ?? [];

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.WriteIndented = true;
});

var app = builder.Build();

app.UseCors();

app.MapFactsEndpoints(facts)
   .WithTags("Space Facts");

app.Run();
