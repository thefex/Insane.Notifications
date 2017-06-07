using Windows.UI.Notifications;
using Insane.Notifications.UWP.Handlers.Badge;

namespace Insane.Notifications.PushSample.UWP.Services.Handlers
{
    class BadgeNotificationsIdProvider : IBadgeRemoteNotificationHandlerIdProvider
    {
        public string GetBadgeNotificationHandlerId(BadgeNotification bageNotificaiton)
        {
            return string.Empty;
        }
    }
}