using Domain.Entidade.AdminEntidade;
using FluentResults;

namespace Infraestrutura.Repositorio.AdminRepositorio;

public interface IAdminRepositorio
{
    public Result<bool> CriarAdmin(Admin admin);
    public Result<Admin> GetAdminLogin(string login, string senha);
}