namespace JanZeman.System.IO;

public static class FileInfoExtensions
{
    public static void DeleteSafely(this FileInfo fileInfo)
    {
        if (!fileInfo.Exists) return;

        try
        {
            fileInfo.Delete();
        }
        catch (Exception ex)
        {
            Debussy.BreakIfAttached(ex);
        }
    }
}