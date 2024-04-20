namespace MauiTemplate2024.Core.Services.Storage;

public interface IStorageService
{
    Task<int> SavePotato(Potato potato);
    Task<Potato> GetPotato(string textContent);
    Task<List<Potato>> GetPotatos();
    Task<List<Potato>> GetFavoritePotatos();
    Task DeleteAllPotatos();
}