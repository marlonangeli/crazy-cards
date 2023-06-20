using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using CrazyCards.Application.Interfaces.Services;
using CrazyCards.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;

namespace CrazyCards.Infrastructure.Storage;

public class BlobStorageService : IBlobStorageService
{
    private readonly BlobContainerClient _containerClient;

    public BlobStorageService(IConfiguration configuration)
    {
        var azureSettings = configuration.GetSection(AzureStorageSettings.SectionName).Get<AzureStorageSettings>()!;
        var blobServiceClient = new BlobServiceClient(azureSettings.ConnectionString);
        _containerClient = blobServiceClient.GetBlobContainerClient(azureSettings.ContainerName);
    }

    public async Task<string> UploadAsync(Guid filename, string extension, Stream content, CancellationToken cancellationToken)
    {
        var blobClient = _containerClient.GetBlobClient(filename + extension);
        await blobClient.UploadAsync(content, new BlobUploadOptions
        {
            Tags = new Dictionary<string, string>
            {
                {"Id", filename.ToString()},
                {"UploadedAt", $"{DateTime.UtcNow}"}
            }
        }, cancellationToken);
        return blobClient.Uri.AbsoluteUri;
    }
    
    public async Task<string> GetUrlAsync(Guid fileName, CancellationToken cancellationToken)
    {
        var blobs = _containerClient.GetBlobsAsync(
            BlobTraits.Tags,
            BlobStates.None,
            fileName.ToString(),
            cancellationToken);
        
        await foreach(var blob in blobs)
        {
            if (blob.Tags.TryGetValue("Id", out var id) && id == fileName.ToString())
            {
                return _containerClient.GetBlobClient(blob.Name).Uri.AbsoluteUri;
            }
        }

        return string.Empty;
    }

    public async Task<Stream> DownloadAsync(Guid fileName, CancellationToken cancellationToken)
    {
        var blobClient = _containerClient.GetBlobClient(fileName.ToString());
        return await blobClient.OpenReadAsync(cancellationToken: cancellationToken);
    }

    public async Task<bool> DeleteAsync(Guid fileName, CancellationToken cancellationToken)
    {
        var blobClient = _containerClient.GetBlobClient(fileName.ToString());
        return await blobClient.DeleteIfExistsAsync(cancellationToken: cancellationToken);
    }
}