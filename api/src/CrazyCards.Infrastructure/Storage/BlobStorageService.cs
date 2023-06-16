using Azure.Storage.Blobs;
using CrazyCards.Application.Interfaces.Services;
using CrazyCards.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;

namespace CrazyCards.Infrastructure.Storage;

public class BlobStorageService : IBlobStorageService
{
    private readonly BlobContainerClient _containerClient;

    public BlobStorageService(IConfiguration configuration)
    {
        var azureSettings = configuration.GetSection("AzureStorage").Get<AzureStorageSettings>()!;
        var blobServiceClient = new BlobServiceClient(azureSettings.ConnectionString);
        _containerClient = blobServiceClient.GetBlobContainerClient(azureSettings.ContainerName);
    }

    public Task<string> UploadAsync(string fileName, Stream content, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Stream> DownloadAsync(string fileName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(string fileName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}