using Aplicacao.UseCase.UseCasePadrao;

namespace Aplicacao.UseCase.Email.EmailDuvidas;

public class EmailDuvidasUseCaseInput : UseCaseAsyncBaseInput
{
    public string Email { get; set; }
    public string Assunto { get; set; }
    public string Corpo { get; set; }

    public EmailDuvidasUseCaseInput(string email, string assunto, string corpo)
    {
        Email = email;
        Assunto = assunto;
        Corpo = corpo;
    }
}