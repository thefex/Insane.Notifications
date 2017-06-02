using System;

namespace InsaneNotifications.UWP.Handlers
{
    public class BadgeRemoteNotificationHandlerAttribute : Attribute
    {
        public BadgeRemoteNotificationHandlerAttribute(string handlerId)
        {
            HandlerId = handlerId;
        }

        public string HandlerId { get; }
    }
}