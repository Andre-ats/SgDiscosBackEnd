namespace Aplicacao.Service.Email;

public class EmailServiceInput
{
    public string Email { get; set; }
    public string Assunto { get; set; }
    public string Corpo { get; set; }
    
    public EmailServiceInput(string email, string assunto, string corpo)
    {
        Email = email;
        Assunto = assunto;
        Corpo = corpo;
    }
    
}