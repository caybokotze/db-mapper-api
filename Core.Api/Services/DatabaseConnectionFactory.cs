using System.Data;
using Core.Api.Configuration;
using Core.Api.Enum;
using Core.Api.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;

namespace Core.Api.Services;

public class DatabaseConnectionFactory
{
    private readonly IOptions<Database> _dbOptions;

    public DatabaseConnectionFactory(IOptions<Database> dbOptions)
    {
        _dbOptions = dbOptions;
    }
    
    public IDbConnection Create(DatabaseType databaseType)
    {
        return databaseType switch
        {
            DatabaseType.MsSql => new SqlConnection(_dbOptions.Value.ConnectionString),
            DatabaseType.MySql => new MySqlConnection(_dbOptions.Value.ConnectionString),
            _ => throw new Exception("Nothin' be registered")
        };
    }
}