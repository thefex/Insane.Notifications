using Windows.UI.Notifications;

namespace InsaneNotifications.UWP.Handlers
{
    public interface IBadgeRemoteNotificationHandlerIdProvider
    {
        string GetBadgeNotificationHandlerId(BadgeNotification bageNotificaiton);
    }
}
