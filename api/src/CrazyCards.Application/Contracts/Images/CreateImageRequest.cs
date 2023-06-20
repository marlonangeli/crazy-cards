namespace CrazyCards.Application.Contracts.Images;

public class CreateImageRequest
{
    public string Base64 { get; set; }
    public string MimeType { get; set; }
}