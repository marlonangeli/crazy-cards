namespace CrazyCards.Infrastructure.Settings;

internal sealed class AzureStorageSettings
{
    public const string SectionName = "AzureStorage";
    public string ConnectionString { get; set; } = null!;
    public string ContainerName { get; set; } = null!;
}