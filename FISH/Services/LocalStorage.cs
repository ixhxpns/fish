using FISH.Services.Interface;
using Blazored.LocalStorage;

namespace FISH.Services
{
    public class LocalStorage : ILocalStorage
    {
        private readonly Blazored.LocalStorage.ILocalStorageService localStorage;

        public LocalStorage(Blazored.LocalStorage.ILocalStorageService localStorage)
        {
            this.localStorage = localStorage;
        }

        public async Task SetItem(string key, string value)
        {
            await this.localStorage.SetItemAsync(key, value);
        }

        public async  Task<string> GetItem(string key)
        {
            return await localStorage.GetItemAsync<string>(key);
        }

        public async Task RemoveItem(string key)
        {
            await this.localStorage.RemoveItemAsync(key);
        }

        public async Task Clear()
        {
           await this.localStorage.ClearAsync();
        }
    }
}
