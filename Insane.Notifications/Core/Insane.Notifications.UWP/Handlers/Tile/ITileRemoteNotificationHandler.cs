using Windows.UI.Notifications;

namespace Insane.Notifications.UWP.Handlers.Tile
{
    public interface ITileRemoteNotificationHandler
    {
        void HandleTileNotification(TileNotification tileNotification);
    }
}
