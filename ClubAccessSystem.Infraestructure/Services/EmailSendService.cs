

using ClubAccessSystem.Infraestructure.Interfaces;
using ClubAccessSystem.Infraestructure.Models;
using ClubAccessSystem.Infraestructure.Results;
using System.Net.Mail;
using System.Net;

namespace ClubAccessSystem.Infraestructure.Services
{
    public class EmailSendService : INotificationService
    {
        public async Task<NotificationResult> SendEmailAsync(EmailModels emailModels)
        {
            NotificationResult result = new NotificationResult();

            try
            {
                using (var client = new SmtpClient())
                {
                    client.Host = "";
                    client.Port = 0;
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential("user", "pwd");

                    var message = new MailMessage(emailModels.From!, emailModels.To!);
                    message.Body = emailModels.Body;
                    message.IsBodyHtml = true;
                    message.Subject = emailModels.Subject;

                    await client.SendMailAsync(message);

                }
            }
            catch (Exception ex)
            {

                result.Message = $"Error realizando la notificación {ex.Message}";
            }

            return result;
        }
    }
}
