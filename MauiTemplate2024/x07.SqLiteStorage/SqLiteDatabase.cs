using System.Diagnostics;
using JanZeman.Testing;

namespace MauiTemplate2024.SqLiteStorage;

/// Better check here: "http://codetraveler.io/2019/11/26/efficiently-initializing-sqlite-database/
public class SqLiteDatabase : SQLiteAsyncConnection
{
    private const SQLiteOpenFlags OpenFlags =
        // open the database in read/write mode
        SQLiteOpenFlags.ReadWrite |
        // create the database if it doesn't exist
        SQLiteOpenFlags.Create |
        // enable multi-threaded database access
        SQLiteOpenFlags.SharedCache;

    public string DatabaseFilePath { get; }

    private SqLiteDatabase(string databasePath) : base(databasePath, OpenFlags)
    {
#if DEBUG
        Tracer = message => Debug.WriteLine(message);
        Trace = true;
#endif
        DatabaseFilePath = databasePath;
    }

    public SqLiteDatabase(string databaseName, bool useAppDataDirectory = true)
        : this(GetDatabasePath(databaseName, useAppDataDirectory))
    {
    }

    private static string GetDatabasePath(string databaseName, bool useAppDataDirectory = true) => 
        UnitTestDetector.IsRunningXUnit ? databaseName : Path.Combine(useAppDataDirectory ? FileSystem.AppDataDirectory : FileSystem.CacheDirectory, databaseName);

    public Task InitAsync()
    {
        return SchemaEntry.CreateDatabase(this);
    }

    public async Task CloseAndDeleteAsync()
    {
        await CloseAsync();
        File.Delete(DatabaseFilePath);
    }
}