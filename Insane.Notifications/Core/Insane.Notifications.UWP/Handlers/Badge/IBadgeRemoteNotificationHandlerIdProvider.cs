using Windows.UI.Notifications;

namespace Insane.Notifications.UWP.Handlers.Badge
{
    public interface IBadgeRemoteNotificationHandlerIdProvider
    {
        string GetBadgeNotificationHandlerId(BadgeNotification bageNotificaiton);
    }
}
