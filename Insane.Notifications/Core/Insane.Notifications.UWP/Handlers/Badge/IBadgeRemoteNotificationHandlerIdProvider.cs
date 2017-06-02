using Windows.UI.Notifications;

namespace InsaneNotifications.UWP.Handlers.Badge
{
    public interface IBadgeRemoteNotificationHandlerIdProvider
    {
        string GetBadgeNotificationHandlerId(BadgeNotification bageNotificaiton);
    }
}
