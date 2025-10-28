using Plugin.SecureStorage;
using Phanteon.Services.Interfaces;

namespace Phanteon.Services.Implementations
{
    public class SecureStorageService : ISecureStorageService
    {
        public bool HasKey(string key)
        {
            try { return CrossSecureStorage.Current.HasKey(key); }
            catch { return false; }
        }

        public string Get(string key)
        {
            try { return CrossSecureStorage.Current.GetValue(key); }
            catch { return null; }
        }

        public void Set(string key, string value)
        {
            try { CrossSecureStorage.Current.SetValue(key, value); }
            catch { /* manejar/registrar según necesidad */ }
        }

        public void Remove(string key)
        {
            try { CrossSecureStorage.Current.DeleteKey(key); }
            catch { /* manejar/registrar según necesidad */ }
        }
    }
}