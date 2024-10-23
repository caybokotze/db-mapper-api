namespace Core.Api.Services;

public class DatabaseFactory
{
    private readonly MsSqlService _msSqlService;

    public DatabaseFactory(MsSqlService msSqlService)
    {
        _msSqlService = msSqlService;
    }
    
    public IDatabaseService Create(DatabaseType databaseType)
    {
        if (databaseType == DatabaseType.MsSql)
        {
            return _msSqlService;
        }
        
        if (databaseType == DatabaseType.MySql)
        {
            return _msSqlService;
        }

        throw new Exception("Nothin' be registered");
    }
}