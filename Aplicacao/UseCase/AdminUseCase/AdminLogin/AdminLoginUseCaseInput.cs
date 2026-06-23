using Aplicacao.UseCase.UseCasePadrao;

namespace Aplicacao.UseCase.AdminUseCase.AdminLogin;

public class AdminLoginUseCaseInput : UseCaseBaseInput
{
    public string Login;
    public string Senha;
}