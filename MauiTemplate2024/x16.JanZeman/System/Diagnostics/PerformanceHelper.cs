using System.Diagnostics;

namespace JanZeman.System.Diagnostics;

public class PerformanceHelper
{
    //public TimeSpan Measure(Action measuredAction)
    //{
    //    var timer = Stopwatch.StartNew();
    //    measuredAction();
    //    timer.Stop();
    //    return timer.Elapsed;
    //}

    public async Task<TimeSpan> MeasureAsync(Func<Task> measuredAction)
    {
        var timer = Stopwatch.StartNew();
        await measuredAction.Invoke();
        timer.Stop();
        return timer.Elapsed;
    }
}