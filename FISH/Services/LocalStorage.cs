using FISH.Services.Interface;

namespace FISH.Services;

public class LocalStorage : ILocalStorage
{
    private readonly Blazored.LocalStorage.ILocalStorageService localStorage;

    public LocalStorage(Blazored.LocalStorage.ILocalStorageService localStorage)
    {
        this.localStorage = localStorage;
    }

    public async Task SetItem(string key, string value)
    {
        await localStorage.SetItemAsync(key, value);
    }

    public async Task<string> GetItem(string key)
    {
        return await localStorage.GetItemAsync<string>(key);
    }

    public async Task RemoveItem(string key)
    {
        await localStorage.RemoveItemAsync(key);
    }

    public async Task Clear()
    {
        await localStorage.ClearAsync();
    }
}