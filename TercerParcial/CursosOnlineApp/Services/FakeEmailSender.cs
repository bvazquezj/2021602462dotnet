using Microsoft.AspNetCore.Identity.UI.Services;

public class FakeEmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        Console.WriteLine($"Correo enviado a: {email}");
        Console.WriteLine($"Asunto: {subject}");
        Console.WriteLine($"Mensaje HTML: {htmlMessage}");
        return Task.CompletedTask;
    }
}