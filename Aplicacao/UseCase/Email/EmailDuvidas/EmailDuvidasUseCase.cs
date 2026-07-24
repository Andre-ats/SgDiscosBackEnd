using Aplicacao.Service.Email;
using Aplicacao.UseCase.UseCaseAsync;
using Aplicacao.UseCase.UseCasePadrao;
using FluentResults;

namespace Aplicacao.UseCase.Email.EmailDuvidas;

public class EmailDuvidasUseCase : UseCaseAsyncBase<EmailDuvidasUseCaseInput, EmailDuvidasUseCaseOutput>
{
    private readonly IEmailService _emailService;

    public EmailDuvidasUseCase(IEmailService emailService)
    {
        _emailService = emailService;
    }
    protected override async Task<Result<EmailDuvidasUseCaseOutput>> ExecuteUseCase(EmailDuvidasUseCaseInput input)
    {
        var resultEmail = await _emailService.MandarEmailAsync(new EmailServiceInput(input.Email, input.Assunto, input.Corpo));

        if (resultEmail.IsFailed)
            return Result.Fail(resultEmail.Errors);

        return Result.Ok(new EmailDuvidasUseCaseOutput
        {
            Mensagem = "Mensagem enviada com sucesso!"
        });
    }
}