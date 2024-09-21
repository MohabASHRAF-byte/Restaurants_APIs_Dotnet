namespace Restuarants.infrastructure.Configrutions;

public class BlobStorageSetting
{
    public string ConnectionString { get; set; } = default!;
    public string LogosContainerName { get; set; } = default!;
}