using System.Diagnostics;

namespace JanZeman.System.Diagnostics;

public static class Debussy
{
    public static void WriteLine(string message = "")
    {
#if DEBUG
        Debug.WriteLine(message);
#endif
    }

////    public static Task WriteLineAsync(string? message)
////    {
////#if DEBUG
////        WriteLine(message);
////#endif
////        return Task.CompletedTask;
////    }

    public static void WriteTimestamp()
    {
#if DEBUG
        var timestamp = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss:fff");
        WriteLine(timestamp);
#endif
    }

    ////public static void BreakIfAttached()
    ////{
    ////    BreakIfAttached(null, message);
    ////}

    ////public static void BreakIfAttached(string? message = null)
    ////{
    ////    BreakIfAttached(null, message);
    ////}

    public static void BreakIfAttached(Exception? ex = null, string? message = null)
    {
#if DEBUG
        if (ex != null)
            WriteLine(ex.Message);

        if (!string.IsNullOrEmpty(message))
            WriteLine(message);

        if (Debugger.IsAttached)
            Debugger.Break();
#endif
    }
}