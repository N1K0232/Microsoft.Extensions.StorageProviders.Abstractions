using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.StorageProviders;

/// <summary>
/// the base class for all the storage providers
/// this class must be inherited
/// </summary>
public abstract class StorageProvider : IStorageProvider, IDisposable, IAsyncDisposable
{
    private IServiceProvider? _serviceProvider;
    private bool _disposed = false;

    /// <summary>
    /// creates a new instance of the <see cref="StorageProvider"/> class
    /// </summary>
    protected StorageProvider() : this(null)
    {
    }

    /// <summary>
    /// creates a new instance of the <see cref="StorageProvider"/> class with the specified
    /// <see cref="IServiceProvider"/>
    /// </summary>
    /// <param name="serviceProvider">the IServiceProvider for this instance</param>
    protected StorageProvider(IServiceProvider? serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }


    /// <summary>
    /// gets or sets the ServiceProvider for this object
    /// </summary>
    public virtual IServiceProvider? ServiceProvider
    {
        get
        {
            IServiceProvider? serviceProvider = _serviceProvider;
            if (serviceProvider is null)
            {
                return null;
            }

            IServiceScope serviceScope = serviceProvider.CreateScope();
            return serviceScope.ServiceProvider;
        }
        set
        {
            if (value == ServiceProvider)
            {
                return;
            }

            _serviceProvider = value;
        }
    }

    /// <summary>
    /// gets the content as a <see cref="string"/>
    /// </summary>
    /// <param name="path">the path of the file</param>
    /// <returns>the content as a <see cref="string"/></returns>
    public abstract string ReadAsString(string? path);

    /// <summary>
    /// gets the content as a <see cref="Stream"/>
    /// </summary>
    /// <param name="path">the path of the file</param>
    /// <returns>the content as a <see cref="Stream"/></returns>
    public abstract Stream ReadAsStream(string? path);

    /// <summary>
    /// like the <see cref="ReadAsString(string)"/> but asynchronously
    /// </summary>
    /// <param name="path">the path of the file</param>
    /// <returns>the content as <see cref="string"/></returns>
    public abstract Task<string> ReadAsStringAsync(string? path);

    /// <summary>
    /// like the <see cref="ReadAsStream(string)"/> but asynchronously
    /// </summary>
    /// <param name="path">the path of the file</param>
    /// <returns>the content as <see cref="Stream"/></returns>
    public abstract Task<Stream> ReadAsStreamAsync(string? path);


    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting
    /// unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting
    /// unmanaged resources asynchronously.
    /// </summary>
    /// <returns> A task that represents the asynchronous dispose operation.</returns>
    public ValueTask DisposeAsync()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);

        return ValueTask.CompletedTask;
    }

    /// <summary>
    /// performs the operation during object disposing
    /// </summary>
    /// <param name="disposing">true if the object should be disposed otherwise false</param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing && !_disposed)
        {
            _disposed = true;
        }
    }

    /// <summary>
    /// throws a <see cref="ObjectDisposedException"/> if the <see cref="_disposed"/>
    /// field is true
    /// </summary>
    /// <exception cref="ObjectDisposedException">if the object is disposed throws an exception</exception>
    protected void ThrowIfDisposed()
    {
        bool disposed = _disposed;

        if (disposed)
        {
            throw new ObjectDisposedException(GetType().FullName);
        }
    }
}