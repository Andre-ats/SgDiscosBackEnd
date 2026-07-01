using FluentResults;

namespace Aplicacao.Service.Email;

public interface IEmailService
{
    public Task<Result> MandarEmailAsync(EmailServiceInput emailServiceInput);
}