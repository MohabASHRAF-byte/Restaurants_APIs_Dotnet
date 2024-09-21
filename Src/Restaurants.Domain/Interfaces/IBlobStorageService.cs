namespace Restaurants.Domain.Interfaces;

public interface IBlobStorageService
{
    public Task<string> UploadAsync(Stream data, string fileName);
}