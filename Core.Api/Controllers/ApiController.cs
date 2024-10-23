using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using Core.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Core.Api.Controllers;

[Route("/api")]
[ApiController]
public class ApiController
{
    private readonly DatabaseFactory _databaseFactory;

    public ApiController(DatabaseFactory databaseFactory)
    {
        _databaseFactory = databaseFactory;
    }
    
    [HttpPost]
    [Route("db-proxy")]
    public string GetDatabaseData(PostObject postObject)
    {
        var db = _databaseFactory.Create(DatabaseType.MySql);

        var results = db
            .ExecuteQuery(postObject.ColumnMappings, postObject.Query, postObject.DatabaseType);

        return JsonSerializer.Serialize(results,
            new JsonSerializerOptions
            {
                WriteIndented = true
            });
    }
    
}

public class PostObject
{
    [Required] public string Query { get; set; } = string.Empty;
    public Dictionary<string, string> ColumnMappings { get; set; } = new();
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public DatabaseType DatabaseType { get; set; }
}
