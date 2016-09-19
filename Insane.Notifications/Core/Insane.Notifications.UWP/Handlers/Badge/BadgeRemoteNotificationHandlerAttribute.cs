using System;

namespace Insane.Notifications.UWP.Handlers.Badge
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