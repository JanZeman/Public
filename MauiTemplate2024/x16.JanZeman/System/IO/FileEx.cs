namespace JanZeman.System.IO;

public static class FileEx
{
    public static void DeleteSafely(string path)
    {
        if (!File.Exists(path)) return;

        try
        {
            File.Delete(path);
        }
        catch (Exception ex)
        {
            Debussy.BreakIfAttached(ex);
        }
    }

    public static void CopySafely(string sourceFileName, string destFileName, bool overwrite)
    {
        if (!File.Exists(sourceFileName)) return;

        try
        {
            File.Copy(sourceFileName, destFileName, overwrite);
        }
        catch (Exception ex)
        {
            Debussy.BreakIfAttached(ex);
        }
    }
}