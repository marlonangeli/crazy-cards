namespace CrazyCards.Application.Interfaces.Services;

public interface IBlobStorageService
{
    Task<string> UploadAsync(string fileName, Stream content, CancellationToken cancellationToken);
    Task<Stream> DownloadAsync(string fileName, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(string fileName, CancellationToken cancellationToken);
}