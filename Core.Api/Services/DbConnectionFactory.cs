using System.Data;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace Core.Api.Services;

public enum DatabaseType
{
   MsSql,
   MySql
}

public class DbConnectionFactory
{
   public IDbConnection Create(DatabaseType type)
   {
      if (type == DatabaseType.MsSql)
      {
         return new SqlConnection("");
      }

      if (type == DatabaseType.MySql)
      {
         return new MySqlConnection("Server=localhost;Database=dispatch_db;Uid=sqltracking;Pwd=SqlTracking#1234;Allow User Variables=true;");
      }

      return new SqlConnection("");
   } 
}