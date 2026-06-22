namespace Api.Config;

public class JwtConfig
{
    private static readonly IConfiguration _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json",optional:false,reloadOnChange:false).Build();
    
    public static readonly string Secret = _configuration.GetValue<string>("Jwt:Key") ?? throw new InvalidOperationException();
}