namespace FISH.Services.Interface
{
    public interface ILocalStorage
    {
        Task SetItem(string key, string value);
        Task<string> GetItem(string key);
        Task RemoveItem(string key);
        Task Clear();
    }
}
