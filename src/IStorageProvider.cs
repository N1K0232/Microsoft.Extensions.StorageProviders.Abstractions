namespace Microsoft.Extensions.StorageProviders.Abstractions;

public interface IStorageProvider : IDisposable, IAsyncDisposable
{
    IServiceProvider ServiceProvider { get; set; }

    string ReadAsString(string path);
    Stream ReadAsStream(string path);

    Task<string> ReadAsStringAsync(string path);
    Task<Stream> ReadAsStreamAsync(string path);
}