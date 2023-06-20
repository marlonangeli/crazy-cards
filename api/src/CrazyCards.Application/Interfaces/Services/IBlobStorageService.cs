namespace CrazyCards.Application.Interfaces.Services;

public interface IBlobStorageService
{
    Task<string> UploadAsync(Guid fileName, string extension, Stream content, CancellationToken cancellationToken);
    Task<Stream> DownloadAsync(Guid fileName, CancellationToken cancellationToken);
    
    Task<string> GetUrlAsync(Guid fileName, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid fileName, CancellationToken cancellationToken);
}