namespace ClassManager.Data.Options;

public class AuthenticationSettings
{
    public string Secret { get; set; }
    public int TokenExpirationInMinutes { get; set; }
}