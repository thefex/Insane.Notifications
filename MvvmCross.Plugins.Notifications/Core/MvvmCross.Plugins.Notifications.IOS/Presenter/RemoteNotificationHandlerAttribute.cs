using System;
using System.Runtime.InteropServices;

namespace MvvmCross.Plugins.Notifications.IOS.NotificationsPresenter
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