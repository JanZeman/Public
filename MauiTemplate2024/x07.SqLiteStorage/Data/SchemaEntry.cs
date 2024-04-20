namespace MauiTemplate2024.SqLiteStorage.Data;

[Table("Schema")]
public class SchemaEntry
{
    //Updated this to introduce schema changes
    public const int LatestDatabaseVersion = 1;
    private bool UpgradeRequired => Version < LatestDatabaseVersion;

    public const string Potatoes = "Potatoes";

    [NotNull]
    public int Version { get; set; }

    public static async Task CreateDatabase(SQLiteAsyncConnection db)
    {
        try
        {
            await db.CreateTableAsync<SchemaEntry>();
            await db.CreateTableAsync<PotatoEntry>();

            var schema = await db.Table<SchemaEntry>().FirstOrDefaultAsync();
            if (schema == null)
            {
                await db.InsertAsync(new SchemaEntry { Version = LatestDatabaseVersion });
                return;
            }

            if (!schema.UpgradeRequired) return;

            var from = schema.Version;
            var updatedTo = await UpdateDatabaseAsync(db, from);
            if (updatedTo <= from) return;
            schema.Version = updatedTo;
            await db.UpdateAsync(schema);
        }
        catch (Exception ex)
        {
            throw new DatabaseCreationException("Database creation / update failed.", ex);
        }
    }

    private static async Task<int> UpdateDatabaseAsync(SQLiteAsyncConnection db, int from)
    {
        ////NotifyUser(true);

        ////var updateNotificationTimer = new BackgroundTimer { Interval = new TimeSpan(0, 0, 8) };
        ////updateNotificationTimer.Tick += (sender, args) => { NotifyUser(); };
        ////updateNotificationTimer.Start();

        var version = from;

        try
        {
            if (version == 1)
                version = await UpdateFrom1To2Async(db, version);

            //...
        }
        catch (Exception ex)
        {
            HandleUpdateException(ex, "UpdateDatabase failed");
        }
        finally
        {
            ////updateNotificationTimer.Stop();
        }

        return version;
    }

    // Version 2 was introduced because ...
    // It does this and that...
    private static async Task<int> UpdateFrom1To2Async(SQLiteAsyncConnection db, int version)
    {
        try
        {
        }
        catch (Exception ex)
        {
            HandleUpdateException(ex, $"{nameof(UpdateFrom1To2Async)} failed");
            return version;
        }

        ////BugHandler.ReportToAnalytics(_successException, "UpdateFrom1To2");

        return version + 1;
    }

    private static void HandleUpdateException(Exception ex, string customMessage)
    {
        ////bool enforceAnalyticsOnly = ex is SQLiteException { Message: "database is locked" };
        ////BugHandler.Handle(ex, customMessage, enforceAnalyticsOnly, enforceAnalyticsOnly);
    }
}