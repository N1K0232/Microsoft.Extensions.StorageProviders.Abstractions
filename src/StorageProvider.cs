namespace Microsoft.Extensions.StorageProviders.Abstractions;

public abstract class StorageProvider : IStorageProvider, IDisposable, IAsyncDisposable
{
    private IServiceProvider _serviceProvider;
    private bool _disposed = false;

    /// <summary>
    /// 
    /// </summary>
    protected StorageProvider()
    {
        ServiceProvider = null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="serviceProvider"></param>
    protected StorageProvider(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }


    /// <summary>
    /// 
    /// </summary>
    public virtual IServiceProvider ServiceProvider
    {
        get
        {
            IServiceProvider serviceProvider = _serviceProvider;
            if (serviceProvider == null)
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
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public abstract string ReadAsString(string path);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public abstract Stream ReadAsStream(string path);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public abstract Task<string> ReadAsStringAsync(string path);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public abstract Task<Stream> ReadAsStreamAsync(string path);


    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    public ValueTask DisposeAsync()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);

        return ValueTask.CompletedTask;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing && !_disposed)
        {
            _disposed = true;
        }
    }

    protected void ThrowIfDisposed()
    {
        bool disposed = _disposed;

        if (disposed)
        {
            throw new ObjectDisposedException(GetType().FullName);
        }
    }
}