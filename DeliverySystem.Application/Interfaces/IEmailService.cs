namespace DeliverySystem.Application.Interfaces
{
    public enum EmailType
    {
        ResetPassword = 1,
        ConfirmEmail = 2,
    }
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string link, EmailType subjectOfEmail = EmailType.ConfirmEmail, CancellationToken cancellationToken = default);
    }
}