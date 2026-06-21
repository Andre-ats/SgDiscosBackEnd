using Aplicacao.UseCase.UseCaseBase;

namespace Aplicacao.UseCase.AdminUseCase.AdminLogin;

public class AdminLoginUseCaseInput : UseCaseBaseInput
{
    public string Login;
    public string Senha;
}