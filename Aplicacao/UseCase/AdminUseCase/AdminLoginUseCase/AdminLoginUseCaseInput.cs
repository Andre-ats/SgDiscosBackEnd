using Aplicacao.UseCase.UseCaseBase;

namespace Aplicacao.UseCase.AdminUseCase.AdminLoginUseCase;

public class AdminLoginUseCaseInput : UseCaseBaseInput
{
    public string Login;
    public string Senha;
}