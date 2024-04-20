namespace MauiTemplate2024.Core.Exceptions;

public class MauiTemplate2024Exception : Exception
{
    public MauiTemplate2024Exception()
    {
    }

    public MauiTemplate2024Exception(string message) : base(message)
    {
    }

    public MauiTemplate2024Exception(string message, Exception innerException) : base(message, innerException)
    {
    }
}