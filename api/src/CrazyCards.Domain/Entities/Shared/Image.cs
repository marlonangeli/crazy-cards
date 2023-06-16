namespace CrazyCards.Domain.Entities.Shared;

public class Image : Entity
{
    public int Size { get; set; }
    public string MimeType { get; set; } = string.Empty;
}