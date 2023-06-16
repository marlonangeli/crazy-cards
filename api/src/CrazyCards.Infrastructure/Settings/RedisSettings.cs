namespace CrazyCards.Infrastructure.Settings;

internal sealed class RedisSettings
{
    public const string SectionName = "Redis";
    public string Host { get; set; }  = null!;
    public int Port { get; set; }
    public string Password { get; set; } = null!;
    public string InstanceName { get; set; } = null!;
}