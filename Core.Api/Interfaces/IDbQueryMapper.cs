using Core.Api.Enum;
using Core.Api.Services;

namespace Core.Api.Interfaces;

public interface IDbQueryMapper
{
    List<Dictionary<string, object>> ExecuteQuery(Dictionary<string, string> mappings, string query, DatabaseType databaseType);
}