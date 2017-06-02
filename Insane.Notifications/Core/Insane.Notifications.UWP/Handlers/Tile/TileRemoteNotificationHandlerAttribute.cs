using System;

namespace Insane.Notifications.UWP.Handlers.Tile
{
    public class TileRemoteNotificationHandlerAttribute : Attribute
    {
        public TileRemoteNotificationHandlerAttribute(string handlerId)
        {
            HandlerId = handlerId;
        }

        public string HandlerId { get; }
    }
}