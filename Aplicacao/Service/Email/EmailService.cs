using FluentResults;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;

namespace Aplicacao.Service.Email;

public class EmailService : IEmailService
{
    
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public async Task<Result> MandarEmailAsync(EmailServiceInput emailServiceInput)
    {
        try
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress(
                "SG Discos",
                _configuration["Email:Email"] ?? throw new InvalidOperationException("Email:Email não configurado.")));

            message.To.Add(new MailboxAddress(
                emailServiceInput.Email,
                emailServiceInput.Email));

            message.Subject = emailServiceInput.Assunto;

            message.Body = new TextPart("plain")
            {
                Text = "Email Usuario: " + emailServiceInput.Email + "\n" + emailServiceInput.Corpo
            };

            using var client = new SmtpClient();

            await client.ConnectAsync(
                _configuration["Email:Host"] ?? throw new InvalidOperationException("Email:Host não configurado."),
                int.Parse(_configuration["Email:Port"] ?? throw new InvalidOperationException("Email:Port não configurado.")),
                MailKit.Security.SecureSocketOptions.StartTls);

            await client.AuthenticateAsync(
                _configuration["Email:Email"] ?? throw new InvalidOperationException("Email:Email não configurado."),
                _configuration["Email:Senha"] ?? throw new InvalidOperationException("Email:Senha não configurado."));

            await client.SendAsync(message);

            await client.DisconnectAsync(true);

            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Fail($"Erro ao enviar e-mail: {ex.Message}");
        }
    }
}