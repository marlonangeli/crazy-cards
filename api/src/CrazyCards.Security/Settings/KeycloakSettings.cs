namespace CrazyCards.Security.Settings;

public sealed class KeycloakSettings
{
    public const string SectionName = "Keycloak";
    public string Realm { get; set; }
    public string AuthServerUrl { get; set; }
    public string SslRequired { get; set; }
    public string Resource { get; set; }
    public Credentials Credentials { get; set; }
    public bool VerifyTokenAudience { get; set; }
    public int ConfidentialPort { get; set; }
}

public sealed class Credentials
{
    public string Secret { get; set; }
}
