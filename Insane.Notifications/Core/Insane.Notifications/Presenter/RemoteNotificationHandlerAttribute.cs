using System;

namespace Insane.Notifications.Presenter
{
    public sealed class RemoteNotificationHandlerAttribute : Attribute
    {
        public RemoteNotificationHandlerAttribute(string notificationId)
        {
            NotificationId = notificationId;
        }

        public string NotificationId { get; }
    }
}