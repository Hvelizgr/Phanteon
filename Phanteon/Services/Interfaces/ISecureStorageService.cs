namespace Phanteon.Services.Interfaces
{
    // Wrapper para abstraer Plugin.SecureStorage y facilitar tests
    public interface ISecureStorageService
    {
        bool HasKey(string key);
        string Get(string key);
        void Set(string key, string value);
        void Remove(string key);
    }
}