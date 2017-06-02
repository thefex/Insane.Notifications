using Windows.UI.Notifications;

namespace Insane.Notifications.UWP.Handlers.Tile
{
    public interface ITileRemoteNotificationHandlerIdProvider
    {
        string GetTileNotificationHandlerId(TileNotification tileNotification);
    }
}
