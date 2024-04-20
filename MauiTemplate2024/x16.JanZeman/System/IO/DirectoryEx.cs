namespace JanZeman.System.IO;

public static class DirectoryEx
{
    public static void DeleteFilesSafely(string? directoryPath, string containingFilesPattern)
    {
        if (string.IsNullOrEmpty(directoryPath)) return;
        var directoryInfo = new DirectoryInfo(directoryPath);
        directoryInfo.DeleteFilesSafely(containingFilesPattern);
    }

    public static void EnsureDirectoryExists(string directoryPath)
    {
        if (string.IsNullOrEmpty(directoryPath) || Directory.Exists(directoryPath)) return;
        Directory.CreateDirectory(directoryPath);
    }
}