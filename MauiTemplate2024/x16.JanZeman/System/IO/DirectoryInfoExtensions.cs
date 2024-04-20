namespace JanZeman.System.IO;

public static class DirectoryInfoExtensions
{
    public static void DeleteFilesSafely(this DirectoryInfo directoryInfo, string pattern)
    {
        if (!directoryInfo.Exists) return;

        var fileInfos = directoryInfo.GetFiles(pattern);
        foreach (var fileInfo in fileInfos)
            fileInfo.DeleteSafely();
    }
}