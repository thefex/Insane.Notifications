using Windows.UI.Notifications;

namespace InsaneNotifications.UWP.Handlers
{
    public interface IBadgeRemoteNotificationHandler
    {
        void HandleBadgeNotification(BadgeNotification badgeNotification);
    }
}
