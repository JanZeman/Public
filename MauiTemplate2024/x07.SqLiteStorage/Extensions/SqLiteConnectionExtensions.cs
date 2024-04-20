using JanZeman.System;

namespace MauiTemplate2024.SqLiteStorage.Extensions;

public static class SqLiteAsyncConnectionExtensions
{
    public static string GenerateTempFileName() => $"{StringExtensions.GetRandomString(12)}.db3";

    public static async Task<bool> TableExists(this SQLiteAsyncConnection db, string tableName)
    {
        var tableCount = await db.ExecuteScalarAsync<int>("SELECT COUNT(*) AS QtRecords FROM sqlite_master WHERE type = 'table' AND name = ?", tableName);
        return tableCount > 0;
    }

    ////public static bool TableExists<TTable>(this SQLiteAsyncConnection db)
    ////{
    ////    var tableMapping = new TableMapping(typeof(TTable));
    ////    return db.TableExists(tableMapping.TableName);
    ////}

    ////public static int DropTable(this SQLiteAsyncConnection db, string tableName)
    ////{
    ////    return db.Execute("DROP TABLE " + tableName);
    ////}
}