namespace MauiTemplate2024.Core.Services.Storage;

public class DatabaseCreationException : MauiTemplate2024Exception
{
    public DatabaseCreationException()
    {
    }

    public DatabaseCreationException(string message)
        : base(message)
    {
    }

    public DatabaseCreationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}