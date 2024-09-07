namespace GogoLibrary.Domain.Settings;

public class JwtSettings
{
    public const string DefaultSection = "JWT";
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string Authority { get; set; }
    public string JwtKey { get; set; }
    public int LifeTime { get; set; }
    public int RefreshTokenValidityInDays { get; set; }
}