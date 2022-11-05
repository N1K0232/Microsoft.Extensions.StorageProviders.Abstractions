namespace Microsoft.Extensions.StorageProviders;

/// <summary>
/// contains all the methods that the storage providers must implement
/// </summary>
public interface IStorageProvider : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// gets or sets the ServiceProvider for this object
    /// </summary>
    IServiceProvider? ServiceProvider { get; set; }


    /// <summary>
    /// gets the content as a <see cref="string"/>
    /// </summary>
    /// <param name="path">the path of the file</param>
    /// <returns>the content as a <see cref="string"/></returns>
    string? ReadAsString(string? path);

    /// <summary>
    /// gets the content as a <see cref="Stream"/>
    /// </summary>
    /// <param name="path">the path of the file</param>
    /// <returns>the content as a <see cref="Stream"/></returns>
    Stream? ReadAsStream(string? path);

    /// <summary>
    /// like the <see cref="ReadAsString(string)"/> but asynchronously
    /// </summary>
    /// <param name="path">the path of the file</param>
    /// <returns>the content as <see cref="string"/></returns>
    Task<string> ReadAsStringAsync(string? path);

    /// <summary>
    /// like the <see cref="ReadAsStream(string)"/> but asynchronously
    /// </summary>
    /// <param name="path">the path of the file</param>
    /// <returns>the content as <see cref="Stream"/></returns>
    Task<Stream> ReadAsStreamAsync(string? path);
}