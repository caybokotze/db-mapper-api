using System.Transactions;
using Core.Api.Enum;
using Core.Api.Interfaces;
using Dapper;
using IsolationLevel = System.Data.IsolationLevel;

namespace Core.Api.Services;

public class DatabaseMapper : IDbQueryMapper
{
    private readonly DatabaseConnectionFactory _databaseConnectionFactory;

    public DatabaseMapper(DatabaseConnectionFactory databaseConnectionFactory)
    {
        _databaseConnectionFactory = databaseConnectionFactory;
    }

    public List<Dictionary<string, object>> ExecuteQuery(Dictionary<string, string> mappings, string query, DatabaseType databaseType)
    {
        using var connection = _databaseConnectionFactory.Create(databaseType);
        using var transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);

        var result = connection.Query(query);
        var outputRows = new List<Dictionary<string, object>>();

        foreach (var row in result)
        {
            var outputRow = new Dictionary<string, object>();

            foreach (KeyValuePair<string, object> column in row)
            {
                if (mappings.TryGetValue(column.Key, out var columnTitle))
                {
                    var columnValue = column.Value;

                    if (columnTitle.Contains(':'))
                    {
                        var columnTitleParts = columnTitle.Split(':');

                        if (columnTitleParts.Length > 2)
                        {
                            throw new Exception("Multidimensional object nesting is not supported because I am too lazy to make this recursive. You can only have one cookie. Now be a good boy and eat it.");
                        }

                        Dictionary<string, object> obj = new()
                        {
                            {columnTitleParts[1], columnValue}
                        };

                        if (outputRow.TryGetValue(columnTitleParts[0], out var existingObj))
                        {
                            if (existingObj is Dictionary<string, object> existingDict)
                            {
                                existingDict.TryAdd(columnTitleParts[1], columnValue);
                            }
                        }
                        else
                        {
                            outputRow.Add(columnTitleParts[0], obj);
                        }

                        continue;

                        // dynamic obj = new { };
                        // obj[columnTitleParts[0]] = new
                        // {
                        //     columnTitleParts[1] = columnValue
                        // };
                    }

                    outputRow.Add(columnTitle, columnValue);
                }
            }

            outputRows.Add(outputRow);
        }

        return outputRows;
    }
}