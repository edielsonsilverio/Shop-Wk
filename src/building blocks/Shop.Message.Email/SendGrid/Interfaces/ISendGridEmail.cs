using Shop.Message.Email.SendGrid.Models;

namespace Shop.Message.Email.SendGrid.Interfaces;

public interface ISendGridEmail
{
    Task<(bool result, string errorMessage)> SendEmail(SendGridData data);
}
