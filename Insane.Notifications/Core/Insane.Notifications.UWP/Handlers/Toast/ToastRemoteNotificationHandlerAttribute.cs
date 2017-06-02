using System;

namespace Insane.Notifications.UWP.Handlers.Toast
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