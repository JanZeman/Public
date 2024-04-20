namespace MauiTemplate2024.SqLiteStorage.Data;

[Table(SchemaEntry.Potatoes)]
public class PotatoEntry
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; } // Internal ID for the database entry

    [NotNull]
    public string PotatoId { get; set; } // Corresponds to the 'Id' in the Potato class

    public string Name { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public string Currency { get; set; }

    public DateTime LastSold { get; set; }

    public string ImageUrl { get; set; }

    public bool IsFavorite { get; set; }
}