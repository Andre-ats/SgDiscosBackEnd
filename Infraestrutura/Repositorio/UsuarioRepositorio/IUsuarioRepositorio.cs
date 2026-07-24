using Domain.Entidade.UsuarioEntidade;
using FluentResults;

namespace Infraestrutura.Repositorio.UsuarioRepositorio;

public interface IUsuarioRepositorio
{
    public Result<Usuario> GetUsuarioById(Guid usuarioId);
    public Result<Usuario> GetUsuarioLogin(string login, string senha);
    public Result<bool> CriarUsuario(Usuario usuario);
}