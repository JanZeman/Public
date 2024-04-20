using MauiTemplate2024.Core.Logic;

namespace MauiTemplate2024.SqLiteStorage;

public class SqLiteStorageService : IStorageService
{
    private const string DatabaseFileNameDefault = "MauiTemplate2024.db3";

    //public static string DatabasePath =>
    //    Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);

    private SqLiteDatabase _database;
    private bool _fileOwner;

    public string DatabaseFileName { get; set; }

    public bool IsTest { get; set; }

    private async Task Init()
    {
        if (_database is not null) return;

        if (string.IsNullOrEmpty(DatabaseFileName))
            DatabaseFileName = DatabaseFileNameDefault;

        _fileOwner = !File.Exists(DatabaseFileName);

        _database = new SqLiteDatabase(DatabaseFileName);
        await _database.InitAsync();
    }

    public async Task Close()
    {
        if (_database != null)
            await _database.CloseAsync();

        if (IsTest && _fileOwner)
            File.Delete(DatabaseFileName);
    }

    public async Task<int> SavePotato(Potato potato)
    {
        await Init();
        var entry = new PotatoEntry { PotatoId = potato.Id, Name = potato.Name, Description = potato.Description, Price = potato.Price, Currency = potato.Currency, LastSold = potato.LastSold, ImageUrl = potato.ImageUrl, IsFavorite = potato.IsFavorite };

        var alreadyExistingPotatoEntry = await GetPotatoEntry(potato.Id);

        if (alreadyExistingPotatoEntry == null)
        {
            try
            {
                return await _database.InsertAsync(entry);
            }
            catch (SQLiteException ex)
            {
                if (!ex.Message.Contains("UNIQUE constraint failed")) throw;
                return 0;
                //alreadyExistingPotatoEntry = await FixInsertUpdateRaceCondition();
            }
        }

        entry.Id = alreadyExistingPotatoEntry.Id;
        return await _database.UpdateAsync(entry);
    }

    private async Task<PotatoEntry> GetPotatoEntry(string id)
    {
        return await _database.Table<PotatoEntry>().FirstOrDefaultAsync(e => e.PotatoId == id);
    }

    public async Task<Potato> GetPotato(string textContent)
    {
        await Init();
        var entry = await GetPotatoEntry(textContent);
        return entry == null
        ? null
            : new Potato { Id = entry.PotatoId, Name = entry.Name, Description = entry.Description, Price = entry.Price, Currency = entry.Currency, LastSold = entry.LastSold, ImageUrl = entry.ImageUrl, IsFavorite = entry.IsFavorite };
    }

    public async Task<List<Potato>> GetPotatos()
    {
        await Init();
        var entries = await _database.Table<PotatoEntry>().ToListAsync();
        return entries.ConvertAll(e => new Potato { Id = e.PotatoId, Name = e.Name, Description = e.Description, Price = e.Price, Currency = e.Currency, LastSold = e.LastSold, ImageUrl = e.ImageUrl, IsFavorite = e.IsFavorite });
    }

    public async Task<List<Potato>> GetFavoritePotatos()
    {
        await Init();
        var entries = await _database.Table<PotatoEntry>()
            .Where(c => c.IsFavorite == true)
            .ToListAsync();
        return entries.ConvertAll(e => new Potato { Id = e.PotatoId, Name = e.Name, Description = e.Description, Price = e.Price, Currency = e.Currency, LastSold = e.LastSold, ImageUrl = e.ImageUrl, IsFavorite = e.IsFavorite });
    }

    public async Task DeleteAllPotatos()
    {
        await Init();
        await _database.DeleteAllAsync<PotatoEntry>();
    }
}