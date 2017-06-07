using Windows.UI.Notifications;
using Insane.Notifications.UWP.Handlers.Tile;

namespace Insane.Notifications.PushSample.UWP.Services.Handlers
{
    class TileNotificationsIdProvider : ITileRemoteNotificationHandlerIdProvider
    {
        public string GetTileNotificationHandlerId(TileNotification tileNotification)
        {
            return string.Empty;
        }
    }
}