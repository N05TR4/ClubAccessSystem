

using ClubAccessSystem.Infraestructure.Models;
using ClubAccessSystem.Infraestructure.Results;

namespace ClubAccessSystem.Infraestructure.Interfaces
{
    public interface INotificationService
    {
        Task<NotificationResult> SendEmailAsync(EmailModels emailModels);
    }
}
