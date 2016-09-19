using Windows.UI.Notifications;

namespace Insane.Notifications.UWP.Handlers.Badge
{
    public interface IBadgeRemoteNotificationHandler
    {
        void HandleBadgeNotification(BadgeNotification badgeNotification);
    }
}
