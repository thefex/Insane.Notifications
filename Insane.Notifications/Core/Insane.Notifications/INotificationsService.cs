using System.Threading.Tasks;
using Insane.Notifications.Data;

namespace Insane.Notifications
{
    public interface INotificationsService
    {
        Task<ServiceResponse> SubscribeToNotifications(bool forceSubscribe = false);
        Task<ServiceResponse> UnsubscribeFromNotifications();

        bool IsSubscribedToNotifications { get; }
    }
}