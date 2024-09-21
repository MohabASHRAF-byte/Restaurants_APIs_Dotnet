using Azure.Storage.Blobs;
using Microsoft.Extensions.Options;
using Restaurants.Domain.Interfaces;
using Restuarants.infrastructure.Configrutions;

namespace Restuarants.infrastructure.Storage.Blob;

public class BlobStorageService(
    IOptions<BlobStorageSetting> settingOptions
) : IBlobStorageService
{
    private readonly BlobStorageSetting _setting = settingOptions.Value;

    public Task<string> UploadAsync(Stream data, string fileName)
    {
        var client = new BlobServiceClient(_setting.ConnectionString);
        var container = client.GetBlobContainerClient(_setting.LogosContainerName);
        var blobClient = container.GetBlobClient(fileName);
        blobClient.UploadAsync(data);
        var blobUri = blobClient.Uri.ToString();
        return Task.FromResult(blobUri);
    }
}