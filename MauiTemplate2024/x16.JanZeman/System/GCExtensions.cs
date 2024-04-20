namespace JanZeman.System;

public static class GarbageCollector
{
    public static void Collect()
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
    }
}