using Domain.Entidade.AdminEntidade;
using FluentResults;

namespace Infraestrutura.Repositorio.AdminRepositorio;

public class EFCoreAdminRepositorio : IAdminRepositorio
{
    private readonly DataBaseContext _dataBaseContext = null!;

    public EFCoreAdminRepositorio(DataBaseContext context)
    {
        _dataBaseContext = context;
    }
    
    public Result<bool> CriarAdmin(Admin admin)
    {
        bool jaExiste = _dataBaseContext.AdminsDB.Any(x => x.Email == admin.Email);

        if (jaExiste)
            return Result.Fail("Já existe um admin com este e-mail.");
        
        _dataBaseContext.AdminsDB.Add(admin);
        
        var salvou = _dataBaseContext.SaveChanges() > 0;

        if (!salvou)
            return Result.Fail("Não foi possível criar o admin.");

        return Result.Ok(true);
    }

    public Result<Admin> GetAdminLogin(string email, string senha)
    {
        var admin = _dataBaseContext.AdminsDB.FirstOrDefault(x => x.Email == email);
        
        if (admin is null)
            return Result.Fail("E-mail ou senha não encontrado");

        if (admin.Senha != senha)
            return Result.Fail("E-mail ou senha não encontrado");

        return Result.Ok(admin);
    }
}