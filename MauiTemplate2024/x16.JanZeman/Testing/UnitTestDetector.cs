namespace JanZeman.Testing;

/// <summary>
/// Detect if the given piece of code is running as part of a unit test session.
/// </summary>    
public static class UnitTestDetector
{
    private static bool? _runningXUnit;

    public static bool IsRunningXUnit => _runningXUnit ?? DetectIsRunningXUnit();

    private static bool DetectIsRunningXUnit()
    {
        _runningXUnit = false;
        
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            if (!assembly.GetName().FullName.ToLowerInvariant().StartsWith("xunit.core")) continue;
            _runningXUnit = true;
            break;
        }

        return _runningXUnit.Value;
    }
}