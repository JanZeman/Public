using JanZeman.System.IO;

namespace MauiTemplate2024.SqLiteStorage.Tests;

// ReSharper disable once ClassNeverInstantiated.Global
public class SqLiteDatabaseTestsFixture : IDisposable
{
    public SqLiteDatabaseTestsFixture()
    {
        // Delete all .db3 files - remnants from the previously unfinished test runs
        DirectoryEx.DeleteFilesSafely(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "*.db3");
    }

    public void Dispose()
    {
        // Delete all .db3 files - guard the cases when the individual tests 'forgot' to clean up
        DirectoryEx.DeleteFilesSafely(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "*.db3");
    }
}