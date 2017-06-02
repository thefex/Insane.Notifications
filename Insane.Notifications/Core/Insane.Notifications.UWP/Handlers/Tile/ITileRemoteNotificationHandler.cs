using Windows.UI.Notifications;

namespace InsaneNotifications.UWP.Handlers.Tile
{
    public interface ITileRemoteNotificationHandler
    {
        void HandleTileNotification(TileNotification tileNotification);
    }
}
