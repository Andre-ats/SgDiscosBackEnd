using Api.DTO;

namespace Api.Service.TokenService;

public interface ITokenGenerator
{
    public string CreateToken(UsuarioToken usuarioToken);
}