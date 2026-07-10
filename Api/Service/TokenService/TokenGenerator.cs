using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api.Config;
using Api.DTO;
using Microsoft.IdentityModel.Tokens;

namespace Api.Service.TokenService;

public class TokenGenerator : ITokenGenerator
{

    private float _tempoExpiracao = 10;
    
    public string CreateToken(UsuarioToken usuarioToken)
    {
        var handler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(JwtConfig.Secret);
        
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256
        );


        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            SigningCredentials = credentials,
            Expires = DateTime.UtcNow.AddHours(_tempoExpiracao),
            Subject = GenerateClaims(usuarioToken)
        };
        
        var token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }

    private static ClaimsIdentity GenerateClaims(UsuarioToken usuarioToken)
    {
        var ci = new ClaimsIdentity();
        
        ci.AddClaim(new Claim("Id", usuarioToken.Id.ToString()));
        ci.AddClaim(new Claim(ClaimTypes.Email, usuarioToken.Email));
        ci.AddClaim(new Claim(ClaimTypes.GivenName, usuarioToken.Nome));
        ci.AddClaim(new Claim(ClaimTypes.Role, usuarioToken.Cargo));

        return ci;
    }
    
}