namespace CrazyCards.Infrastructure.Options;

public class RabbitMQOptions
{
    public const string SectionName = "RabbitMQ";
    public string Host { get; set; } = null!;
    public int Port { get; set; }
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Exchange { get; set; } = null!;
}