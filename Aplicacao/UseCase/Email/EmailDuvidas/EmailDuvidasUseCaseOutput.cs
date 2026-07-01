using Aplicacao.UseCase.UseCasePadrao;

namespace Aplicacao.UseCase.Email.EmailDuvidas;

public class EmailDuvidasUseCaseOutput : UseCaseAsyncBaseOutput
{
    public string Mensagem { get; set; }
}