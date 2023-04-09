namespace CrazyCards.Security.Settings;

public class OidcSettings
{
    public const string SectionName = "OidcSettings";
    public string Authority { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string Scope { get; set; }
}