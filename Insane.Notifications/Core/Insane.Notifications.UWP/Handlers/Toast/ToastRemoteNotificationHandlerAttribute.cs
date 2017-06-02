using System;

namespace InsaneNotifications.UWP.Handlers
{
    public class ToastRemoteNotificationHandlerAttribute : Attribute
    {
        public ToastRemoteNotificationHandlerAttribute(string handlerId)
        {
            HandlerId = handlerId;
        }

        public string HandlerId { get; }
    }
}