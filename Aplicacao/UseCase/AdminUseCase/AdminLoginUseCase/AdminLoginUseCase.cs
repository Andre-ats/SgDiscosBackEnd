using Aplicacao.UseCase.UseCaseBase;
using Domain.Utilitarios;
using FluentResults;
using Infraestrutura.Repositorio.AdminRepositorio;

namespace Aplicacao.UseCase.AdminUseCase.AdminLoginUseCase;

public class AdminLoginUseCase : IUseCaseBase<AdminLoginUseCaseInput, AdminLoginUseCaseOutput>
{
    private readonly IAdminRepositorio _adminRepositorio;
    
    public AdminLoginUseCase(IAdminRepositorio adminRepositorio)
    {
        _adminRepositorio = adminRepositorio;
    }
    
    protected override Result<AdminLoginUseCaseOutput> executeUseCase(AdminLoginUseCaseInput input)
    {
        var admin = _adminRepositorio.GetAdminLogin(input.Login, Hash256.CriptografiaSenha(input.Senha));

        if (admin.IsFailed)
        {
            return Result.Fail(admin.Errors);
        }

        return Result.Ok(new AdminLoginUseCaseOutput()
            {
                Admin = admin.Value
            }
        );
    }
}