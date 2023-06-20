namespace CrazyCards.Application.Contracts.Images;

public class ImageResponse
{
    public Guid Id { get; set; }
    public string Url { get; set; }
    public int Size { get; set; }
    public string MimeType { get; set; }
}