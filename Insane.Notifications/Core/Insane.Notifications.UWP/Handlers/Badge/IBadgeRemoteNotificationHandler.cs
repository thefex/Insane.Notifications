using Windows.UI.Notifications;

namespace InsaneNotifications.UWP.Handlers.Badge
{
    public interface IBadgeRemoteNotificationHandler
    {
        void HandleBadgeNotification(BadgeNotification badgeNotification);
    }
}
